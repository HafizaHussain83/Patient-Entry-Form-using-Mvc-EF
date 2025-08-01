//using Microsoft.AspNetCore.Mvc;
//using MVCStore.Models;
//using MVCStore.Services;

//namespace MVCStore.Controllers
//{
//    public class ProductController : Controller
//    {
//        private readonly ApplicationDBContext context;

//        public ProductController(ApplicationDBContext context, IWebHostEnvironment environment)
//        {
//            this.context = context;
//        }
//        public IActionResult Index()
//        {
//            var product = context.products.OrderByDescending(p => p.Id).ToList();
//            return View(product);
//        }
//        public IActionResult Create()
//        {
//            return View();

//        }
//        [HttpPost]
//        public IActionResult Create(ProductDto productDto)
//        {
//            if (productDto.phone_number == null)
//            {
//                ModelState.AddModelError("phone Number:", "Phone Number is required");
//            }

//            if (!ModelState.IsValid)
//            {
//                return View(productDto);
//            }

//            // Check if entry_date has value before assigning
//            if (!productDto.entry_date.HasValue)
//            {
//                ModelState.AddModelError("entry_date", "Entry date is required");
//                return View(productDto);
//            }

//            var product = new Product
//            {
//                name = productDto.name,
//                age = productDto.age,
//                sex = productDto.sex,
//                adrress = productDto.adrress,
//                city = productDto.city,
//                phone_number = productDto.phone_number,
//                entry_date = productDto.entry_date.Value, // Use .Value to get the DateTime from DateTime?
//                referencing_doctor_id = productDto.referencing_doctor_id,
//                diagnosis = productDto.diagnosis,
//                department_id = productDto.department_id
//            };

//            context.products.Add(product);
//            context.SaveChanges();

//            return RedirectToAction("Index", "Product");
//        }

//        public IActionResult Edit(int id)
//        {
//            var product = context.products.Find(id);
//            if (product == null)
//            {
//                return RedirectToAction("Index", "Product");
//            }

//            var productDto = new ProductDto
//            {
//                name = product.name,
//                age = product.age,
//                sex = product.sex,
//                adrress = product.adrress,
//                city = product.city,
//                phone_number = product.phone_number,
//                entry_date = product.entry_date, // Use .Value to get the DateTime from DateTime?
//                referencing_doctor_id = product.referencing_doctor_id,
//                diagnosis = product.diagnosis,
//                department_id = product.department_id
//            };
//            ViewData["id"] = id;
//            ViewData["name"] = product.name;
//            ViewData["entry_date"] = product.entry_date;

//            return View(productDto);
//        }

//        [HttpPost]
//        public IActionResult Edit(int id, ProductDto productDto)
//        {
//            var product = context.products.Find(id);
//            if (product == null)
//            {
//                return RedirectToAction("Index", "Product");
//            }

//            if (!ModelState.IsValid)
//            {
//                ViewData["id"] = id;
//                ViewData["name"] = product.name;
//                ViewData["entry_date"] = product.entry_date;

//                return View(productDto);
//            }

//            // Update product properties
//            product.name = productDto.name;
//            product.age = productDto.age;
//            product.sex = productDto.sex;
//            product.adrress = productDto.adrress;
//            product.city = productDto.city;
//            product.phone_number = productDto.phone_number;
//            product.entry_date = productDto.entry_date.Value; // Make sure to handle nullable DateTime
//            product.referencing_doctor_id = productDto.referencing_doctor_id;
//            product.diagnosis = productDto.diagnosis;
//            product.department_id = productDto.department_id;

//            context.SaveChanges();

//            return RedirectToAction("Index", "Product"); // Add this return statement
//        }

//        //[HttpPost]
//        //public IActionResult Edit(int id, ProductDto productDto)
//        //{
//        //    var product = context.products.Find(id);
//        //    if (product == null)
//        //    {
//        //        return RedirectToAction("Index", "Product");
//        //    }
//        //    if (!ModelState.IsValid)
//        //    {
//        //        ViewData["id"] = id;
//        //        ViewData["name"] = product.name;
//        //        ViewData["entry_date"] = product.entry_date;

//        //        return View(productDto);
//        //    }
//        //    if (productDto.name != null)
//        //    {
//        //        product.name = productDto.name;
//        //        product.age = productDto.age;
//        //       product.sex= productDto.sex; 
//        //        product.adrress= productDto.adrress;
//        //        product.city= productDto.city;
//        //        product.phone_number= productDto.phone_number;
//        //        product.referencing_doctor_id=productDto.referencing_doctor_id;
//        //        product.diagnosis= productDto.diagnosis;
//        //        product.department_id= productDto.department_id;
//        //        context.SaveChanges();
//        //    }

//        //}
//    }

//}


