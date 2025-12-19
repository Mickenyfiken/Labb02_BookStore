using System.Collections.ObjectModel;
using System.IO;
using System.Reflection.Emit;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Labb02_BookStore.Domain;
using Labb02_BookStore.Infrastructure.Data.Model;
using Labb02_BookStore.Presentation.Dialogs;
using Labb02_BookStore.Presentation.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Labb02_BookStore
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainWindowViewModel viewModel;
        public MainWindow()
        {
            InitializeComponent();


            DataContext = new MainWindowViewModel();

            using var db = new BookStoreDbContext();

            var books = db.Books.Where(b => b.Title == b.Title).ToList();

            var booksCollection = new ObservableCollection<Book>(books);
            
            selectBookComboBox.ItemsSource = booksCollection;

            LoadBookStores();



            //Loaded += MainWindow_Loaded;

        }

        //private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        //{
        //    //LoadBookStores();
        //}

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Stores.SelectedItem is not null)
            {
                using var db = new BookStoreDbContext();



            }
        }

        private void Stores_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (e.NewValue is BookStore store)
            {
                using var db = new BookStoreDbContext();
                LoadInventory(store, db);
                storeDetailGrid.ItemsSource = new ObservableCollection<BookStore> { store };

            }
        }

        private void LoadStoreDetails(BookStore store, BookStoreDbContext db)
        {


            var storeDetail = db.BookStores
                .Where(sd => sd.Id == store.Id)
                .Select(sd => new
                {
                    Name = sd.Name,
                    Adress = sd.Street,
                    Zipcode = sd.Zipcode,
                    City = sd.City,
                    Country = sd.Country
                })
                .ToList();

            var collection = new ObservableCollection<object>(storeDetail);

            storeDetailGrid.ItemsSource = collection;

        }

        private void LoadInventory(BookStore store, BookStoreDbContext db)
        {

            var inventory = db.Inventories
                .Where(i => i.StoreId == store.Id)
                .Include(i => i.Isbn13Navigation)
                    .ThenInclude(b => b.Authors)
                .Include(i => i.Isbn13Navigation)
                    .ThenInclude(b => b.Publisher)
                .Include(i => i.Isbn13Navigation)
                    .ThenInclude(b => b.Categories)
                .AsNoTracking()
                .ToList();

            //var storeIsbns = db.Inventories
            //     .Where(i => i.StoreId == store.Id)
            //     .Select(i => i.Isbn13)
            //     .ToList();

            //var booksInInventory = db.Books
            //    .Where(b => storeIsbns.Contains(b.Isbn13))
            //    .Include(b => b.Authors)
            //    .ToList();

            var collection = new ObservableCollection<object>(inventory);

            myDataGrid.ItemsSource = collection;
        }

        private void LoadBookStores()
        {
            using var db = new BookStoreDbContext();

            var bookStores = db.BookStores
                .Include(bs => bs.Inventories)
                .ToList();

            Stores.ItemsSource = new ObservableCollection<BookStore>(bookStores);

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var dialog = new AddStoreDialog();

            dialog.Show();
        }
    }
}