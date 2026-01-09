using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using Labb02_BookStore.Presentation.ViewModels;

namespace Labb02_BookStore.Presentation.Dialogs
{
    /// <summary>
    /// Interaction logic for EditStoreDialog.xaml
    /// </summary>
    public partial class EditStoreDialog : Window
    {
        public StoreSetupViewModel EditStore { get; }
        public EditStoreDialog()
        {
            InitializeComponent();



            //EditStore = new StoreSetupViewModel(this);
            //DataContext = EditStore;
        }
    }
}
