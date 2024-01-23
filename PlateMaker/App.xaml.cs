using System.Windows;
using PlateMaker.Windows;

namespace PlateMaker
{
	public partial class App
	{
	    protected override void OnStartup(StartupEventArgs e)
	    {
			base.OnStartup(e);

			var mainWindow = new MainWindow();
			mainWindow.InitializeControllers();
			MainWindow = mainWindow;
			mainWindow.Show();
	    }
    }
}
