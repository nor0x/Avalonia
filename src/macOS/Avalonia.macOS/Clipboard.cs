using System.Threading.Tasks;
using Avalonia.Input.Platform;
using AppKit;

namespace Avalonia.macOS
{
    public class Clipboard : IClipboard
    {
        public Task<string> GetTextAsync()
        {
            return Task.FromResult(NSPasteboard.GeneralPasteboard.GetStringForType(NSPasteboard.NSStringType));
        }

        public Task SetTextAsync(string text)
        {
            NSPasteboard.GeneralPasteboard.SetStringForType(text, NSPasteboard.NSStringType);
            return Task.FromResult(0);
        }

        public Task ClearAsync()
        {
            NSPasteboard.GeneralPasteboard.SetStringForType(string.Empty, NSPasteboard.NSStringType);
            return Task.FromResult(0);
        }
    }
}
