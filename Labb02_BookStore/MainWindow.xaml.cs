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
            DataContext = this;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //LoadAuthors();
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