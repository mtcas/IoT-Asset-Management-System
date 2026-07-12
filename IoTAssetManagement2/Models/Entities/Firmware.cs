using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace IoTAssetManagement2.Models.Entities;

[Index("Version", Name = "UN_Firmware_Version", IsUnique = true)]
public partial class Firmware
{
    [Key]
    [Column("FirmwareID")]
    public int FirmwareId { get; set; }

    [StringLength(20)]
    public string Version { get; set; } = null!;

    public DateOnly ReleaseDate { get; set; }

    [StringLength(250)]
    public string? Description { get; set; }

    [InverseProperty("Firmware")]
    public virtual ICollection<Devices> Devices { get; set; } = new List<Devices>();
}
