using System;
using System.Linq;
using WebApi.DBOperations;

namespace WebApi.BookOperations.DeleteBook{
    public class DeleteBookCommand{
         private readonly BookStoreDbContext _dbContext;
        public int bookId;
        public DeleteBookCommand(BookStoreDbContext context){
            _dbContext=context;
        }

        public void Handle(){
            
            var book=_dbContext.Books.SingleOrDefault(x=>x.Id==bookId);
          
            if(book == null){
                throw new InvalidOperationException("Silinecek Kitap Bulumadi.");
            }

            _dbContext.Books.Remove(book);
            _dbContext.SaveChanges();

        }
    }
}