using System;
using System.Collections.Generic;
using System.Linq;
using WebApi.Common;
using WebApi.DBOperations;

namespace WebApi.BookOperations.GetBooks
{
    public class GetBooksQuery{

        private readonly BookStoreDbContext _dbContext;
        public GetBooksQuery(BookStoreDbContext dbContext){
            _dbContext=dbContext;
        }

        public List<BooksViewModel> Handle(){
            var bookList=_dbContext.Books.OrderBy(x=>x.Id).ToList<Book>();
            List<BooksViewModel> vm = new List<BooksViewModel>();
            foreach (var book in bookList)
            {
                vm.Add(new BooksViewModel(){
                    Title=book.Title,
                    Genre= ((GenreEnum)book.GenreId).ToString(),
                    PublishDate = book.PublishDate.Date.ToString("dd/MM/yyy"),
                    PageCount=book.PageCount
                });
            }
            return vm;   
        }

        public BooksViewModel GetById(int id){
            var book=_dbContext.Books.Where(book=>book.Id==id).SingleOrDefault();
            BooksViewModel booksViewModel=new BooksViewModel(){
                Title=book.Title,
                PageCount=book.PageCount,
                Genre=((GenreEnum)book.GenreId).ToString(),
                PublishDate=book.PublishDate.ToString()
            };
            booksViewModel.Title=book.Title;

            return booksViewModel;
        }


    }

    public class BooksViewModel{//bunun adının yanında vievmodel olması bunun uı tarafından kullanılacağını ifade ediyormuş
        public string Title { get; set; }

        public int PageCount { get; set; }

        public string PublishDate { get; set; }

        public string Genre { get; set; }
    }
}