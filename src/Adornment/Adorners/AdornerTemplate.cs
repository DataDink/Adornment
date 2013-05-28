using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Markup;

namespace Adorners
{
    [ContentProperty("ConstructorParameters")]
    public class AdornerTemplate : DependencyObject
    {
        protected readonly List<object> Parameters = new List<object>();

        public List<object> ConstructorParameters
        {
            get { return Parameters; }
            set { Parameters.Clear(); Parameters.AddRange(value); }
        } 

        /// <summary>
        /// The adorner type to be created
        /// </summary>
        public Type Type { get; set; }

        public virtual Adorner LoadContent(UIElement adornedElement)
        {
            var parameters = ConstructorParameters.ToList();
            parameters.Insert(0, adornedElement);
            return Activator.CreateInstance(Type, parameters.ToArray()) as Adorner;
        }
    }
}
