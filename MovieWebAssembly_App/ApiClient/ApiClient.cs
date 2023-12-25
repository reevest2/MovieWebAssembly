using System.Text.Json;
using DataAccess.Data;
using Models;

namespace MovieWebAssembly_App.ApiClient;

public class ApiClient
{
    
    public async Task<List<HotelRoomDTO>> GetHotelRooms()
    {
        var options = new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true,
        };
        
        var httpClient = new HttpClient();

        var response = await httpClient.GetAsync("https://localhost:7282/api/HotelRoom");
        var responseString = await response.Content.ReadAsStringAsync();
        var hotelRooms = JsonSerializer.Deserialize<List<HotelRoomDTO>>(responseString, options);
        return hotelRooms;
    }
    
    
}