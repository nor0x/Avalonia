using System;
using System.Collections.Generic;
using Avalonia.Controls.Platform.Surfaces;
using Avalonia.Input;
using Avalonia.Input.Raw;
using Avalonia.macOS.Specific;
using Avalonia.Platform;
using Avalonia.Rendering;
using CoreGraphics;
using Foundation;
using ObjCRuntime;
using AppKit;

namespace Avalonia.macOS
{
    [Adopts("UIKeyInput")]
    class TopLevelImpl : NSView, ITopLevelImpl, IFramebufferPlatformSurface
    {
        private IInputRoot _inputRoot;
        private readonly KeyboardEventsHelper<TopLevelImpl> _keyboardHelper;

        public TopLevelImpl()
        {
            _keyboardHelper = new KeyboardEventsHelper<TopLevelImpl>(this);
            AutoresizingMask = NSViewAutoresizing.All;
            _keyboardHelper.ActivateAutoShowKeyboard();
        }

        [Export("hasText")]
        public bool HasText => _keyboardHelper.HasText();

        [Export("insertText:")]
        public void InsertText(string text) => _keyboardHelper.InsertText(text);

        [Export("deleteBackward")]
        public void DeleteBackward() => _keyboardHelper.DeleteBackward();

        public override bool CanBecomeFirstResponder => _keyboardHelper.CanBecomeFirstResponder();

        public Action Closed { get; set; }
        public Action<RawInputEventArgs> Input { get; set; }
        public Action<Rect> Paint { get; set; }
        public Action<Size> Resized { get; set; }
        public Action<double> ScalingChanged { get; set; }

        public new IPlatformHandle Handle => null;

        public double Scaling => NSScreen.MainScreen.BackingScaleFactor;


        public override void LayoutSubviews() => Resized?.Invoke(ClientSize);

        public Size ClientSize => Bounds.Size.ToAvalonia();

        public IMouseDevice MouseDevice => macOSPlatform.MouseDevice;

        public IRenderer CreateRenderer(IRenderRoot root)
        {
            return new ImmediateRenderer(root);
        }

        public override void Draw(CGRect rect)
        {
            Paint?.Invoke(new Rect(rect.X, rect.Y, rect.Width, rect.Height));
        }

        public void Invalidate(Rect rect) => SetNeedsDisplay();

        public void SetInputRoot(IInputRoot inputRoot) => _inputRoot = inputRoot;

        public Point PointToClient(PixelPoint point) => point.ToPoint(1);

        public PixelPoint PointToScreen(Point point) => PixelPoint.FromPoint(point, 1);

        public void SetCursor(IPlatformHandle cursor)
        {
            //Not supported
        }

        public IEnumerable<object> Surfaces => new object[] { this };

        public override void TouchesEnded(NSSet touches, NSEvent evt)
        {
            var touch = touches.AnyObject as NSTouch;
            if (touch != null)
            {
                var location = touch.LocationInView(this).ToAvalonia();

                Input?.Invoke(new RawMouseEventArgs(
                    macOSPlatform.MouseDevice,
                    (uint)touch.Timestamp,
                    _inputRoot,
                    RawMouseEventType.LeftButtonUp,
                    location,
                    InputModifiers.None));
            }
        }

        Point _touchLastPoint;
        public override void TouchesBegan(NSSet touches, NSEvent evt)
        {
            var touch = touches.AnyObject as UITouch;
            if (touch != null)
            {
                var location = touch.LocationInView(this).ToAvalonia();
                _touchLastPoint = location;
                Input?.Invoke(new RawMouseEventArgs(macOSPlatform.MouseDevice, (uint)touch.Timestamp, _inputRoot,
                    RawMouseEventType.Move, location, InputModifiers.None));

                Input?.Invoke(new RawMouseEventArgs(macOSPlatform.MouseDevice, (uint)touch.Timestamp, _inputRoot,
                    RawMouseEventType.LeftButtonDown, location, InputModifiers.None));
            }
        }

        public override void TouchesMoved(NSSet touches, NSEvent evt)
        {
            var touch = touches.AnyObject as NSTouch;
            if (touch != null)
            {
                var location = touch.LocationInView(this).ToAvalonia();
                if (macOSPlatform.MouseDevice.Captured != null)
                    Input?.Invoke(new RawMouseEventArgs(macOSPlatform.MouseDevice, (uint)touch.Timestamp, _inputRoot,
                        RawMouseEventType.Move, location, InputModifiers.LeftMouseButton));
                else
                {
                    //magic number based on test - correction of 0.02 is working perfect
                    double correction = 0.02;

                    Input?.Invoke(new RawMouseWheelEventArgs(macOSPlatform.MouseDevice, (uint)touch.Timestamp,
                        _inputRoot, location, (location - _touchLastPoint) * correction, InputModifiers.LeftMouseButton));
                }
                _touchLastPoint = location;
            }
        }

        public ILockedFramebuffer Lock() => new EmulatedFramebuffer(this);
    }
}
