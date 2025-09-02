


using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Product
{
    public int Id { get; set; }

    [Required, MaxLength(100)]
    [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Name can only contain letters and spaces")]
    public string name { get; set; } = "";

    [Required, Range(0, 150)]
    public int? age { get; set; }

    [Required, MaxLength(10)]
    public string sex { get; set; } = "";

    [Required, MaxLength(200)]
    public string adrress { get; set; } = "";   // (typo kept to match your DB)

    [Required, MaxLength(100)]
    public string city { get; set; } = "";

    [Required]
    [RegularExpression(@"^\+?\d{10,15}$", ErrorMessage = "Enter a valid phone number")]
    public string phone_number { get; set; } = "";

    [Required, DataType(DataType.Date)]
    public DateTime? entry_date { get; set; }

    [Required]
    public int referencing_doctor_id { get; set; }

    [Required]
    public string RefDr { get; set; } = "";

    [Required, MaxLength(200)]
    public string diagnosis { get; set; } = "";

    public int? department_id { get; set; }

    // ---- Room (FK) ----
    public int? Room_id { get; set; }     // make nullable so selection is optional
    [ForeignKey(nameof(Room_id))]
    public Room? Room { get; set; }
}

