using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Labb02_BookStore.Domain;

namespace Labb02_BookStore.Presentation.ViewModels
{
    class MainWindowViewModel : ViewModelBase
    {

        public ObservableCollection<BookStore> BookStores { get; set; }



    }
}
