using System;
using System.Linq;
using WebApi.DBOperations;

namespace WebApi.BookOperations.UpdateBook
{
    public class UpdateBookCommand{

        public UpdateBookModel UpdateModel {get;set;}
        private readonly BookStoreDbContext _dbContext;
        public int bookId;
        public UpdateBookCommand(BookStoreDbContext context){
            _dbContext=context;
        }

        public void Handle(){
                 var book=_dbContext.Books.SingleOrDefault(x=>x.Id==bookId);

                 
             if(book==null){
                 throw new InvalidOperationException("Kitap Bulunamadi!");
             }else{
                 
                /* book.Title=Model.Title;
                 book.PublishDate=Model.PublishDate;
                 book.PageCount=Model.PageCount;
                 book.GenreId=Model.GenreId;*/

                 book.GenreId=UpdateModel.GenreId!=default ? UpdateModel.GenreId :book.GenreId;
                 book.PageCount=UpdateModel.PageCount!=default ? UpdateModel.PageCount :book.PageCount;
                 book.PublishDate=UpdateModel.PublishDate!=default ? UpdateModel.PublishDate :book.PublishDate;
                 book.Title=UpdateModel.Title!=default ? UpdateModel.Title :book.Title;

                 _dbContext.SaveChanges();                
                 }
        }

        public class UpdateBookModel { //sadece model olması büyük ihtimal dto anlamına geliyor

                public string Title { get; set; }
                public int GenreId { get; set; }

                public int PageCount { get; set; }

                public DateTime PublishDate { get; set; } 

            }   
    } 

   
}