using FalconERP.Application.DTOs;
using FalconERP.Application.Interfaces;
using FalconERP.WPF.ViewModels.Shared;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FalconERP.WPF.ViewModels.Invoices;

public class InvoiceDetailsViewModel : BaseViewModel
{
    private readonly ISaleRepository _saleRepository;

    public SaleDetailsDto? Invoice { get; set; }

    public ObservableCollection<SaleItemDetailsDto> Items
    { get; set; } = new();

    public InvoiceDetailsViewModel(
        ISaleRepository saleRepository,
        int saleId)
    {
        _saleRepository = saleRepository;

        LoadInvoice(saleId);
    }

    private void LoadInvoice(int saleId)
    {
        Invoice = _saleRepository
            .GetDetailsAsync(saleId)
            .GetAwaiter()
            .GetResult();

        if (Invoice == null)
            return;

        Items.Clear();

        foreach (var item in Invoice.Items)
        {
            Items.Add(item);
        }

        OnPropertyChanged(nameof(Invoice));
    }

}