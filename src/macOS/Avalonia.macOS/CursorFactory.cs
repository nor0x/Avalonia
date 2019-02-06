using System;
using Avalonia.Input;
using Avalonia.Platform;

namespace Avalonia.macOS
{
    class CursorFactory : IStandardCursorFactory
    {
        public IPlatformHandle GetCursor(StandardCursorType cursorType) => new PlatformHandle(IntPtr.Zero, "NULL");
    }
}