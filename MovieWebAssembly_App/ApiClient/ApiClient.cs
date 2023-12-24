using System.Text.Json;
using DataAccess.Data;

namespace MovieWebAssembly_App.ApiClient;

public class ApiClient
{
    public async Task<List<HotelRoom>> GetHotelRooms()
    {
        var httpClient = new HttpClient();

        var response = await httpClient.GetAsync("https://localhost:7282/api/HotelRoom");
        var responseString = await response.Content.ReadAsStringAsync();
        var hotelRooms = JsonSerializer.Deserialize<List<HotelRoom>>(responseString);

        return hotelRooms;
    }
}