using ShopTARge24.Core.Dto;
using ShopTARge24.Core.ServiceInterface;
using System.Text.Json;

namespace ShopTARge24.ApplicationServices.Services
{
    public class WeatherForecastServices : IWeatherForecastServices
    {
        public async Task<AcculocationWeatherResultDto> AccuWeatherResult(AcculocationWeatherResultDto dto)
        {

            string apiKey = "";

            var response = $"http://dataservice.accuweather.com/locations/v1/cities/search?apikey={apiKey}&q={dto.CityName}";

            using (var client = new HttpClient())
            {
                var httpResponse = await client.GetAsync(response);

                string json = await httpResponse.Content.ReadAsStringAsync();

                // Tallinna linna kood on 127964
                List<AccuLocationRootDto> weatherData =
                JsonSerializer.Deserialize<List<AccuLocationRootDto>>(json);

                //// Fill your result DTO from the API result
                //dto.LocationKey = weatherData[0].Key;            // e.g., "127964"
                //dto.CityName = weatherData[0].LocalizedName;  // e.g., "Tallinn"
            }

            return dto;

        }
    }
}
