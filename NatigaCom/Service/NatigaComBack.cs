using DataShare;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text;

namespace NatigaCom.Service
{
    
    public interface INatigaComBack
    {
        public Task<List<BackToFront>> GetNatiga(FrontToBack input);
    }
    public class NatigaComBack : INatigaComBack
    {
        private readonly HttpClient _httpClient;
        public NatigaComBack(HttpClient httpClient)
        {
            _httpClient=httpClient;
        }
        public async Task<List<BackToFront>> GetNatiga(FrontToBack? input)
        {
            var Natiga = new StringContent(JsonSerializer.Serialize(input), Encoding.UTF8, "application/json");
            // var result = await _httpClient.GetFromJsonAsync<List<BackToFront>>($"api/Natiga/{Natiga}");
            var data = await _httpClient.PostAsync("Natiga", Natiga);
            var result = await JsonSerializer.DeserializeAsync<List<BackToFront>>(data.Content.ReadAsStream(), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            return result;
        }
    }
}
