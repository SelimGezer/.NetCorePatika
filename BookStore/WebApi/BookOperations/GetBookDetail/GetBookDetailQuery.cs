using System;
using System.Linq;
using AutoMapper;
using WebApi.DBOperations;

namespace WebApi.BookOperations.GetBookDetail{

    public class GetBookDetailQuery{
            private readonly BookStoreDbContext _dbContext;

            IMapper _mapper;
            public int bookId;
        public GetBookDetailQuery(BookStoreDbContext context, IMapper mapper)
        {
            _dbContext = context;
            _mapper = mapper;
        }

        public BookDetailViewModel Handle(){
                 var book=_dbContext.Books.Where(book=>book.Id==bookId).SingleOrDefault();

                if(book==null){
                    throw new InvalidOperationException("Kitap Bulunamadı!");
                }

                BookDetailViewModel booksViewModel=_mapper.Map<BookDetailViewModel>(book); //new BooksViewModel(){
                /*  Title=book.Title,
                    PageCount=book.PageCount,
                    Genre=((GenreEnum)book.GenreId).ToString(),
                    PublishDate=book.PublishDate.ToString()*/
                //};

                return booksViewModel;
            }
    }

    public class BookDetailViewModel{
        public string Title {get;set;}
        public string Genre {get;set;}
        public int PageCount { get; set; }
        public string  PublishDate { get; set; }
    }

}