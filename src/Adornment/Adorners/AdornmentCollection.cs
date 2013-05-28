using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Markup;
using Adorners.Extensions;

namespace Adorners
{
    [ContentProperty("Items")]
    public class AdornmentCollection : DependencyCollection<DependencyObject>
    {
        // TODO - research a better way to handle this
        public List<DependencyObject> Items
        {
            get { return Internal; }
            set { Clear(); AddRange(value); }
        } 
    }
}
