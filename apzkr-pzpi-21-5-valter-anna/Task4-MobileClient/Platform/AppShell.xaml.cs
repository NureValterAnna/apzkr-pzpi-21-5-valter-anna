using Platform.Pages;
using Platform.ViewModels;

namespace Platform;

public partial class AppShell : Shell
{
    public AppShell(AppShellVM viewModel, LoginPageVM loginPageVM, PrescriptionPageVM prescriptionPageVM)
    {
        InitializeComponent();

        BindingContext = viewModel;
        
        AddItems(loginPageVM, prescriptionPageVM);
    }

    private void AddItems(LoginPageVM loginPageVM, PrescriptionPageVM prescriptionPageVM)
    {
        Items.Clear();
        
        var flyoutItem = new FlyoutItem();
        flyoutItem.Items.Add(new LoginPage(loginPageVM));
        flyoutItem.Title = "Login";
        flyoutItem.SetBinding(FlyoutItem.IsVisibleProperty, nameof(AppShellVM.IsLoggedOut));
        Items.Add(flyoutItem);
        
        flyoutItem = new FlyoutItem();
        flyoutItem.Items.Add(new PrescriptionsPage(prescriptionPageVM));
        flyoutItem.Title = "MediSync";
        flyoutItem.SetBinding(FlyoutItem.IsVisibleProperty, nameof(AppShellVM.IsAuthenticated));
        Items.Add(flyoutItem);
    }
}