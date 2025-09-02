


using System;
using System.ComponentModel.DataAnnotations;

public class ProductDto
{
    public int Id { get; set; }

    [Required, MaxLength(100)]
    [RegularExpression(@"^[a-zA-Z\s]+$")]
    public string name { get; set; } = "";

    [Required, Range(0, 150)]
    public int? age { get; set; }

    [Required, MaxLength(10)]
    public string sex { get; set; } = "";

    [Required, MaxLength(200)]
    public string address { get; set; } = "";

    [Required, MaxLength(100)]
    public string city { get; set; } = "";

    [Required]
    [RegularExpression(@"^\+?\d{10,15}$")]
    public string phone_number { get; set; } = "";

    [Required, DataType(DataType.Date)]
    public DateTime? entry_date { get; set; }

    [Required]
    public int referencing_doctor_id { get; set; }

    public string RefDr { get; set; } = "";

    [Required, MaxLength(200)]
    public string diagnosis { get; set; } = "";

    public int? department_id { get; set; }

    // Room coming from modal
    [Required(ErrorMessage = "Please pick a room")]
    public int? Room_id { get; set; }
}
