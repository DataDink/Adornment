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
    public class TestAdorner : Adorner
    {
        public TestAdorner(UIElement adornedElement) : base(adornedElement) {}

        protected override void OnRender(System.Windows.Media.DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            drawingContext.DrawLine(new Pen(Brushes.Red, 10), new Point(0, 0), new Point(50, 50));
        }
    }
}
