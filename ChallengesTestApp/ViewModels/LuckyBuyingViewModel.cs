using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using ChallengesTestApp.Services;
using Prism.Commands;
using Prism.Mvvm;

namespace ChallengesTestApp.ViewModels
{
	public class LuckyBuyingViewModel : BindableBase
	{
		private readonly IBonusDrawingService _bonusDrawingService;
		private const string ItemsInStringSeparator = ", ";
		private const string TotalPriceDefaultValue = "0";

		private string _totalPrice = TotalPriceDefaultValue;
		private string _selectedItemsNames;
		private bool _isInCheckoutState;

		public LuckyBuyingViewModel( IBonusDrawingService bonusDrawingService)
		{
			_bonusDrawingService = bonusDrawingService;

			SelectionChangedCommand = new DelegateCommand(OnSelectionChanged);

			CheckoutCommand = new DelegateCommand(ExecuteCheckout, CanExecuteCheckout);

			FinishPurchaseCommand = new DelegateCommand(ExecuteFinishPurchase);
		}

		public ICommand SelectionChangedCommand { get; }

		public DelegateCommand CheckoutCommand { get; }

		public ICommand FinishPurchaseCommand { get; }


		public ObservableCollection<PhotoPackageViewModel> PhotoPackages { get; } =
			new( new[]
			{
				new PhotoPackageViewModel("Prints", "4x6 photo", 5),
				new PhotoPackageViewModel("Panoramas", "6x12 prints", 7),
				new PhotoPackageViewModel("Strips", "two 2x6 photo strips", 5),
			} );

		public ObservableCollection<PhotoPackageViewModel> BonusPhotoPackages { get; } = new();

		public string TotalPrice
		{
			get => _totalPrice;
			set => SetProperty(ref _totalPrice, value);
		}

		public string SelectedItemsNames
		{
			get => _selectedItemsNames;
			set
			{
				SetProperty(ref _selectedItemsNames, value);
				CheckoutCommand.RaiseCanExecuteChanged();
			}
		}

		public string BonusItemsNames => string.Join(ItemsInStringSeparator, BonusPhotoPackages.Select(x => x.Name));

		public bool IsInCheckoutState
		{
			get => _isInCheckoutState;
			set => SetProperty(ref _isInCheckoutState, value);
		}

		public bool IsBonusGiven => BonusPhotoPackages.Any();

		private void OnSelectionChanged()
		{
			UpdateCartProperties();
		}

		private bool CanExecuteCheckout()
		{
			return !string.IsNullOrEmpty(SelectedItemsNames) && !IsInCheckoutState;
		}

		private void ExecuteCheckout()
		{
			IsInCheckoutState = true;

			CheckAndGenerateBonus();
		}

		private void UpdateCartProperties()
		{
			TotalPrice = TotalPriceDefaultValue;
			SelectedItemsNames = string.Empty;

			var selectedItems = PhotoPackages.Where(p => p.IsSelected).ToList();

			if (selectedItems != null && selectedItems.Any())
			{
				TotalPrice = selectedItems.Sum(x => x.Price).ToString();

				SelectedItemsNames = string.Join(ItemsInStringSeparator, selectedItems.Select(x => x.Name));
			}
		}

		private void CheckAndGenerateBonus()
		{
			var selectedItems = PhotoPackages.Where(p => p.IsSelected).ToList();

			if (selectedItems == null || selectedItems.Count == PhotoPackages.Count)
			{
				return;
			}

			if (!_bonusDrawingService.IsBonusDrawn())
			{
				return;
			}

			MessageBox.Show("Congratulation! You won bonus item(s)!", "Win!");

			BonusPhotoPackages.AddRange(PhotoPackages.Except(selectedItems));
			RaisePropertyChanged(nameof(BonusItemsNames));
			RaisePropertyChanged(nameof(IsBonusGiven));
		}

		private void ResetEverything()
		{
			IsInCheckoutState = false;

			foreach (var photoPackage in PhotoPackages)
			{
				photoPackage.IsSelected = false;
			}

			BonusPhotoPackages.Clear();
			RaisePropertyChanged(nameof(BonusItemsNames));
			RaisePropertyChanged(nameof(IsBonusGiven));
		}

		private void ExecuteFinishPurchase()
		{
			// Resetting everything just for better testing
			ResetEverything();
		}
	}
}
