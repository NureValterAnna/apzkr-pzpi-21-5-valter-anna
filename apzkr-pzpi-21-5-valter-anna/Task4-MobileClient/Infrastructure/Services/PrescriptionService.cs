using System.Net.Http.Headers;
using System.Net.Http.Json;
using Application.Configurations;
using Application.Interfaces;
using Application.Prescriptions.Queries;
using Domain.Entities;

namespace Infrastructure.Services;

public class PrescriptionService : IPrescriptionService
{
    private readonly TokenConfiguration _tokenConfiguration;

    public PrescriptionService(TokenConfiguration tokenConfiguration)
    {
        _tokenConfiguration = tokenConfiguration;
    }

    public async Task<List<Prescription>> GetPrescriptionsByEmailAsync()
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
            RequestUri = new Uri("Prescription", UriKind.Relative)
        };
        
        var response = await httpClient.SendAsync(httpRequestMessage);
        if (!response.IsSuccessStatusCode)
        {
            _tokenConfiguration.Token = null;
            return default;
        }
        
        var prescriptions = await response.Content.ReadFromJsonAsync<List<Prescription>>();
        return prescriptions;
    }
}