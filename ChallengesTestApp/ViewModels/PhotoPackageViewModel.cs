using Prism.Mvvm;

namespace ChallengesTestApp.ViewModels;

public class PhotoPackageViewModel : BindableBase
{
	private bool _isSelected;

	public PhotoPackageViewModel(string name, string description, float price)
	{
		Name = name;
		Price = price;
		Description = description;
	}

	public string Name { get; set; }

	public string Description { get; set; }

	public float Price { get; set; }

	public string DisplayValue => $"{Name} ({Description}) - ${Price}";

	public bool IsSelected
	{
		get => _isSelected;
		set => SetProperty(ref _isSelected, value);
	}
}