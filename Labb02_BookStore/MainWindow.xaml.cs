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
            LoadBookStores();
            using var db = new BookStoreDbContext();

            var books = db.Books.Where(b => b.Title == b.Title).ToList();

            var booksCollection = new ObservableCollection<Book>(books);

            

            selectBookComboBox.ItemsSource = booksCollection;

            //using var db = new BookStoreDbContext();

            //var books = db.Books.ToList();


            //Loaded += MainWindow_Loaded;
        }
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //LoadBookStores();
        }

        //private void Button_Click(object sender, RoutedEventArgs e)
        //{
        //    myDataGrid.Visibility =
        //        myDataGrid.Visibility == Visibility.Visible
        //        ? Visibility.Collapsed
        //        : Visibility.Visible;
        //}



        private void Stores_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (e.NewValue is BookStore store &&
                DataContext is MainWindowViewModel vm)
            {
                vm.LoadInventoryForStore(store);
                vm.SelectedStore = e.NewValue as BookStore;
                using var db = new BookStoreDbContext();
                LoadStoreDetails(store, db);
                storeDetailGrid.ItemsSource = new ObservableCollection<BookStore> { store };
            }
            
        }

        //private void Stores_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        //{
        //    if (e.NewValue is BookStore store)
        //    {
        //        using var db = new BookStoreDbContext();
        //        LoadInventory(store, db);
        //        LoadStoreDetails(store, db);
        //    }
        //}

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

        //private void loadinventory(bookstore store, bookstoredbcontext db)
        //{
        //    var inventories = db.inventories
        //     .where(i => i.storeid == store.id)
        //     .include(i => i.isbn13navigation)
        //     .tolist();

        //    mydatagrid.itemssource =
        //           new observablecollection<inventory>(inventories);
        //}

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