using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class Room
{
    [Key]
    public int Room_id { get; set; }   // PK

    [Required, MaxLength(20)]
    public string RoomNo { get; set; } = "";

    [Required, MaxLength(10)]
    public string BedNo { get; set; } = "";

    [Required, MaxLength(20)]
    public string Floor { get; set; } = "";

    // mark if someone is already assigned (so we can filter "available")
    public bool IsOccupied { get; set; } = false;

    public ICollection<Product> Products { get; set; } = new List<Product>();
}

