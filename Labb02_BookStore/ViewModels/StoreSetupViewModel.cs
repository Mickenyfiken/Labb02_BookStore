using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Labb02_BookStore.Domain;
using Labb02_BookStore.Presentation.Command;

namespace Labb02_BookStore.Presentation.ViewModels
{
    public class StoreSetupViewModel : ViewModelBase
    {
        public string _storeName;
        public string StoreName
        {
            get => _storeName;
            set
            {
                _storeName = value;
                RaisePropertyChanged();
            }
        }
        public string Street { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }


        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }
        private readonly Window _window;
        private readonly BookStore? _editStore;
        public StoreSetupViewModel(Window window)
        {
            _window = window;
            SaveCommand = new DelegateCommand(SaveStore);

            CancelCommand = new DelegateCommand(CancelAddStore);

        }
        public StoreSetupViewModel(Window window, BookStore store) : this(window)
        {
            _editStore = store;

            if(store == null)
            {
                MessageBox.Show("Selected a Store");
                return;
            }
            StoreName = store.Name;
            Street = store.Street;
            ZipCode = store.Zipcode;
            City = store.City;
            Country = store.Country;
        }


        private void CancelAddStore(object? obj)
        {
            _window.DialogResult = false;
            _window.Close();
        }

        private void SaveStore(object? obj)
        {
            if (_editStore != null)
            {
                _editStore.Name = StoreName;
                _editStore.Street = Street;
                _editStore.Zipcode = ZipCode;
                _editStore.City = City;
                _editStore.Country = Country;
            }
            _window.DialogResult = true;
            _window.Close();
        }
    }
}