using Labb02_BookStore.Domain;
using Labb02_BookStore.Infrastructure.Data.Model;
using Labb02_BookStore.Presentation.Command;
using System.Windows.Input;

namespace Labb02_BookStore.Presentation.ViewModels
{
    class BookEditViewModel : ViewModelBase
    {
        private readonly BookStoreDbContext _context;

        public Inventory SelectedBook { get; }

        public ICommand SaveBookCommand { get; }

        public BookEditViewModel(Inventory book, BookStoreDbContext context)
        {
            SelectedBook = book;
            _context = context;

            SaveBookCommand = new DelegateCommand(SaveEditedBook);
        }

        private void SaveEditedBook(object obj)
        {
            _context.SaveChanges();
        }
    }
}
