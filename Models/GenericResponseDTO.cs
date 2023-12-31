namespace Models;

public class GenericResponseDTO
{
    public bool IsSuccessful { get; set; }
    public IEnumerable<string> Errors { get; set; }
}