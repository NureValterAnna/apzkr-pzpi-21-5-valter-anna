using System.Net.Http.Headers;
using System.Net.Http.Json;
using Application.Configurations;
using Application.Interfaces;
using Domain.Entities;

namespace Infrastructure.Services;

public class MedicineStockService : IMedicineStockService
{
    private readonly TokenConfiguration _tokenConfiguration;

    public MedicineStockService(TokenConfiguration tokenConfiguration)
    {
        _tokenConfiguration = tokenConfiguration;
    }

    public async Task<List<MedicineStock>> GetAllMedicineStocksAsync()
    {
        var httpClient = new HttpClient()
        {
            BaseAddress = new Uri("http://10.0.2.2:8080/api/"),
            DefaultRequestHeaders =
            {
                Authorization = new AuthenticationHeaderValue("Bearer", _tokenConfiguration.Token)
            }
        };

        var httpRequestMessage = new HttpRequestMessage()
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri("stock", UriKind.Relative)
        };
        
        var response = await httpClient.SendAsync(httpRequestMessage);
        if (!response.IsSuccessStatusCode)
        {
            _tokenConfiguration.Token = null;
            return default;
        }
        
        var prescriptions = await response.Content.ReadFromJsonAsync<List<MedicineStock>>();
        return prescriptions;
    }
}