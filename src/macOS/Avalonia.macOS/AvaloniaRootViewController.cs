using Avalonia.Media;
using CoreGraphics;
using AppKit;

namespace Avalonia.macOS
{
    class AvaloniaRootViewController : NSViewController
    {
        private object _content;
        private Color _statusBarColor = Colors.White;

        public object Content
        {
            get { return _content; }
            set
            {
                _content = value;
                var view = (View as AvaloniaView);
                if (view != null)
                    view.Content = value;
            }
        }

        public Color StatusBarColor
        {
            get { return _statusBarColor; }
            set
            {
                _statusBarColor = value;
                var view = (View as AvaloniaView);
                if (view != null)
                    view.Layer.BackgroundColor = value.ToCgColor();
            }
        }

        void AutoFit()
        {

            var frame = NSScreen.MainScreen.Frame;
            ((AvaloniaView)View).Padding =
                new Thickness(0, 0, 0, 0);
            View.Frame = frame;
        }

        public override void LoadView()
        {
            View = new AvaloniaView() { Content = Content };
            NSApplication.Notifications.ObserveWindowResized(delegate { AutoFit(); });
            AutoFit();
        }
    }
}