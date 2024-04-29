using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using TelephoneCompany.Models;

namespace TelephoneCompany.ViewModels
{
    class StreetsViewModel : DependencyObject
    {


        public ICollectionView StreetItems
        {
            get { return (ICollectionView)GetValue(StreetItemsProperty); }
            set { SetValue(StreetItemsProperty, value); }
        }
        public static readonly DependencyProperty StreetItemsProperty =
            DependencyProperty.Register("MyProperty", typeof(ICollectionView), typeof(StreetsViewModel), new PropertyMetadata(null));
        public StreetsViewModel()
        {
            StreetItems = CollectionViewSource.GetDefaultView(DataWorker.GetStreets());
        }

    }
}
