using BookStore.DBOperations;
using BookStore.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Application.AuthorOperations.Commands.DeleteAuthor
{
    public class DeleteAuthorCommand
    {
        private readonly IBookStoreDbContext _context;
        public int AuthorIdDto;
        
        public DeleteAuthorCommand(IBookStoreDbContext context)
        {
            _context = context;
            
        }

        public void Handle()
        {
            var author = _context.Authors.Include(x => x.Books).SingleOrDefault(x => x.Id == AuthorIdDto);
            if (author == null)
            {
                throw new InvalidOperationException("Silmek istediğiniz Id'e ait yazar yok");

            }

            if (_context.Books.Where(x => x.AuthorId == AuthorIdDto).Any())
            { 
                throw new InvalidOperationException("Silmek istediğiniz yazarın yayında kitabı var önce kitabı silmelisiniz");
            }
            _context.Authors.Remove(author);
            _context.SaveChanges();
          
        }



    }
}
