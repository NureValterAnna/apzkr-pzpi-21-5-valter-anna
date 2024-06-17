using System.Windows.Input;
using Application.Interfaces;
using Application.Users.Commands;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Platform.ViewModels;

public partial class LoginPageVM : ObservableObject
{
    private readonly IUserService _userService;

    [ObservableProperty]
    private string _email = "";
    
    [ObservableProperty]
    private string _password = "";
    
    public ICommand LoginCommand { get; set; }
    
    public LoginPageVM(IUserService userService)
    {
        _userService = userService;
        
        LoginCommand = new AsyncRelayCommand(LoginAsync);
    }
    
    private async Task LoginAsync(CancellationToken cancellationToken)
    {
        var loginCommand = new LoginCommand
        {
            Email = Email,
            Password = Password
        };
        
        await _userService.Login(loginCommand);
    }
}