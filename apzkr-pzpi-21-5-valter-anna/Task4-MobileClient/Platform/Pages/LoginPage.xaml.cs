using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Structs;
using Platform.ViewModels;

namespace Platform.Pages;

public partial class LoginPage : ContentPage
{
    public LoginPage(LoginPageVM viewModel)
    {
        InitializeComponent();
        
        BindingContext = viewModel;
    }
}