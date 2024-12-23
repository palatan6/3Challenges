using ChallengesTestApp.Services;
using Prism.Mvvm;

namespace ChallengesTestApp.ViewModels;

public class MirrorTextViewModel : BindableBase
{
	private readonly ITextProcessorService _textProcessorService;
	private string? _inputText;

	public MirrorTextViewModel(ITextProcessorService textProcessorService)
	{
		_textProcessorService = textProcessorService;
	}

	public string InputText
	{
		get => _inputText;
		set
		{
			SetProperty(ref _inputText, value);
			RaisePropertyChanged(nameof(ProcessedText));
		}
	}

	public string ProcessedText
	{
		get
		{
			if (string.IsNullOrWhiteSpace(InputText))
			{
				return string.Empty;
			}

			return _textProcessorService.ProcessText(InputText);
		}
	}
}