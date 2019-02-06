using AppKit;

namespace Avalonia.macOSTestApplication
{
    static class MainClass
    {
        static void Main(string[] args)
        {
            NSApplication.Init();
            NSApplication.Main(args);
        }
    }
}

