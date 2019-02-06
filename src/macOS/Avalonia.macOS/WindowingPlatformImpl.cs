using System;
using Avalonia.Platform;

namespace Avalonia.macOS
{
    class WindowingPlatformImpl : IWindowingPlatform
    {
        public IWindowImpl CreateWindow()
        {
            throw new NotSupportedException();
        }

        public IEmbeddableWindowImpl CreateEmbeddableWindow()
        {
            throw new NotSupportedException();
        }

        public IPopupImpl CreatePopup()
        {
            throw new NotImplementedException();
        }
    }
}
