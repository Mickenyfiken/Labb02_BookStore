using Labb02_BookStore.Domain;
using Labb02_BookStore.Infrastructure.Data.Model;
using Labb02_BookStore.Presentation.Command;
using Microsoft.EntityFrameworkCore;
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
        private Inventory _selectedBook;
        public Inventory SelectedBook
        {
            get => _selectedBook;
            set
            {
                _selectedBook = value;
                RaisePropertyChanged();
            }
        }
        public ObservableCollection<Inventory> Books { get; private set; }

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
            
            if (selectedBook == null)
            {
                MessageBox.Show("No book selected.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            MessageBoxResult result = MessageBox.Show(
                "Are you sure you want to delete this book?",
                "Confirmation",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question
            );

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    using var db = new BookStoreDbContext();

                    db.Attach(selectedBook);
                    db.Inventories.Remove(selectedBook);
                    db.SaveChanges();
                    
                    Books.Remove(selectedBook);
                    var grid = Application.Current.MainWindow.FindName("myDataGrid");
                   
                    MessageBox.Show("Book deleted successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                    catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting book: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        public void LoadInventoryForStore(BookStore store)
        {
            using var db = new BookStoreDbContext();

            Books = new ObservableCollection<Inventory>(
                db.Inventories
                  .Where(i => i.StoreId == store.Id)
                  .Include(i => i.Isbn13Navigation)
                  .ToList());

            RaisePropertyChanged(nameof(Books));
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

            Books = new ObservableCollection<Inventory>(db.Inventories.ToList());

            BookStores = new ObservableCollection<string?>
                (
                    db.BookStores.Select(bs => bs.Name).ToList()
                );

        }

    }
}