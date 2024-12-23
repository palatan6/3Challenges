using System.Linq;

namespace ChallengesTestApp.Services;

public class TextProcessorService : ITextProcessorService
{
	public string ProcessText(string inputText)
	{
		return string.Join(' ', inputText.Split(' ').Reverse());
	}
}