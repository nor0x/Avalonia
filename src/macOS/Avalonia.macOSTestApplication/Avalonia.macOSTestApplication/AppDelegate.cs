using AppKit;
using Avalonia.Controls;
using Avalonia.macOS;
using Avalonia.Media;
using Avalonia.Threading;
using Foundation;

namespace Avalonia.macOSTestApplication
{
    [Register("AppDelegate")]
    public class AppDelegate : NSApplicationDelegate
    {
        public AppDelegate()
        {
        }

        public override void DidFinishLaunching(NSNotification notification)
        {
            // Insert code here to initialize your application
            AppBuilder.Configure<SimpleApp>().UseSkia().UsemacOS().SetupWithoutStarting();
            var window = new AvaloniaWindow { Content = new SimpleControl() };
            window.MakeMainWindow();
            window.MakeKeyAndOrderFront(this);
        }

        public override void WillTerminate(NSNotification notification)
        {
            // Insert code here to tear down your application
        }
    }
}
