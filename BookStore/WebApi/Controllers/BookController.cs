using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebApi.BookOperations.CreateBook;
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

         public BookController(BookStoreDbContext context){
             _context=context;
         }

        [HttpGet]
         public IActionResult GetBooks(){
           /* var bookList=_context.Books.OrderBy(x=>x.Id).ToList<Book>();
            return bookList;*/
            GetBooksQuery query =new GetBooksQuery(_context);
            var result=query.Handle();
            return Ok(result);
         }

        [HttpGet("{id}")]
        public IActionResult GetById(int id){
           // var bookList=_context.Books.Where(bookList=>bookList.Id==id).SingleOrDefault();
           // return bookList;

            GetBooksQuery query=new GetBooksQuery(_context);
            return Ok(query.GetById(id));
         }


       /*  [HttpGet]
        public Book getFromQuery([FromQuery] int id){
            var bookList=BookList.Where(bookList=>bookList.Id==id).SingleOrDefault();
            return bookList;
         }*/

         [HttpPost]
         public IActionResult AddBook([FromBody] CreateBookModel newBook){
           var book = _context.Books.SingleOrDefault(x=> x.Title== newBook.Title);
            
            try
            {
            CreateBookCommand command=new CreateBookCommand(_context);
            command.Model=newBook;
            command.Handle();
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
            command.Handle(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();

         }

         [HttpDelete("{id}")]

         public IActionResult DeleteBook(int id){


            var book= _context.Books.SingleOrDefault(x=> x.Id==id);

            if(book is null){
                return BadRequest();
            }

            _context.Books.Remove(book);
            _context.SaveChanges();
            return Ok();

           /*  KeyValuePair<bool,int> arananKitap=new KeyValuePair<bool, int>(false,0);
             for (int i = 0; i < BookList.Count; i++)
             {
                if(id == BookList[i].Id){
                    arananKitap=new KeyValuePair<bool, int>(true,i);
                    break;
                }   
             }

             if(arananKitap.Key){
                 BookList.RemoveAt(arananKitap.Value);
                 return Ok();
             }else{
                 return BadRequest();
             }*/


         }
    }
}