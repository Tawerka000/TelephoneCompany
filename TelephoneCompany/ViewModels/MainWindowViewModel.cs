using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using TelephoneCompany.Models;
using TelephoneCompany.Services;
using TelephoneCompany.Views;

namespace TelephoneCompany.ViewModels
{
    class MainWindowViewModel : DependencyObject
    {
        public string FilterText
        {
            get { return (string)GetValue(FilterTextProperty); }
            set { SetValue(FilterTextProperty, value); }
        }

        public static readonly DependencyProperty FilterTextProperty =
            DependencyProperty.Register("FilterText", typeof(string), typeof(MainWindowViewModel), new PropertyMetadata("", FilterText_Changed));

        private static void FilterText_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var current = d as MainWindowViewModel;
            if (current != null)
            {
                current.Abonents.Filter = null;
                current.Abonents.Filter = current.FilterAbonent;
            }
        }

        public ICollectionView Abonents
        {
            get { return (ICollectionView)GetValue(AbonentsProperty); }
            set { SetValue(AbonentsProperty, value); }
        }

        public static readonly DependencyProperty AbonentsProperty =
            DependencyProperty.Register("MyProperty", typeof(ICollectionView), typeof(MainWindowViewModel), new PropertyMetadata(null));


        public ICommand OpenSearchNumberWindowCommand { get; set; }
        public ICommand OpenStreetsWindowCommand { get; set; }
        public ICommand SaveDataCommand { get; set; }

        public MainWindowViewModel()
        {
            OpenSearchNumberWindowCommand = new RelayCommand(OpenSearchNumberWindow, CanUseCommand);
            OpenStreetsWindowCommand = new RelayCommand(OpenStreetsWindow, CanUseCommand);
            SaveDataCommand = new RelayCommand(Save, CanUseCommand);

            Abonents = CollectionViewSource.GetDefaultView(DataWorker.GetMainData());
            Abonents.Filter = FilterAbonent;
            
        }

        private void OpenStreetsWindow(object obj)
        {
            StreetsView streetsWindow = new StreetsView();
            streetsWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            streetsWindow.ShowDialog();
        }

        private void OpenSearchNumberWindow(object obj)
        {
            NumberSearchView searchWindow = new NumberSearchView();
            searchWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            searchWindow.ShowDialog();
        }


        public bool FilterAbonent(object obj)
        {
            bool result = true;
            MainWindowData data = obj as MainWindowData;
            if (!String.IsNullOrWhiteSpace(FilterText) && 
                  data != null && 
                !(data.Name.Contains(FilterText) ||
                  data.SecondName.Contains(FilterText) ||
                  data.SurName.Contains(FilterText) ||
                  data.Street.Contains(FilterText) ||
                  data.HomeNumber.Contains(FilterText) ||
                  data.WorkNumber.Contains(FilterText) ||
                  data.MobileNumber.Contains(FilterText) ||
                  data.HouseNumber.ToString().Contains(FilterText) ||
                  data.AbonentID.ToString().Contains(FilterText))
                )
            {
                result = false;
            }
            return result;
        }
        private string[] SaveData(ICollectionView abonents)
        {
            List<object> filteredData = GetFilteredData(abonents);
            return GetConvertedData(abonents, filteredData);
        }
        private void Save(object obj)
        {
            DataWorker.SaveToCSV(SaveData(Abonents));
            MessageView message = new MessageView("Данные сохранены.");
            message.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            message.ShowDialog();
        }
        private bool CanUseCommand(object obj)
        {
            return true;
        }
        private string[] GetConvertedData(ICollectionView abonents, List<object> filteredData)
        {
            string[] array = new string[filteredData.Count()+1];
            var itemType = abonents.Cast<object>().FirstOrDefault()?.GetType();
            var properties = itemType?.GetProperties();
            IEnumerable<string> columnNames = properties?.Select(prop => prop.Name);
            if(columnNames != null)
                array[0] = string.Join(",", columnNames);
            for (int i = 0; i < filteredData.Count(); i++)
            {
                var fieldValues = properties.Select(prop => prop.GetValue(filteredData[i])?.ToString() ?? string.Empty);
                array[i + 1] = string.Join(",", fieldValues);
            }
            return array;
        }
        private List<object> GetFilteredData(ICollectionView items)
        {
            List<object> filteredData = new List<object>();

            foreach (object item in items)
            {
                if (items.Filter == null || items.Filter(item))
                {
                    filteredData.Add(item);
                }
            }
            return filteredData;
        }
    }
}
