using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QRCodeEvidentationApp.Models;

public partial class Room
{
    [Key]
    public string Name { get; set; } = null!;

    public long? Capacity { get; set; }

    public string? EquipmentDescription { get; set; }

    public string? LocationDescription { get; set; }

    public string? Type { get; set; }

    public virtual ICollection<Lecture> Lectures { get; set; } = new List<Lecture>();

}
