using System;
using Avalonia.Media;
using CoreGraphics;
using AppKit;

namespace Avalonia.macOS
{
    static class Extensions
    {

        public static Size ToAvalonia(this CGSize size) => new Size(size.Width, size.Height);

        public static Point ToAvalonia(this CGPoint point) => new Point(point.X, point.Y);

        static nfloat ColorComponent(byte c) => ((float)c) / 255;

        public static NSColor ToNsColor(this Color color) => NSColor.FromRgba(
            ColorComponent(color.R),
            ColorComponent(color.G),
            ColorComponent(color.B),
            ColorComponent(color.A));

        public static CGColor ToCgColor(this Color color) => NSColor.FromRgba(
            ColorComponent(color.R),
            ColorComponent(color.G),
            ColorComponent(color.B),
            ColorComponent(color.A)).CGColor;
    }
}
