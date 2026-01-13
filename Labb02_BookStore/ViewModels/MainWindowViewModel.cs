using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Labb02_BookStore.Domain;
using Labb02_BookStore.Infrastructure.Data.Model;
using Labb02_BookStore.Presentation.Command;
using Labb02_BookStore.Presentation.Dialogs;

namespace Labb02_BookStore.Presentation.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {



        public ICommand OpenAddStoreDialogCommand { get; }
        public ICommand OpenEditStoreDialogCommand { get; }
        public ICommand DeleteStoreCommand { get; }
        private BookStore? _selectedStore;
        public BookStore? SelectedStore
        {
            get => _selectedStore;
            set
            {
                if (_selectedStore != value)
                {
                    _selectedStore = value;
                    RaisePropertyChanged();

                }
            }
        }
        private ObservableCollection<BookStore> _bookStores;

        public ObservableCollection<BookStore> BookStores
        {
            get => _bookStores; 
            set
            {
                _bookStores = value;
                RaisePropertyChanged();
            }
        }


        public MainWindowViewModel()
        {
            LoadBookStores();
            OpenAddStoreDialogCommand = new DelegateCommand(OpenAddStoreDialog);
            OpenEditStoreDialogCommand = new DelegateCommand(OpenEditStoreDialog);
            DeleteStoreCommand = new DelegateCommand(DeleteStore);

        }

        private async void DeleteStore(object? obj)
        {
            using var db = new BookStoreDbContext();

            db.Remove(SelectedStore);
            await db.SaveChangesAsync();

            var tempStore = SelectedStore;
            LoadBookStores();
            SelectedStore = BookStores.FirstOrDefault(s => s.Id == tempStore.Id);
        }

        private async void OpenEditStoreDialog(object? obj)
        {

            var dialog = new EditStoreDialog();
            
            dialog.DataContext = new StoreSetupViewModel(dialog, SelectedStore);


            if (dialog.ShowDialog() == true)
            {
                using var db = new BookStoreDbContext();
                
                var store = await db.BookStores.FindAsync(SelectedStore.Id);

                store.Name = SelectedStore.Name;
                store.Street = SelectedStore.Street;
                store.Zipcode = SelectedStore.Zipcode;
                store.City = SelectedStore.City;
                store.Country = SelectedStore.Country;


                await db.SaveChangesAsync();

                var tempStore = SelectedStore;
                LoadBookStores();
                SelectedStore = BookStores.FirstOrDefault(s => s.Id == tempStore.Id);

            }
        }

        private async void OpenAddStoreDialog(object? obj)
        {
            var dialog = new AddStoreDialog();

            if (dialog.ShowDialog() == true)
            {
                using var db = new BookStoreDbContext();

                var vm = dialog.AddStore;

                var store = new BookStore
                {
                    Name = vm.StoreName,
                    Street = vm.Street,
                    Zipcode = vm.ZipCode,
                    City = vm.City,
                    Country = vm.Country
                };

                await db.BookStores.AddAsync(store);
                await db.SaveChangesAsync();
                BookStores.Add(store);
            }

        }

        private void LoadBookStores()
        {
            using var db = new BookStoreDbContext();
            BookStores = new ObservableCollection<BookStore>(db.BookStores.ToList());
        }

    }
}
