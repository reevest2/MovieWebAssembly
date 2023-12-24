using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Data;

[Table("HotelRooms", Schema = "Core")]
public class HotelRoom
{
    [Key]
    public int Id { get; set; }
    [Required(ErrorMessage = "Please enter a room Name")]
    public string Name { get; set; }
    [Required(ErrorMessage = "Please enter Occupancy")]
    public int Occupancy { get; set; }
    [Range(1,3000,ErrorMessage = "Regular Rate must be between 1 and 3000")]
    public double RegularRate { get; set; }
    public string Details { get; set; }
    public string SqFt { get; set; }
    public string CreatedBy { get; set; } = "Admin";
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    public string UpdatedBy { get; set; } = "Admin";
    public DateTime UpdatedDate { get; set; } = DateTime.Now;
}