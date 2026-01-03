using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Labb02_BookStore.Presentation.Command;

namespace Labb02_BookStore.Presentation.ViewModels
{
    public class AddStoreViewModel
    {
        public string StoreName { get; set; }
        public string Street { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public ICommand AddCommand { get; }
        public ICommand CancelCommand { get; }

        public AddStoreViewModel(Window window)
        {
            AddCommand = new DelegateCommand(d =>
        {
            window.DialogResult = true;
            window.Close();
        });

        }

    }
}
