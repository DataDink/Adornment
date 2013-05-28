using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

namespace Adorners.Ui
{
    public class GridAdornment : Adorner
    {
        public GridAdornment(UIElement adornedElement) : base(adornedElement) {}

        protected override void OnRender(System.Windows.Media.DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            var element = AdornedElement as FrameworkElement;
            if (element == null) return;

            var pen = new Pen(Brushes.Gray, 1);
            for (var x = 0; x < element.ActualWidth; x += 100)
                for (var y = 0; y < element.ActualHeight; y += 100) {
                    drawingContext.DrawLine(pen,
                        new Point(x, 0),
                        new Point(x, element.ActualHeight));
                    drawingContext.DrawLine(pen,
                        new Point(0, y),
                        new Point(element.ActualWidth, y));
                }
        }
    }
}
