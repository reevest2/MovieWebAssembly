using System.ComponentModel.DataAnnotations;

namespace Models.Identity;

public class UserRequestDTO
{
    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; }
    [Required(ErrorMessage = "Email is Required")]
    [RegularExpression("^[a-zA-Z0-9_.-]+@[a-zA-Z0-9-]+.[[a-zA-Z0-9.]+$", ErrorMessage = "Invalid Email")]
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    [Required(ErrorMessage = "Password is Required")]
    [DataType(DataType.Password)]
    public string Password { get; set; }
    [Required(ErrorMessage = "Password is Required")]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "Password and confirm password do not match")]
    public string ConfirmPassword { get; set; }
}