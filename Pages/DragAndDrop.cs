using System.Windows.Input;
using System.Windows;
using System.Diagnostics;

namespace ShreeGovardhanTextilesSystem.Pages
{
    class DragAndDrop
    {
        private Window window;
        private Point offsetPosition;

        public DragAndDrop(Window window)
        {
            this.window = window;
        }

        public void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Trace.WriteLine("DragAndDrop: OnMouseLeftButtonDown");
            Point mousePosition = Mouse.GetPosition(this.window);
            Point cursorPosition = this.window.PointToScreen(mousePosition);
            Point windowPosition = new Point(this.window.Left, this.window.Top);
            offsetPosition = (Point)(cursorPosition - windowPosition);
            Mouse.Capture(this.window, CaptureMode.Element);
        }

        public void OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Trace.WriteLine("DragAndDrop: OnMouseLeftButtonUp");
            Mouse.Capture(null);
        }

        public void OnMouseMove(object sender, MouseEventArgs e)
        {
            Trace.WriteLine("DragAndDrop: OnMouseMove");
            if (Mouse.Captured == this.window
                && Mouse.LeftButton == MouseButtonState.Pressed)
            {
                Trace.WriteLine("DragAndDrop: OnMouseMove: Moving the window provided");
                Point mousePosition = Mouse.GetPosition(this.window);
                Point cursorPosition = this.window.PointToScreen(mousePosition);
                //Point newPosition = new Point (cursorPosition.X - offsetPosition.X, 
                //                               cursorPosition.Y - offsetPosition.Y);
                Point newPosition = (Point)(cursorPosition - offsetPosition);

                this.window.Left = newPosition.X;
                this.window.Top = newPosition.Y;
            }
        }

    }
}
