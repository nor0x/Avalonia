using Avalonia.Markup.Xaml;
using Foundation;

namespace Avalonia.macOSTestApplication
{
    public class SimpleApp : Avalonia.Application
    {
        public override void Initialize()
        {
            //Enforce load
            new Avalonia.Themes.Default.DefaultTheme();
            AvaloniaXamlLoader.Load(this);
        }
    }
}