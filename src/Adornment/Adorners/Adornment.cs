using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Adorners
{
    public sealed class Adornment : DependencyObject
    {
        private Adornment() {}

        public static readonly DependencyProperty LeftProperty = DependencyProperty.RegisterAttached("Left", typeof(double), typeof(Adornment), new PropertyMetadata(double.NaN, OnPositionChanged));
        public static readonly DependencyProperty TopProperty = DependencyProperty.RegisterAttached("Top", typeof(double), typeof(Adornment), new PropertyMetadata(double.NaN, OnPositionChanged));
        public static readonly DependencyProperty RightProperty = DependencyProperty.RegisterAttached("Right", typeof(double), typeof(Adornment), new PropertyMetadata(double.NaN, OnPositionChanged));
        public static readonly DependencyProperty BottomProperty = DependencyProperty.RegisterAttached("Bottom", typeof(double), typeof(Adornment), new PropertyMetadata(double.NaN, OnPositionChanged));
        public static readonly DependencyProperty VisibilityProperty = DependencyProperty.RegisterAttached("Visibility", typeof (Visibility), typeof (Adornment), new PropertyMetadata(Visibility.Visible, OnVisibilityChanged));
        public static readonly DependencyProperty OwnerProperty = DependencyProperty.RegisterAttached("Owner", typeof(DependencyObject), typeof(Adornment), new PropertyMetadata(null));

        /// <summary>
        /// Sets the left offset of an adorning FrameworkElement
        /// </summary>
        public static void SetLeft(DependencyObject element, double value) { element.SetValue(LeftProperty, value); }
        /// <summary>
        /// Gets the left offset of an adorning FrameworkElement
        /// </summary>
        public static double GetLeft(DependencyObject element) { return (double)element.GetValue(LeftProperty); }

        /// <summary>
        /// Sets the top offset of an adorning FrameworkElement
        /// </summary>
        public static void SetTop(DependencyObject element, double value) { element.SetValue(TopProperty, value); }
        /// <summary>
        /// Gets the top offset of an adorning FrameworkElement
        /// </summary>
        public static double GetTop(DependencyObject element) { return (double)element.GetValue(TopProperty); }

        /// <summary>
        /// Sets the right offset of an adorning FrameworkElement
        /// </summary>
        public static void SetRight(DependencyObject element, double value) { element.SetValue(RightProperty, value); }
        /// <summary>
        /// Gets the right offset of an adorning FrameworkElement
        /// </summary>
        public static double GetRight(DependencyObject element) { return (double)element.GetValue(RightProperty); }

        /// <summary>
        /// Sets the bottom offset of an adorning FrameworkElement
        /// </summary>
        public static void SetBottom(DependencyObject element, double value) { element.SetValue(BottomProperty, value); }
        /// <summary>
        /// Gets the bottom offset of an adorning FrameworkElement
        /// </summary>
        public static double GetBottom(DependencyObject element) { return (double)element.GetValue(BottomProperty); }

        private static void OnPositionChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var element = sender as FrameworkElement;
            if (element == null) return;

            var owner = GetOwner(element) as FrameworkElement;
            if (owner == null) return;

            var left = GetLeft(element);
            var top = GetTop(element);
            var right = GetRight(element);
            var bottom = GetBottom(element);

            var x = 0d;
            if (!double.IsNaN(left)) {
                x = left;
            } else if (!double.IsNaN(right)) {
                x = owner.ActualWidth - right - element.Width;
            }

            var y = 0d;
            if (!double.IsNaN(top)) {
                y = top;
            } else if (!double.IsNaN(bottom)) {
                y = owner.ActualHeight - bottom - element.Height;
            }

            var width = double.IsNaN(element.Width) ? 0 : element.Width;
            if (!double.IsNaN(left) && !double.IsNaN(right)) {
                width = owner.ActualWidth - left - right;
            }
            width = Math.Max(0d, width);

            var height = double.IsNaN(element.Height) ? 0 : element.Height;
            if (!double.IsNaN(top) && !double.IsNaN(bottom))
            {
                height = owner.ActualHeight - top - bottom;
            }
            height = Math.Max(0d, height);


            element.Arrange(new Rect(x, y, width, height));
        }

        /// <summary>
        /// Sets the visiblity of this element's adorner layer
        /// </summary>
        public static void SetVisibility(DependencyObject element, Visibility value)
        {
            element.SetValue(VisibilityProperty, value);
        }

        /// <summary>
        /// Gets the visiblity of this element's adorner layer
        /// </summary>
        public static Visibility GetVisibility(DependencyObject element)
        {
            return (Visibility)element.GetValue(VisibilityProperty);
        }

        private static void OnVisibilityChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var visual = sender as Visual;
            if (visual == null) return;

            var layer = AdornerLayer.GetAdornerLayer(visual);
            if (layer == null) return;

            layer.Visibility = GetVisibility(sender);
        }

        /// <summary>
        /// Retrieves the DependencyObject that owns the AdornerLayer containing the Adornment
        /// </summary>
        public static DependencyObject GetOwner(DependencyObject adorner)
        {
            return adorner.GetValue(OwnerProperty) as DependencyObject;
        }

        private static void SetOwner(DependencyObject adorner, DependencyObject value)
        {
            adorner.SetValue(OwnerProperty, value);
        }

        public static readonly DependencyProperty ContentProperty = DependencyProperty.RegisterAttached("Content", typeof (DependencyObject), typeof (Adornment), new PropertyMetadata(null, OnContentSet));

        /// <summary>
        /// Sets the adornment content for the DependencyObject
        /// </summary>
        /// <param name="owner">The DependencyObject to adorn</param>
        /// <param name="value">Can be a(n) AdornerTemplate, AdornmentCollection, or UIElement</param>
        public static void SetContent(DependencyObject owner, DependencyObject value)
        {
            owner.SetValue(ContentProperty, value);
        }

        private static DependencyObject GetContent(DependencyObject owner)
        {
            return owner.GetValue(ContentProperty) as DependencyObject;
        }

        private static void OnContentSet(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var owner = sender as FrameworkElement;
            if (owner == null) {
                SetAdorners(sender, null);
                return;
            };

            if (owner.IsLoaded) SetAdorners(owner, null);
            else owner.Loaded += SetAdorners;
        }

        private static void SetAdorners(object sender, EventArgs e)
        {
            var element = sender as FrameworkElement;
            if (element != null) element.Loaded -= SetAdorners;

            var owner = sender as UIElement;
            if (owner == null) return;

            var content = GetContent(owner);
            if (content == null) return;

            var items = new List<DependencyObject>();
            var collection = content as AdornmentCollection;
            if (collection != null) {
                items.AddRange(collection);
            } else {
                items.Add(content);
            }


            var layer = AdornerLayer.GetAdornerLayer(owner);
            foreach (var item in items)
            {
                var itemTemplate = item as AdornerTemplate;
                var itemElement = item as UIElement;
                var adorner = itemTemplate != null ? itemTemplate.LoadContent(owner)
                    : itemElement != null ? new CanvasAdorner(owner, new[] { itemElement }) 
                    : null;
                if (adorner == null) continue;
                SetOwner(adorner, owner);
                if (itemElement != null) SetOwner(itemElement, owner);

                layer.Add(adorner);
            }
        }

        private class CanvasAdorner : Adorner
        {
            private readonly VisualCollection _container;
            private readonly List<UIElement> _elements; 

            public CanvasAdorner(UIElement adornedElement, IEnumerable<UIElement> elements) : base(adornedElement)
            {
                _container = new VisualCollection(this);
                var owner = adornedElement as FrameworkElement;
                if (elements == null) return;
                _elements = elements.ToList();
                
                _elements.ForEach(element => {
                    _container.Add(element);
                    if (owner != null) {
                        owner.SizeChanged += (s, e) => OnPositionChanged(element, new DependencyPropertyChangedEventArgs());
                    }
                });
            }

            protected override Size ArrangeOverride(Size finalSize)
            {
                var owner = AdornedElement as FrameworkElement;
                if (owner == null) return finalSize;
                _elements.ForEach(element => OnPositionChanged(element, new DependencyPropertyChangedEventArgs()));
                return finalSize;
            }

            protected override int VisualChildrenCount { get { return _container.Count; } }
            protected override Visual GetVisualChild(int index) { return _container[index]; }
        }
    }
}
