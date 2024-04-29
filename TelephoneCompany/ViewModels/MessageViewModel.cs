using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TelephoneCompany.Services;

namespace TelephoneCompany.ViewModels
{
    class MessageViewModel : DependencyObject
    {
        public string MessageText
        {
            get { return (string)GetValue(MessageTextProperty); }
            set { SetValue(MessageTextProperty, value); }
        }

        public static readonly DependencyProperty MessageTextProperty =
            DependencyProperty.Register("MessageText", typeof(string), typeof(MessageViewModel), new PropertyMetadata(""));
        
        public ICommand CloseWindow { get; set; }
        public MessageViewModel(string message)
        {
            CloseWindow = new RelayCommand(Close, CanUseCommand);
            MessageText = message;
        }

        private void Close(object obj)
        {
            Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive).Close();
        }

        private bool CanUseCommand(object obj)
        {
            return true;
        }
    }
}
