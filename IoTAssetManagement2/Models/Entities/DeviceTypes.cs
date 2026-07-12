using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace IoTAssetManagement2.Models.Entities;

public partial class DeviceTypes
{
    [Key]
    [Column("DeviceTypeID")]
    public int DeviceTypeId { get; set; }

    [StringLength(100)]
    public string Name { get; set; } = null!;

    [StringLength(250)]
    public string? Description { get; set; }

    [StringLength(250)]
    public string? SupportedFeatures { get; set; }

    [InverseProperty("DeviceType")]
    public virtual ICollection<Devices> Devices { get; set; } = new List<Devices>();
}
