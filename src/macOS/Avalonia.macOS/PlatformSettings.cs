using System;
using Avalonia.Platform;
using AppKit;

namespace Avalonia.macOS
{
    class PlatformSettings : IPlatformSettings
    {
        public Size DoubleClickSize => new Size(4, 4);
        public TimeSpan DoubleClickTime => TimeSpan.FromMilliseconds(200);
        public double RenderScalingFactor => NSScreen.MainScreen.BackingScaleFactor;
        public double LayoutScalingFactor => 1;
    }
}
