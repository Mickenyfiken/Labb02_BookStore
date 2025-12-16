using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

namespace Labb02_BookStore.Views
{
    /// <summary>
    /// Interaction logic for StoreView.xaml
    /// </summary>
    public partial class StoreView : UserControl
    {
        public StoreView()
        {
            InitializeComponent();
        }

        private void StoreWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadAllStores();
        }

        private void LoadAllStores()
        {
            using var db = new BookStoreDbContext();

            var stores = db.BookStores
                 .Where(stores => stores.Name != null)
                 .ToList();


            //myListBox.ItemsSource = new ObservableCollection<BookStore>(stores);

        }
    }
}
