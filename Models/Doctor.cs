using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("ALL_DOCTORS")]
public class Doctor
{
    [Key]
    [Column("doctor_id")]
    public int DoctorId { get; set; }  // ✅ int now

    [Column("department_id")]
    public int? DepartmentId { get; set; }

    [Column("doctor_name")]
    [MaxLength(100)]
    public string? DoctorName { get; set; }

    [Column("doctor_type")]
    [MaxLength(50)]
    public string? DoctorType { get; set; }
}


