using System;
using System.Linq;
using AutoMapper;
using WebApi.DBOperations;

namespace WebApi.BookOperations.CreateBook{

    public class CreateBookCommand{
        
        public CreateBookModel Model {get;set;}

        private readonly BookStoreDbContext _dbContext;
        IMapper _mapper;
        public CreateBookCommand(BookStoreDbContext context, IMapper mapper)
        {
            _dbContext = context;
            _mapper = mapper;
        }

        public void Handle(){
            var book = _dbContext.Books.SingleOrDefault(x=> x.Title== Model.Title);

             if(book!=null){
                 throw new InvalidOperationException("Kitap Zaten Mevcut");
             }else{
                 
                 book=_mapper.Map<Book>(Model);        //new Book();
               /*  book.Title=Model.Title;
                 book.PublishDate=Model.PublishDate;
                 book.PageCount=Model.PageCount;
                 book.GenreId=Model.GenreId;*/

                 _dbContext.Books.Add(book);
                 _dbContext.SaveChanges();                
        }
    }

    public class CreateBookModel { //sadece model olması büyük ihtimal dto anlamına geliyor

        public string Title { get; set; }
        public int GenreId { get; set; }

        public int PageCount { get; set; }

        public DateTime PublishDate { get; set; } 

    }

}

}