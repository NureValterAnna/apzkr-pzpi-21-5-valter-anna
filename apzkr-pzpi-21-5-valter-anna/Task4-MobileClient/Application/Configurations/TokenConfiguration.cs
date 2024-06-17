using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Application.Configurations;

public class TokenConfiguration : INotifyPropertyChanged
{
    private string? _token;
    public string? Token 
    {
        get => _token;
        set
        {
            _token = value;
            PropertyChanged?.Invoke(null, new PropertyChangedEventArgs(nameof(Token)));
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;
}