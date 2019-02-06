using Avalonia.Media;
using AppKit;

namespace Avalonia.macOS
{
    public sealed class AvaloniaWindow : NSWindow
    {
        readonly AvaloniaRootViewController _controller = new AvaloniaRootViewController();
        public object Content
        {
            get { return _controller.Content; }
            set { _controller.Content = value; }
        }

        public AvaloniaWindow() : base()
        {
            RootViewController = _controller;
        }

        public Color StatusBarColor
        {
            get { return _controller.StatusBarColor; }
            set { _controller.StatusBarColor = value; }
        }
    }
}