using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using MVCStore.Models;
using MVCStore.Services;

namespace MVCStore.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDBContext context;

        public ProductController(ApplicationDBContext context, IWebHostEnvironment environment)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            var products = context.products.OrderByDescending(p => p.Id).ToList();

            // Display success/error messages if they exist
            if (TempData["Message"] != null)
            {
                ViewBag.Message = TempData["Message"];
            }
            if (TempData["Error"] != null)
            {
                ViewBag.Error = TempData["Error"];
            }

            return View(products);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(ProductDto productDto)
        {
            if (productDto.phone_number == null)
            {
                ModelState.AddModelError("phone_number", "Phone Number is required");
            }

            if (!ModelState.IsValid)
            {
                return View(productDto);
            }

            try
            {
                var product = new Product
                {
                    name = productDto.name,
                    age = productDto.age,
                    sex = productDto.sex,
                    adrress = productDto.adrress,
                    city = productDto.city,
                    phone_number = productDto.phone_number,
                    entry_date = productDto.entry_date.Value,
                    referencing_doctor_id = productDto.referencing_doctor_id,
                    diagnosis = productDto.diagnosis,
                    department_id = productDto.department_id
                };

                context.products.Add(product);
                context.SaveChanges();

                TempData["Message"] = "Patient created successfully";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Failed to create patient: " + ex.Message;
                return View(productDto);
            }
        }

        public IActionResult Edit(int id)
        {
            var product = context.products.Find(id);
            if (product == null)
            {
                TempData["Error"] = "Patient not found";
                return RedirectToAction("Index");
            }

            var productDto = new ProductDto
            {
                name = product.name,
                age = product.age,
                sex = product.sex,
                adrress = product.adrress,
                city = product.city,
                phone_number = product.phone_number,
                entry_date = product.entry_date,
                referencing_doctor_id = product.referencing_doctor_id,
                diagnosis = product.diagnosis,
                department_id = product.department_id
            };

            ViewData["id"] = id;
            return View(productDto);
        }

        [HttpPost]
        public IActionResult Edit(int id, ProductDto productDto)
        {
            var product = context.products.Find(id);
            if (product == null)
            {
                TempData["Error"] = "Patient not found";
                return RedirectToAction("Index");
            }

            if (!ModelState.IsValid)
            {
                ViewData["id"] = id;
                return View(productDto);
            }

            try
            {
                product.name = productDto.name;
                product.age = productDto.age;
                product.sex = productDto.sex;
                product.adrress = productDto.adrress;
                product.city = productDto.city;
                product.phone_number = productDto.phone_number;
                product.entry_date = productDto.entry_date.Value;
                product.referencing_doctor_id = productDto.referencing_doctor_id;
                product.diagnosis = productDto.diagnosis;
                product.department_id = productDto.department_id;

                context.SaveChanges();

                TempData["Message"] = "Patient updated successfully";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Failed to update patient: " + ex.Message;
                ViewData["id"] = id;
                return View(productDto);
            }
            
        }
        //public IActionResult Delete(int id)
        //{
        //    var product = context.products.Find(id);
        //    if (product == null)
        //    {

        //        return RedirectToAction("Index");
        //    }
        //    context.products.Remove(product);
        //    context.SaveChanges();
        //    return View(product);
        //}

        // GET: Product/Delete/5
        //public IActionResult Delete(int id)
        //{
        //    var product = context.products.Find(id);
        //    if (product == null)
        //    {
        //        return RedirectToAction("Index");
        //    }

        //    var productDto = new ProductDto
        //    {
        //        Id = product.Id,

        //         name = product.name,
        //         age = product.age,
        //        sex = product.sex,
        //        adrress = product.adrress,
        //        city = product.city,
        //        phone_number = product.phone_number,
        //        entry_date = product.entry_date,
        //        referencing_doctor_id = product.referencing_doctor_id,
        //       diagnosis = product.diagnosis,
        //        department_id = product.department_id,


        //    };

        //    return View(productDto);
        //}

        //// POST: Product/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //[ActionName("Delete")]
        //public IActionResult DeleteConfirmed(int id)
        //{
        //    var product = context.products.Find(id);
        //    if (product != null)
        //    {
        //        context.products.Remove(product);
        //        context.SaveChanges();
        //        TempData["Message"] = "Patient record deleted successfully";
        //    }
        //    return RedirectToAction("Index");
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            try
            {
                var product = context.products.Find(id);
                if (product != null)
                {
                    context.products.Remove(product);
                    context.SaveChanges();
                    TempData["Message"] = "Patient deleted successfully";
                }
                return RedirectToAction("Index");
            }
            catch
            {
                TempData["Error"] = "Error deleting patient";
                return RedirectToAction("Index");
            }
        }


    }
}