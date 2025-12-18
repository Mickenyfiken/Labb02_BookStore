using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Labb02_BookStore.Domain;
using Labb02_BookStore.Infrastructure.Data.Model;

namespace Labb02_BookStore.Presentation.ViewModels
{
    class MainWindowViewModel : ViewModelBase
    {



        public ObservableCollection<string?> BookStores { get; private set; }

        public MainWindowViewModel()
        {
            LoadBookStores();
        }

        private void LoadBookStores()
        {
            using var db = new BookStoreDbContext();

            BookStores = new ObservableCollection<string?>
                (
                    db.BookStores.Select(bs => bs.Name).ToList()
                );

        }

    }
}
