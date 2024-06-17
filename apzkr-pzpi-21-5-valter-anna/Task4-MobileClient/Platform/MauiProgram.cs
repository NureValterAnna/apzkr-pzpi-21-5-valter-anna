using Application.Configurations;
using Application.Interfaces;
using CommunityToolkit.Maui;
using Infrastructure;
using Platform.Pages;
using Platform.ViewModels;
using SkiaSharp.Views.Maui.Controls.Hosting;

namespace Platform;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .UseSkiaSharp(true)
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        builder.Services.RegisterPages();
        builder.Services.RegisterConfigurations();
        builder.Services.AddInfrastructureServices();

        return builder.Build();
    }
    
    private static IServiceCollection RegisterPages(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<AppShell>();
        serviceCollection.AddScoped<AppShellVM>();
        serviceCollection.AddScoped<LoginPage>();
        serviceCollection.AddScoped<LoginPageVM>();
        serviceCollection.AddScoped<IGuardedPage, PrescriptionsPage>();
        serviceCollection.AddScoped<PrescriptionPageVM>();
        
        return serviceCollection;
    }
    
    private static IServiceCollection RegisterConfigurations(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton(_ =>
        {
            return new TokenConfiguration()
            {
                Token = Preferences.Get("access_token", null)
            };
        });
        
        return serviceCollection;
    }
}