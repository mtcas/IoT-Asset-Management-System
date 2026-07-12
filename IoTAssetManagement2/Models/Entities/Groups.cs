using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace IoTAssetManagement2.Models.Entities;

public partial class Groups
{
    [Key]
    [Column("GroupID")]
    public int GroupId { get; set; }

    [StringLength(100)]
    public string GroupName { get; set; } = null!;

    [Column("ParentGroupID")]
    public int? ParentGroupId { get; set; }

    [StringLength(250)]
    public string? Description { get; set; }

    [InverseProperty("Group")]
    public virtual ICollection<Devices> Devices { get; set; } = new List<Devices>();

    [InverseProperty("ParentGroup")]
    public virtual ICollection<Groups> InverseParentGroup { get; set; } = new List<Groups>();

    [ForeignKey("ParentGroupId")]
    [InverseProperty("InverseParentGroup")]
    public virtual Groups? ParentGroup { get; set; }
}
