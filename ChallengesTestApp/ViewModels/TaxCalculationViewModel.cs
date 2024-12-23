using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using ChallengesTestApp.Services;
using Microsoft.Win32;
using Prism.Commands;
using Prism.Mvvm;

namespace ChallengesTestApp.ViewModels;

public class TaxCalculationViewModel : BindableBase
{
	private const float TaxRatePercent = ISalesDataService.TaxRateDefaultValue;
	private readonly ISalesDataService _salesDataService;

	private string _selectedFilePath = string.Empty;
	private DateTime _selectedDate = DateTime.Today;
	private float _taxRate = TaxRatePercent;
	private float _calculatedTaxValue;

	public TaxCalculationViewModel(ISalesDataService salesDataService)
	{
		_salesDataService = salesDataService;

		BrowseDataFileCommand = new DelegateCommand(BrowseDataFile);

		CalculateTaxCommand = new DelegateCommand(CalculateTax, () => TaxRate > 0).ObservesProperty(() => TaxRate);
	}

	public string SelectedFilePath
	{
		get => _selectedFilePath;
		private set => SetProperty(ref _selectedFilePath, value);
	}

	public ObservableCollection<object> Sales { get; private set; } = new ObservableCollection<object>();

	public DateTime SelectedDate
	{
		get => _selectedDate;
		set => SetProperty(ref _selectedDate, value);
	}

	public float TaxRate
	{
		get => _taxRate;
		set => SetProperty(ref _taxRate, value);
	}

	public float CalculatedTaxValue
	{
		get => _calculatedTaxValue;
		private set => SetProperty(ref _calculatedTaxValue, value);
	}

	public ICommand BrowseDataFileCommand { get; set; }

	public ICommand CalculateTaxCommand { get; set; }

	private void BrowseDataFile()
	{
		var openFileDialog = new OpenFileDialog
		{
			Filter = ISalesDataService.SourceFileFilters
		};

		if (openFileDialog.ShowDialog() == true)
		{
			SelectedFilePath = openFileDialog.FileName;

			InitializeSalesList(openFileDialog.FileName);
		}
	}

	private void InitializeSalesList(string sourcePath)
	{
		Sales.Clear();

		var sales = _salesDataService.GetSalesFromFile(sourcePath);
		
		Sales.AddRange(sales);
	}

	private void CalculateTax()
	{
		if (!Sales.Any())
		{
			return;
		}

		CalculatedTaxValue = _salesDataService.CalculateTax(SelectedDate, TaxRate);
	}
}