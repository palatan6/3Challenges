using System;
using System.Windows;
using System.Windows.Data;

namespace ChallengesTestApp.Converters;

[ValueConversion(typeof(bool), typeof(Visibility))]
public class InverseBooleanToVisibilityConverter : IValueConverter
{
	public object Convert(object value, Type targetType, object parameter,
		System.Globalization.CultureInfo culture)
	{
		if (targetType != typeof(Visibility))
			throw new InvalidOperationException("The target must be a Visibility");

		return (bool)value ? Visibility.Collapsed : Visibility.Visible;
	}

	public object ConvertBack(object value, Type targetType, object parameter,
		System.Globalization.CultureInfo culture)
	{
		throw new NotSupportedException();
	}
}