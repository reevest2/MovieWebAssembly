using System.ComponentModel.DataAnnotations;

namespace Models.Identity;

public class AuthenticationDTO
{
    [Required(ErrorMessage = "UserName is Required")]
    [RegularExpression("^[a-zA-Z0-9_.-]+@[a-zA-Z0-9-]+.[[a-zA-Z0-9.]+$", ErrorMessage = "Invalid Email")]
    public string UserName { get; set; }
    [Required(ErrorMessage = "Password is Required")]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}