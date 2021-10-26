using System;
using FluentValidation;
using WebApi.BookOperations.DeleteBook;

namespace WebApi.BookOperations.UpdateBook
{
    public class UpdateBookCommandValidator : AbstractValidator<UpdateBookCommand>{

        public UpdateBookCommandValidator(){
            RuleFor(command => command.bookId).GreaterThan(0);
            RuleFor(command => command.UpdateModel.GenreId).GreaterThan(0);
            RuleFor(command => command.UpdateModel.PublishDate.Date).LessThan(DateTime.Now.Date);
            RuleFor(command => command.UpdateModel.Title.Length).GreaterThan(2);   
        }

    }

}
