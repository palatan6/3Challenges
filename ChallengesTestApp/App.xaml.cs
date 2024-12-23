using System.Windows;
using ChallengesTestApp.Services;
using Prism.Ioc;
using Prism.Unity;

namespace ChallengesTestApp
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : PrismApplication
	{
		protected override void RegisterTypes(IContainerRegistry containerRegistry)
		{
			containerRegistry.Register<ITextProcessorService, TextProcessorService>();

			containerRegistry.Register<IBonusDrawingService, BonusDrawingService>();

			containerRegistry.Register<ISalesDataService, SalesDataService>();
		}

		protected override Window CreateShell()
		{
			var w = Container.Resolve<MainWindow>();
			return w;
		}
	}
}
