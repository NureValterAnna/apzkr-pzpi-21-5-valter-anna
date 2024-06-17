using System.Net.Http.Headers;
using System.Net.Http.Json;
using Application.Configurations;
using Application.Interfaces;
using Application.MedicineIntake.Commands;
using Domain.Entities;

namespace Infrastructure.Services;

public class MedicineIntakeService : IMedicineIntakeService
{
    private readonly TokenConfiguration _tokenConfiguration;

    public MedicineIntakeService(TokenConfiguration tokenConfiguration)
    {
        _tokenConfiguration = tokenConfiguration;
    }

    public async Task MedicineIntake(MedicineIntakeCommand request)
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
            Content = JsonContent.Create(request),
            Method = HttpMethod.Get,
            RequestUri = new Uri("medicineIntake", UriKind.Relative)
        };
        
        var response = await httpClient.SendAsync(httpRequestMessage);
        if (!response.IsSuccessStatusCode)
        {
            _tokenConfiguration.Token = null;
        }
    }
}