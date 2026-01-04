using Labb02_BookStore.Domain;
using Labb02_BookStore.Infrastructure.Data.Model;
using Labb02_BookStore.Presentation.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Labb02_BookStore.Presentation.ViewModels
{
    class MainWindowViewModel : ViewModelBase
    {
        public ICommand OpenBookEditWindowCommand { get; }
        public ICommand DeleteBookFromStoreCommand { get; }

        private readonly BookStoreDbContext _context;
        public Inventory SelectedBook { get; }


        public ObservableCollection<string?> BookStores { get; private set; }

        public MainWindowViewModel()
        {
            LoadBookStores();
            OpenBookEditWindowCommand = new DelegateCommand(OpenBookEditWindow);
            DeleteBookFromStoreCommand = new DelegateCommand(DeleteBookFromStore);
        }

        private void DeleteBookFromStore(object parameter)
        {
            var selectedBook = parameter as Inventory;
            MessageBoxResult result = MessageBox.Show(
            "Are you sure you want do Delete this book?",
            "Confirmation",
            MessageBoxButton.YesNo,
            MessageBoxImage.Question
);

            if (result == MessageBoxResult.Yes)
            {
                
            }
            else
            {
                
            }
        }


        private void OpenBookEditWindow(object parameter)
        {
            var selectedBook = parameter as Inventory;
            if (selectedBook == null) return;

            var db = new BookStoreDbContext();

           
            db.Attach(selectedBook);
            
            var vm = new BookEditViewModel(selectedBook, db);

            var window = new BookEditWindow
            {
                DataContext = vm
            };

            window.Show();
        }
        

        private void LoadBookStores()
        {
            using var db = new BookStoreDbContext();

            BookStores = new ObservableCollection<string?>
                (
                    db.BookStores.Select(bs => bs.Name).ToList()
                );

        }

    }
}
