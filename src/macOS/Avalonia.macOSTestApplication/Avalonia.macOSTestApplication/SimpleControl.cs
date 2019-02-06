using System;
using Avalonia.Controls;
using Avalonia.Media;

namespace Avalonia.macOSTestApplication
{
    class SimpleControl : ContentControl
    {
        public SimpleControl()
        {
            Content = new Button() {Content = "WAT"};
            MinWidth = 100;
            MinHeight = 200;
            Background = Brushes.CadetBlue;
        }
    }
}