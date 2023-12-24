using System.ComponentModel.DataAnnotations;

namespace DataAccess.Data.Abstractions;

public abstract class ResourceBase
{
    [Key] public int Id { get; set; }
    public string CreatedBy { get; set; } = "Admin";
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    public string UpdatedBy { get; set; } = "Admin";
    public DateTime UpdatedDate { get; set; } = DateTime.Now;
}