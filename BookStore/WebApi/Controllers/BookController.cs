using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using WebApi.BookOperations.CreateBook;
using WebApi.BookOperations.DeleteBook;
using WebApi.BookOperations.GetBookDetail;
using WebApi.BookOperations.GetBooks;
using WebApi.BookOperations.UpdateBook;
using WebApi.DBOperations;
using static WebApi.BookOperations.CreateBook.CreateBookCommand;
using static WebApi.BookOperations.UpdateBook.UpdateBookCommand;

namespace WebApi.AddControllers{
    
    [ApiController]
    [Route("[controller]s")]
    public class BookController:ControllerBase
    {/*
         private static List<Book> BookList=new List<Book>(){

            new Book{
                Id=1,
                Title="Lean Startup",
                GenreId=1,
                PageCount=200,
                PublishDate=new System.DateTime(2001,06,22) 
            },
             new Book{
                Id=2,
                Title="Herland",
                GenreId=2,
                PageCount=250,
                PublishDate=new System.DateTime(2010,05,23) 
            },
             new Book{
                Id=3,
                Title="Dune",
                GenreId=2,
                PageCount=540,
                PublishDate=new System.DateTime(2001,12,21) 
            }
         };*/


         private readonly BookStoreDbContext _context;
        IMapper _mapper;
        public BookController(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
         public IActionResult GetBooks(){
           /* var bookList=_context.Books.OrderBy(x=>x.Id).ToList<Book>();
            return bookList;*/
            GetBooksQuery query =new GetBooksQuery(_context,_mapper);
            var result=query.Handle();
            return Ok(result);
         }

        [HttpGet("{id}")]
        public IActionResult GetById(int id){
           // var bookList=_context.Books.Where(bookList=>bookList.Id==id).SingleOrDefault();
           // return bookList;
            BookDetailViewModel booksViewModel;
            try
            {
                 GetBookDetailQuery query=new GetBookDetailQuery(_context,_mapper);
                 query.bookId=id;
                 GetBookDetailQueryValidator validationRules=new GetBookDetailQueryValidator();
                 validationRules.ValidateAndThrow(query);
                 booksViewModel=query.Handle();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
            return Ok(booksViewModel);
         }


       /*  [HttpGet]
        public Book getFromQuery([FromQuery] int id){
            var bookList=BookList.Where(bookList=>bookList.Id==id).SingleOrDefault();
            return bookList;
         }*/

         [HttpPost]
         public IActionResult AddBook([FromBody] CreateBookModel newBook){
         //  var book = _context.Books.SingleOrDefault(x=> x.Title== newBook.Title);
            
            try
            {
            CreateBookCommand command=new CreateBookCommand(_context,_mapper);
            command.Model=newBook;

            CreateBookCommandValidator validationRules=new CreateBookCommandValidator();
           // ValidationResult result = validationRules.Validate(command); //eğer burda hata yı dönmek istersek VelidateAndThrow kullanılır
            validationRules.ValidateAndThrow(command);
            command.Handle();

           // if(!result.IsValid){
                /*foreach (var item in result.Errors)//Kendimiz görmek istersek
                                {
                    Console.WriteLine("Hangi degiken:"+item.PropertyName+" Error Messsage: "+item.ErrorMessage);
                }*/ 
               // return BadRequest(result.Errors); //böyle de dönülebilir 
          /*  }else{
                command.Handle();
                 }*/

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
         }

         [HttpPut("{id}")]
         public IActionResult UpdateBook(int id,[FromBody] UpdateBookModel updatedBook){
             
           /*  var arananKitap=_context.Books.SingleOrDefault(x=>x.Id==id);
             if(arananKitap!=null){
                 arananKitap.GenreId=updatedBook.GenreId!=default ? updatedBook.GenreId :arananKitap.GenreId;
                 arananKitap.PageCount=updatedBook.PageCount!=default ? updatedBook.PageCount :arananKitap.PageCount;
                 arananKitap.PublishDate=updatedBook.PublishDate!=default ? updatedBook.PublishDate :arananKitap.PublishDate;
                 arananKitap.Title=updatedBook.Title!=default ? updatedBook.Title :arananKitap.Title;

                _context.SaveChanges();
                return Ok();

             }else{
                 return BadRequest();
             }*/
            try
            {
            UpdateBookCommand command=new UpdateBookCommand(_context);
            command.UpdateModel=updatedBook;
            command.bookId=id;
            UpdateBookCommandValidator validationRules =new UpdateBookCommandValidator();
            validationRules.ValidateAndThrow(command);

            command.Handle();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();

         }

         [HttpDelete("{id}")]

         public IActionResult DeleteBook(int id){

            try
            {
                  DeleteBookCommand deleteBookCommand=new DeleteBookCommand(_context);
                  DeleteBookCommandValidator validationRules=new DeleteBookCommandValidator();
                  deleteBookCommand.bookId=id;
                  validationRules.ValidateAndThrow(deleteBookCommand);
                  deleteBookCommand.Handle();
            }
            catch (Exception ex)
            {
                
                return BadRequest(ex.Message);
            }

           
            return Ok();

         }
    }
}