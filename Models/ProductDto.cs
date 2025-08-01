using System.ComponentModel.DataAnnotations;

namespace MVCStore.Models
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string name { get; set; } = "";
        [Required, MaxLength(100)]
        public string age { get; set; } = "";
        [Required, MaxLength(100)]
        public string sex { get; set; } = "";
        [Required, MaxLength(100)]
        public string adrress { get; set; } = "";
        [Required, MaxLength(100)]
        public string city { get; set; } = "";
        [Required, MaxLength(100)]
        public string phone_number { get; set; } = "";
        [Required(ErrorMessage = "Date is required")]
        [DataType(DataType.Date)]
        public DateTime? entry_date { get; set; }





        [Required, MaxLength(100)]
        public string referencing_doctor_id { get; set; } = "";
        [Required, MaxLength(100)]
        public string diagnosis { get; set; } = "";
        [Required, MaxLength(100)]


        public string department_id { get; set; } = "";


    }
}
