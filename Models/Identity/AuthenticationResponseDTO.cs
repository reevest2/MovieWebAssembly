namespace Models.Identity;

public class AuthenticationResponseDTO : GenericResponseDTO
{
    public string Token { get; set; }
    public UserDTO UserDTO { get; set; }
}