using MeteoApp.Models;
using Newtonsoft.Json;

namespace MeteoApp.Services
{
    public static class ApiService
    {
        public static async Task<Root> GetWeatherData(double latitude, double longitude)
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetStringAsync(string.Format("https://api.openweathermap.org/data/2.5/forecast?lat={0}&lon={1}&units=metric&appid=36b65f683451847f0b60287f7d7c2817", latitude, longitude));     
            return JsonConvert.DeserializeObject<Root>(response);    
        }

        public static async Task<Root> GetWeatherByCity(string name)
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetStringAsync(string.Format("https://api.openweathermap.org/data/2.5/forecast?q={0}&units=metric&appid=36b65f683451847f0b60287f7d7c2817", name));
            return JsonConvert.DeserializeObject<Root>(response);
        }
    }
}
