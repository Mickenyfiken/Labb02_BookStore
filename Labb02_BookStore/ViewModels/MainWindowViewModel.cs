using Labb02_BookStore.Domain;
using Labb02_BookStore.Infrastructure.Data.Model;
using Labb02_BookStore.Presentation.Command;
using Labb02_BookStore.Presentation.Command;
using Labb02_BookStore.Presentation.Dialogs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection.Metadata;
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
        public ICommand OpenAddStoreDialogCommand { get; }
        public ICommand OpenEditStoreDialogCommand { get; }
        public ICommand DeleteStoreCommand { get; }

        public ICommand AddBookToSelectedStoreCommand { get; }

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


        private Book _selectedBookToAdd;
        public Book selectedBookToAdd
        {
            get => _selectedBookToAdd;
            set
            {
                _selectedBookToAdd = value;
                RaisePropertyChanged();
            }
        }
        public ObservableCollection<Inventory> Books { get; private set; }

        //public ObservableCollection<string?> BookStores { get; private set; }

        public MainWindowViewModel()
        {
            LoadBookStores();
            OpenAddStoreDialogCommand = new DelegateCommand(OpenAddStoreDialog);
            OpenEditStoreDialogCommand = new DelegateCommand(OpenEditStoreDialog);
            DeleteStoreCommand = new DelegateCommand(DeleteStore);
            OpenBookEditWindowCommand = new DelegateCommand(OpenBookEditWindow);
            DeleteBookFromStoreCommand = new DelegateCommand(DeleteBookFromStore);
            AddBookToSelectedStoreCommand = new DelegateCommand(AddBookToSelectedStore);
        }



        private void AddBookToSelectedStore(object parameter)
        {
            using var db = new BookStoreDbContext();

            var book = selectedBookToAdd;

            var bookstore = db.BookStores
                .Include(b => b.Inventories)
                .ThenInclude(i => i.Isbn13Navigation)
                .FirstOrDefault(b => b.Id == SelectedStore.Id);

            var existing = bookstore.Inventories.FirstOrDefault(inv => inv.Isbn13 == book.Isbn13);
            if (existing != null)
            {
                //existing.Balance += 1; // increment quantity
                MessageBox.Show("Book already in Store, update quantity");
                return;
            }

            bookstore.Inventories.Add(new Inventory()
            {
                    Isbn13 = selectedBookToAdd.Isbn13,
                    Balance = 1
            });
                db.SaveChanges();

                LoadInventoryForStore(bookstore);



            LoadInventoryForStore(bookstore);
        }

        private async void DeleteStore(object? obj)
        {
            using var db = new BookStoreDbContext();

            var bookstore = db.BookStores
                .Include(b => b.Inventories)
                .FirstOrDefault(b => b.Id == SelectedStore.Id);

            if (bookstore.Inventories.Count > 0)
            {
                MessageBox.Show("Store must be empty");
                return;
            }

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
            //using var db = new BookStoreDbContext();

            //Books = new ObservableCollection<Inventory>(db.Inventories.ToList());

            //BookStores = new ObservableCollection<string>
            //    (
            //        db.BookStores.Select(bs => bs.Name).ToList()
            //    );

            using var db = new BookStoreDbContext();
            BookStores = new ObservableCollection<BookStore>(db.BookStores.ToList());

        }

    }
}