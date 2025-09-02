
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Rendering;
//using MVCStore.Models;
//using MVCStore.Services;
//using System.Linq;

//public class ProductController : Controller
//{
//    private readonly ApplicationDBContext context;

//    public ProductController(ApplicationDBContext context)
//    {
//        this.context = context;
//    }

//    public IActionResult Index()
//    {
//        var products = context.products.OrderByDescending(p => p.Id).ToList();
//        return View(products);
//    }

//    // ---------- CREATE ----------
//    [HttpGet]
//    public IActionResult Create()
//    {
//        var doctors = context.Doctors.ToList();
//        ViewBag.DoctorsList = doctors;
//        ViewBag.DoctorMap = doctors.ToDictionary(
//            d => d.DoctorId,
//            d => new { d.DoctorName, d.DepartmentId }
//        );
//        return View();
//    }

//    [HttpPost]
//    public IActionResult Create(ProductDto productDto)
//    {
//        var doc = context.Doctors.FirstOrDefault(d => d.DoctorId == productDto.referencing_doctor_id);
//        if (doc == null)
//            ModelState.AddModelError("referencing_doctor_id", "Please select a valid doctor.");

//        if (!ModelState.IsValid)
//        {
//            var doctors = context.Doctors.ToList();
//            ViewBag.DoctorsList = doctors;
//            ViewBag.DoctorMap = doctors.ToDictionary(d => d.DoctorId, d => new { d.DoctorName, d.DepartmentId });
//            return View(productDto);
//        }

//        var product = new Product
//        {
//            name = productDto.name,
//            age = productDto.age,
//            sex = productDto.sex,
//            adrress = productDto.address,
//            city = productDto.city,
//            phone_number = productDto.phone_number,
//            entry_date = productDto.entry_date,
//            referencing_doctor_id = doc!.DoctorId,
//            RefDr = doc.DoctorName,
//            department_id = doc.DepartmentId,
//            diagnosis = productDto.diagnosis
//        };

//        context.products.Add(product);
//        context.SaveChanges();
//        return RedirectToAction("Index");
//    }

//    // ---------- EDIT ----------
//    [HttpGet]
//    public IActionResult Edit(int id)
//    {
//        var product = context.products.Find(id);
//        if (product == null) return NotFound();

//        var doctors = context.Doctors.ToList();
//        ViewBag.DoctorsList = doctors;
//        ViewBag.DoctorMap = doctors.ToDictionary(d => d.DoctorId, d => new { d.DoctorName, d.DepartmentId });

//        var dto = new ProductDto
//        {
//            Id = product.Id,
//            name = product.name,
//            age = product.age,
//            sex = product.sex,
//            address = product.adrress,
//            city = product.city,
//            phone_number = product.phone_number,
//            entry_date = product.entry_date,
//            referencing_doctor_id = product.referencing_doctor_id,
//            RefDr = product.RefDr,
//            department_id = product.department_id,
//            diagnosis = product.diagnosis
//        };

//        return View(dto);
//    }

//    [HttpPost]
//    public IActionResult Edit(int id, ProductDto productDto)
//    {
//        var product = context.products.Find(id);
//        if (product == null) return NotFound();

//        var doc = context.Doctors.FirstOrDefault(d => d.DoctorId == productDto.referencing_doctor_id);
//        if (doc == null)
//            ModelState.AddModelError("referencing_doctor_id", "Please select a valid doctor.");

//        if (!ModelState.IsValid)
//        {
//            var doctors = context.Doctors.ToList();
//            ViewBag.DoctorsList = doctors;
//            ViewBag.DoctorMap = doctors.ToDictionary(d => d.DoctorId, d => new { d.DoctorName, d.DepartmentId });
//            return View(productDto);
//        }

//        product.name = productDto.name;
//        product.age = productDto.age;
//        product.sex = productDto.sex;
//        product.adrress = productDto.address;
//        product.city = productDto.city;
//        product.phone_number = productDto.phone_number;
//        product.entry_date = productDto.entry_date;
//        product.referencing_doctor_id = doc!.DoctorId;
//        product.RefDr = doc.DoctorName;
//        product.department_id = doc.DepartmentId;
//        product.diagnosis = productDto.diagnosis;

//        context.SaveChanges();
//        return RedirectToAction("Index");
//    }

//    [HttpPost]
//    [ValidateAntiForgeryToken]
//    public IActionResult Delete(int id)
//    {
//        var product = context.products.Find(id);
//        if (product != null)
//        {
//            context.products.Remove(product);
//            context.SaveChanges();
//            TempData["Message"] = "Patient deleted successfully";
//        }
//        return RedirectToAction("Index");
//    }
//}


//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Rendering;
//using Microsoft.EntityFrameworkCore;
//using System.Linq;


using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVCStore.Models;
using MVCStore.Services;
using System.Linq;
using Microsoft.EntityFrameworkCore;
public class ProductController : Controller
{
    private readonly ApplicationDBContext context;

    public ProductController(ApplicationDBContext context)
    {
        this.context = context;
    }

    public IActionResult Index()
    {
        // include Room so we can show RoomNo/BedNo/Floor
        var products = context.products
            .Include(p => p.Room)
            .OrderByDescending(p => p.Id)
            .ToList();

        return View(products);
    }

