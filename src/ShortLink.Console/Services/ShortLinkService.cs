using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
namespace ShortLink.ConsoleApp.Services;
//https://vk.com
public class ShortLinkService
{
    private readonly HttpClient _httpClient;
    public ShortLinkService(string baseAddress)
    {
        _httpClient = new HttpClient()
        {
            BaseAddress = new Uri($"{baseAddress}")
        };
    }

    ~ShortLinkService()
    {
        _httpClient.Dispose();
    }

    public async Task<UrlMapperDTO?> GetLinkByGuidAsync(string guid)
    {
        try
        {
            var response = await _httpClient.GetAsync($"/api/ShortLink?guid={guid}");
            response.EnsureSuccessStatusCode();

            var urlMapper = await response.Content.ReadFromJsonAsync<UrlMapperDTO>();
            return urlMapper;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return null;
        }
    }

    public async Task<UrlMapperDTO?> PostLinkAsync(string url)
    {
        try
        {
            var uri = new Uri(url);
            string json = JsonSerializer.Serialize(new
            {
                link = uri.AbsoluteUri
            }); // https://localhost:5200/api/shortlink?link=https://vk.com
            using var content = new StringContent(uri.AbsoluteUri, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"/api/ShortLink", content);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<UrlMapperDTO>();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return null;
        }
    }
}
