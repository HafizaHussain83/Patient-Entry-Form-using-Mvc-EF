using System.ComponentModel.DataAnnotations;

namespace MVCStore.Models
{
    public class Product
    {
        public int Id { get; set; }
        [MaxLength(100)]    
        public string name { get; set; } = "";
        [MaxLength(100)]
        public string age { get; set; } = "";
        [MaxLength(100)]
        public string sex { get; set; } = "";
        [MaxLength(100)]
        public string adrress { get; set; } = "";
        [MaxLength(100)]
        public string city { get; set; } = "";
        [MaxLength(100)]
        public string phone_number { get; set; } = "";
        [MaxLength(100)]
        public DateTime entry_date { get; set; }



        [MaxLength(100)]
        public string referencing_doctor_id { get; set; } = "";
        [MaxLength(100)]
        public string diagnosis { get; set; } = "";
        [MaxLength(100)]


        public string department_id { get; set; } = "";
       




    }
} 