    // ---------- CREATE ----------
    [HttpGet]
    public IActionResult Create()
    {
        var doctors = context.Doctors.ToList();
        ViewBag.DoctorsList = doctors;
        ViewBag.DoctorMap = doctors.ToDictionary(
            d => d.DoctorId,
            d => new { d.DoctorName, d.DepartmentId }
        );

        // available rooms for the modal
        ViewBag.Rooms = context.Rooms
            .Where(r => !r.IsOccupied)
            .OrderBy(r => r.Floor).ThenBy(r => r.RoomNo).ThenBy(r => r.BedNo)
            .ToList();

        return View(new ProductDto());
    }

    [HttpPost]
    public IActionResult Create(ProductDto productDto)
    {
        var doc = context.Doctors.FirstOrDefault(d => d.DoctorId == productDto.referencing_doctor_id);
        if (doc == null)
            ModelState.AddModelError("referencing_doctor_id", "Please select a valid doctor.");

        if (!productDto.Room_id.HasValue)
            ModelState.AddModelError("Room_id", "Please pick a room.");

        if (!ModelState.IsValid)
        {
            var doctors = context.Doctors.ToList();
            ViewBag.DoctorsList = doctors;
            ViewBag.DoctorMap = doctors.ToDictionary(d => d.DoctorId, d => new { d.DoctorName, d.DepartmentId });
            ViewBag.Rooms = context.Rooms.Where(r => !r.IsOccupied).ToList();
            return View(productDto);
        }

        var product = new Product
        {
            name = productDto.name,
            age = productDto.age,
            sex = productDto.sex,
            adrress = productDto.address,
            city = productDto.city,
            phone_number = productDto.phone_number,
            entry_date = productDto.entry_date,
            referencing_doctor_id = doc!.DoctorId,
            RefDr = doc.DoctorName,
            department_id = doc.DepartmentId,
            diagnosis = productDto.diagnosis,
            Room_id = productDto.Room_id
        };

        context.products.Add(product);

        // mark room occupied
        var room = context.Rooms.Find(productDto.Room_id!.Value);
        if (room != null) room.IsOccupied = true;

        context.SaveChanges();
        return RedirectToAction("Index");
    }

    // ---------- EDIT ----------
    [HttpGet]
    public IActionResult Edit(int id)
    {
        var product = context.products.Include(p => p.Room).FirstOrDefault(p => p.Id == id);
        if (product == null) return NotFound();

        var doctors = context.Doctors.ToList();
        ViewBag.DoctorsList = doctors;
        ViewBag.DoctorMap = doctors.ToDictionary(d => d.DoctorId, d => new { d.DoctorName, d.DepartmentId });

        ViewBag.Rooms = context.Rooms.Where(r => !r.IsOccupied || r.Room_id == product.Room_id).ToList();

        var dto = new ProductDto
        {
            Id = product.Id,
            name = product.name,
            age = product.age,
            sex = product.sex,
            address = product.adrress,
            city = product.city,
            phone_number = product.phone_number,
            entry_date = product.entry_date,
            referencing_doctor_id = product.referencing_doctor_id,
            RefDr = product.RefDr,
            department_id = product.department_id,
            diagnosis = product.diagnosis,
            Room_id = product.Room_id
        };

        return View(dto);
    }

    [HttpPost]
    public IActionResult Edit(int id, ProductDto productDto)
    {
        var product = context.products.Find(id);
        if (product == null) return NotFound();

        var doc = context.Doctors.FirstOrDefault(d => d.DoctorId == productDto.referencing_doctor_id);
        if (doc == null)
            ModelState.AddModelError("referencing_doctor_id", "Please select a valid doctor.");

        if (!productDto.Room_id.HasValue)
            ModelState.AddModelError("Room_id", "Please pick a room.");

        if (!ModelState.IsValid)
        {
            var doctors = context.Doctors.ToList();
            ViewBag.DoctorsList = doctors;
            ViewBag.DoctorMap = doctors.ToDictionary(d => d.DoctorId, d => new { d.DoctorName, d.DepartmentId });
            ViewBag.Rooms = context.Rooms.Where(r => !r.IsOccupied || r.Room_id == product.Room_id).ToList();
            return View(productDto);
        }

        // free previous room if changed
        if (product.Room_id.HasValue && product.Room_id != productDto.Room_id)
        {
            var prev = context.Rooms.Find(product.Room_id.Value);
            if (prev != null) prev.IsOccupied = false;
        }

        product.name = productDto.name;
        product.age = productDto.age;
        product.sex = productDto.sex;
        product.adrress = productDto.address;
        product.city = productDto.city;
        product.phone_number = productDto.phone_number;
        product.entry_date = productDto.entry_date;
        product.referencing_doctor_id = doc!.DoctorId;
        product.RefDr = doc.DoctorName;
        product.department_id = doc.DepartmentId;
        product.diagnosis = productDto.diagnosis;
        product.Room_id = productDto.Room_id;

        var room = context.Rooms.Find(productDto.Room_id!.Value);
        if (room != null) room.IsOccupied = true;

        context.SaveChanges();
        return RedirectToAction("Index");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(int id)
    {
        var product = context.products.Find(id);
        if (product != null)
        {
            // free room on delete
            if (product.Room_id.HasValue)
            {
                var room = context.Rooms.Find(product.Room_id.Value);
                if (room != null) room.IsOccupied = false;
            }

            context.products.Remove(product);
            context.SaveChanges();
            TempData["Message"] = "Patient deleted successfully";
        }
        return RedirectToAction("Index");
    }
}

