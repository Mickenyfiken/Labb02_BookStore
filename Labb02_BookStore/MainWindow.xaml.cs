using System.Collections.ObjectModel;
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
using Labb02_BookStore.Models;
using Microsoft.EntityFrameworkCore;

namespace Labb02_BookStore
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();

            Loaded += MainWindow_Loaded;

        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadBookStores();
        }

        private void Stores_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (e.NewValue is BookStore store)
            {
                LoadBooks(store);
            }
        }

        private void LoadBooks(BookStore store)
        {
            using var db = new BookStoreDbContext();

            var books = db.Inventories
                .Where(i => i.StoreId == store.Id)
                .ToList();

            var collection = new ObservableCollection<object>(books);

            myDataGrid.ItemsSource = collection;
        }

        private void LoadBookStores()
        {
            using var db = new BookStoreDbContext();

            var bookStores = db.BookStores.ToList();

            Stores.ItemsSource = new ObservableCollection<BookStore>(bookStores);
                
        }

        //private void LoadAuthors()
        //{
        //    using var db = new BookStoreDbContext();

        //    var authors = db.Authors
        //         .Where(authors => authors.Firstname != null)
        //         .ToList();


        //    myTreeView.ItemsSource = new ObservableCollection<Author>(authors);

        //}


    }
}