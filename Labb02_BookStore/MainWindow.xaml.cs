using Labb02_BookStore.Domain;
using Labb02_BookStore.Infrastructure.Data.Model;
using Labb02_BookStore.Presentation;
using Labb02_BookStore.Presentation.Command;
using Labb02_BookStore.Presentation.ViewModels;
using Microsoft.EntityFrameworkCore;
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

namespace Labb02_BookStore
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {
     public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
            
            //using var db = new BookStoreDbContext();

            //var books = db.Books.ToList();

            LoadBookStores();
            //Loaded += MainWindow_Loaded;
        }
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //LoadBookStores();
        }

        private void Stores_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (e.NewValue is BookStore store)
            {
                using var db = new BookStoreDbContext();
                LoadInventory(store, db);
                LoadStoreDetails(store, db);
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
            var inventories = db.Inventories
             .Where(i => i.StoreId == store.Id)
             .Include(i => i.Isbn13Navigation) 
             .ToList();

            myDataGrid.ItemsSource =
                   new ObservableCollection<Inventory>(inventories);
        }

        private void LoadBookStores()
        {
            using var db = new BookStoreDbContext();

            var bookStores = db.BookStores
                .Include(bs => bs.Inventories)
                .ToList();

            Stores.ItemsSource = new ObservableCollection<BookStore>(bookStores);
        }
     }
}