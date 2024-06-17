using System.Net.Http.Json;
using Application.Configurations;
using Application.Interfaces;
using Application.Users.Commands;
using Domain.Entities;

namespace Infrastructure.Services;

public class UserService : IUserService
{
    private readonly TokenConfiguration _tokenConfiguration;

    public UserService(TokenConfiguration tokenConfiguration)
    {
        _tokenConfiguration = tokenConfiguration;
    }

    public async Task Login(LoginCommand request)
    {
        var httpClient = new HttpClient()
        {
            BaseAddress = new Uri("http://10.0.2.2:8080/api/")
        };

        var httpRequestMessage = new HttpRequestMessage()
        {
            Content = JsonContent.Create(request),
            Method = HttpMethod.Post,
            RequestUri = new Uri("account/login", UriKind.Relative)
        };
        
        var response = await httpClient.SendAsync(httpRequestMessage);
        var auth = await response.Content.ReadFromJsonAsync<Auth>();
        _tokenConfiguration.Token = auth.Token;
    }
}