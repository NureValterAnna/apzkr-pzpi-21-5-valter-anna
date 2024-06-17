using System.ComponentModel;
using Application.Configurations;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Platform.ViewModels;

public partial class AppShellVM : ObservableObject
{
    private readonly TokenConfiguration _tokenConfiguration;
    [ObservableProperty]
    private bool _isAuthenticated;
    [ObservableProperty]
    private bool _isLoggedOut;

    public AppShellVM(TokenConfiguration tokenConfiguration)
    {
        _tokenConfiguration = tokenConfiguration;
        _tokenConfiguration.PropertyChanged += TokenChanged;
        IsAuthenticated = _tokenConfiguration.Token is not null;
        IsLoggedOut = _tokenConfiguration.Token is null;
    }
    
    private void TokenChanged(object? sender, PropertyChangedEventArgs args)
    {
        if (args.PropertyName == nameof(TokenConfiguration.Token))
        {
            IsAuthenticated = _tokenConfiguration.Token is not null;
            IsLoggedOut = _tokenConfiguration.Token is null;
            Preferences.Set("access_token", _tokenConfiguration.Token);
        }
    }
}