using Avalonia.Controls.Embedding;
using CoreGraphics;
using AppKit;

namespace Avalonia.macOS
{
    public class AvaloniaView : NSView
    {
        private EmbeddableImpl _impl;
        private EmbeddableControlRoot _root;
        private Thickness _padding;

        public Thickness Padding
        {
            get { return _padding; }
            set
            {
                _padding = value;
                NeedsLayout = true;
            }
        }

        public AvaloniaView()
        {

            _impl = new EmbeddableImpl();
            AddSubview(_impl);
            WantsLayer = true;
            Layer.BackgroundColor = NSColor.White.CGColor;
            AutoresizingMask = NSViewResizingMask.HeightSizable | NSViewResizingMask.WidthSizable;
            _root = new EmbeddableControlRoot(_impl);
            _root.Prepare();
        }

        public override void Layout()
        {
            _impl.Frame = new CGRect(Padding.Left, Padding.Top,
                Frame.Width - Padding.Left - Padding.Right,
                Frame.Height - Padding.Top - Padding.Bottom);
        }


        public object Content
        {
            get { return _root.Content; }
            set { _root.Content = value; }
        }
    }
}
