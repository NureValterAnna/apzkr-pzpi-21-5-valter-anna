using Application.Interfaces;
using Domain.Structs;
using Platform.ViewModels;

namespace Platform.Pages;

public partial class PrescriptionsPage : ContentPage, IGuardedPage
{
    public PrescriptionsPage(PrescriptionPageVM viewModel)
    {
        InitializeComponent();

        PrescriptionsHeader.Text = Domain.Resources.Resource.Perscriptions;

        BindingContext = viewModel;
    }

    public IEnumerable<Guard> Guards => new[] { Guard.AuthenticatedOnly };
}