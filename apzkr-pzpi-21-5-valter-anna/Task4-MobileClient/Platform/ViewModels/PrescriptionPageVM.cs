using System.Collections.ObjectModel;
using System.Windows.Input;
using Application.Configurations;
using Application.Interfaces;
using Application.MedicineIntake.Commands;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Domain.Entities;

namespace Platform.ViewModels;

public partial class PrescriptionPageVM : ObservableObject
{
    private readonly IPrescriptionService _prescriptionService;
    private readonly IMedicineStockService _medicineStockService;
    private readonly IMedicineIntakeService _medicineIntakeService;
    private readonly TokenConfiguration _tokenConfiguration;

    [ObservableProperty]
    private ObservableCollection<Prescription> _prescriptions = [];
    
    [ObservableProperty]
    private Prescription? _selectedPrescription;
    
    [ObservableProperty]
    private MedicineStock? _selectedStock;
    
    [ObservableProperty]
    private ObservableCollection<MedicineStock> _medicineStocks = [];
    
    public ICommand Init { get; set; }
    
    public ICommand GetPrescriptions { get; set; }
    
    public ICommand SelectPrescription { get; set; }
    
    public ICommand SelectStock { get; set; }
    
    public ICommand GetBack { get; set; }
    
    public ICommand MedicineIntake { get; set; }
    
    public ICommand Logout { get; set; }
    
    public PrescriptionPageVM(IPrescriptionService prescriptionService, IMedicineStockService medicineStockService, IMedicineIntakeService medicineIntakeService, TokenConfiguration tokenConfiguration)
    {
        _prescriptionService = prescriptionService;
        _medicineStockService = medicineStockService;
        _medicineIntakeService = medicineIntakeService;
        _tokenConfiguration = tokenConfiguration;
        RegisterCommands();
    }
    
    partial void OnSelectedPrescriptionChanged(Prescription? oldValue, Prescription? newValue)
    {
        OnPropertyChanged(nameof(PrescriptionSelected));
        OnPropertyChanged(nameof(PrescriptionNotSelected));
    }

    private void RegisterCommands()
    {
        Init = new AsyncRelayCommand(InitAsync);
        GetPrescriptions = new AsyncRelayCommand(GetPrescriptionsAsync);
        SelectPrescription = new AsyncRelayCommand<SelectedItemChangedEventArgs>(SelectPrescriptionAsync);
        SelectStock = new AsyncRelayCommand<SelectedItemChangedEventArgs>(SelectDispenserAsync);
        GetBack = new AsyncRelayCommand(GetBackAsync);
        MedicineIntake = new AsyncRelayCommand(MedicineIntakeAsync);
        Logout = new AsyncRelayCommand(LogoutAsync);
    }
    
    protected virtual Task InitAsync()
    {
        return GetPrescriptionsAsync();
    }

    public bool PrescriptionSelected => SelectedPrescription is not null;

    public bool PrescriptionNotSelected => SelectedPrescription is null;
    
    private async Task GetPrescriptionsAsync()
    {
        var prescriptions = await _prescriptionService.GetPrescriptionsByEmailAsync();
        Prescriptions.Clear();

        foreach (var prescription in prescriptions)
        {
            Prescriptions.Add(prescription);
        }
    }
    
    protected virtual async Task SelectPrescriptionAsync(SelectedItemChangedEventArgs? args)
    {
        var prescription = args?.SelectedItem as Prescription;
        
        if (prescription is null)
        {
            return;
        }
        
        SelectedPrescription = args?.SelectedItem as Prescription;
        var medicineStocks = await _medicineStockService.GetAllMedicineStocksAsync();
        MedicineStocks = new ObservableCollection<MedicineStock>(medicineStocks
            .Where(ms => ms.MedicineId == SelectedPrescription.MedicineId && ms.Quantity >= SelectedPrescription.Dose)
            .ToList());

    }
    
    protected virtual async Task SelectDispenserAsync(SelectedItemChangedEventArgs? args)
    {
        var medicineStock = args?.SelectedItem as MedicineStock;
        
        if (medicineStock is null)
        {
            return;
        }
        
        SelectedStock = medicineStock;
    }
    
    private async Task GetBackAsync()
    {
        SelectedPrescription = null;
    }
    
    private async Task MedicineIntakeAsync()
    {
        if (SelectedStock is null)
        {
            return;
        }
        
        var request = new MedicineIntakeCommand
        {
            PrescriptionId = SelectedPrescription!.Id,
            DispenserId = SelectedStock!.DispenserId,
        };
        
        await _medicineIntakeService.MedicineIntake(request);
        SelectedPrescription = null;
    }

    private Task LogoutAsync()
    {
        Preferences.Remove("access_token");
        _tokenConfiguration.Token = null;
        return Task.CompletedTask;
    }
}