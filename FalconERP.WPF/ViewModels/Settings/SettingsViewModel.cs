using FalconERP.Application.Interfaces;
using FalconERP.Domain.Entities;
using FalconERP.WPF.Commands;
using FalconERP.WPF.ViewModels.Shared;
using System.Collections.ObjectModel;
using System.Drawing.Printing;
using System.Windows;
using System.Windows.Input;

namespace FalconERP.WPF.ViewModels.Settings;

public class SettingsViewModel : BaseViewModel
{
    private readonly ISystemSettingsRepository _repository;

    private SystemSettings _settings = new();

    public ObservableCollection<string> Printers { get; } = new();

    public ICommand SaveCommand { get; }

    public SettingsViewModel(
        ISystemSettingsRepository repository)
    {
        _repository = repository;

        SaveCommand = new RelayCommand(Save);

        Load();

        LoadPrinters();
    }

    #region Store Information

    public string StoreName
    {
        get => _settings.StoreName;
        set
        {
            _settings.StoreName = value;
            OnPropertyChanged();
        }
    }

    public string OwnerName
    {
        get => _settings.OwnerName;
        set
        {
            _settings.OwnerName = value;
            OnPropertyChanged();
        }
    }

    public string Phone
    {
        get => _settings.Phone;
        set
        {
            _settings.Phone = value;
            OnPropertyChanged();
        }
    }

    public string Email
    {
        get => _settings.Email;
        set
        {
            _settings.Email = value;
            OnPropertyChanged();
        }
    }

    public string Address
    {
        get => _settings.Address;
        set
        {
            _settings.Address = value;
            OnPropertyChanged();
        }
    }

    public string Currency
    {
        get => _settings.Currency;
        set
        {
            _settings.Currency = value;
            OnPropertyChanged();
        }
    }

    public string ReceiptFooter
    {
        get => _settings.ReceiptFooter;
        set
        {
            _settings.ReceiptFooter = value;
            OnPropertyChanged();
        }
    }

    #endregion

    #region Printer Settings

    public string PrinterName
    {
        get => _settings.PrinterName;
        set
        {
            _settings.PrinterName = value;
            OnPropertyChanged();
        }
    }

    public int PaperWidth
    {
        get => _settings.PaperWidth;
        set
        {
            _settings.PaperWidth = value;
            OnPropertyChanged();
        }
    }

    public bool AutoPrintReceipt
    {
        get => _settings.AutoPrintReceipt;
        set
        {
            _settings.AutoPrintReceipt = value;
            OnPropertyChanged();
        }
    }

    #endregion

    private void Load()
    {
        _settings = _repository
            .GetAsync()
            .GetAwaiter()
            .GetResult() ?? new SystemSettings();

        OnPropertyChanged(nameof(StoreName));
        OnPropertyChanged(nameof(OwnerName));
        OnPropertyChanged(nameof(Phone));
        OnPropertyChanged(nameof(Email));
        OnPropertyChanged(nameof(Address));
        OnPropertyChanged(nameof(Currency));
        OnPropertyChanged(nameof(ReceiptFooter));
        OnPropertyChanged(nameof(PrinterName));
        OnPropertyChanged(nameof(PaperWidth));
        OnPropertyChanged(nameof(AutoPrintReceipt));
    }

    private void LoadPrinters()
    {
        Printers.Clear();

        foreach (string printer in PrinterSettings.InstalledPrinters)
        {
            Printers.Add(printer);
        }

        if (Printers.Count > 0 &&
            string.IsNullOrWhiteSpace(PrinterName))
        {
            PrinterName = Printers[0];
        }
    }

    private void Save(object? parameter)
    {
        _repository
            .SaveAsync(_settings)
            .GetAwaiter()
            .GetResult();

        MessageBox.Show(
            "تم حفظ الإعدادات بنجاح.",
            "Falcon ERP",
            MessageBoxButton.OK,
            MessageBoxImage.Information);
    }
}