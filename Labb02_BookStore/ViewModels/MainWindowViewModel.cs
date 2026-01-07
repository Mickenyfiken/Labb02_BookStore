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
    class MainWindowViewModel : ViewModelBase
    {



        public ICommand OpenAddStoreDialogCommand { get; }
        public ICommand DeleteStoreCommand { get; }

        public ObservableCollection<BookStore> BookStores { get; private set; }

        public MainWindowViewModel()
        {
            LoadBookStores();

            OpenAddStoreDialogCommand = new DelegateCommand(OpenAddStoreDialog);

        }

        private void OpenAddStoreDialog(object? obj)
        {
            var dialog = new AddStoreDialog();

            if (dialog.ShowDialog() == true)
            {
                var db = new BookStoreDbContext();

                var vm = dialog.AddStore;

                var store = new BookStore
                {
                    Name = vm.StoreName,
                    Street = vm.Street,
                    Zipcode = vm.ZipCode,
                    City = vm.City,
                    Country = vm.Country
                };

                db.BookStores.AddAsync(store);
                db.SaveChangesAsync();
                BookStores.Add(store);
            }

        }

        private void LoadBookStores()
        {
            using var db = new BookStoreDbContext();

            BookStores = new ObservableCollection<BookStore>
                (
                    db.BookStores.ToList()
                );

        }

    }
}
