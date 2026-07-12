//01110001
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace IoTAssetManagement2.Models.Entities;

[Index("SerialNumber", Name = "UN_Devices_SerialNumber", IsUnique = true)]
public partial class Devices
{
    [Key]
    [Column("DeviceID")]
    public int DeviceId { get; set; }

    [StringLength(100)]
    public string DeviceName { get; set; } = null!;

    [StringLength(100)]
    public string SerialNumber { get; set; } = null!;

    [Column("DeviceTypeID")]
    public int DeviceTypeId { get; set; }

    [Column("FirmwareID")]
    public int FirmwareId { get; set; }

    [Column("GroupID")]
    public int GroupId { get; set; }

    [ForeignKey("DeviceTypeId")]
    [InverseProperty("Devices")]
    public virtual DeviceTypes DeviceType { get; set; } = null!;

    [ForeignKey("FirmwareId")]
    [InverseProperty("Devices")]
    public virtual Firmware Firmware { get; set; } = null!;

    [ForeignKey("GroupId")]
    [InverseProperty("Devices")]
    public virtual Groups Group { get; set; } = null!;
}