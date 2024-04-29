using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using TelephoneCompany.Models;
using TelephoneCompany.Services;
using TelephoneCompany.Views;

namespace TelephoneCompany.ViewModels
{
    class NumberSearchViewModel : DependencyObject, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyname)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
        }
        public string SearchText
        {
            get { return (string)GetValue(SearchTextProperty); }
            set { SetValue(SearchTextProperty, value); }
        }

        public static readonly DependencyProperty SearchTextProperty =
            DependencyProperty.Register("SearchText", typeof(string), typeof(NumberSearchViewModel), new PropertyMetadata(""));


        public ICollectionView NumberItems
        {
            get { return (ICollectionView)GetValue(NumberItemsProperty); }
            set { SetValue(NumberItemsProperty, value); }
        }

        public static readonly DependencyProperty NumberItemsProperty =
            DependencyProperty.Register("MyProperty", typeof(ICollectionView), typeof(NumberSearchViewModel), new PropertyMetadata(null));

        public ICommand SearchNumberCommand { get; set; }
        public NumberSearchViewModel()
        {
            SearchNumberCommand = new RelayCommand(SearchNumber, CanUseCommand);
        }
        private bool CanUseCommand(object obj)
        {
            return true;
        }
        private void SearchNumber(object obj)
        {
            var result = DataWorker.SearchNumber(SearchText);
            if(result.Count == 0)
            {
                NumberItems = null;
                OnPropertyChanged(nameof(NumberItems));
                MessageView message = new MessageView("Абоненты не найдены");
                message.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                message.ShowDialog();
            }
            else
            {
                NumberItems = CollectionViewSource.GetDefaultView(result);
                OnPropertyChanged(nameof(NumberItems));
            }
        }
    }
}
