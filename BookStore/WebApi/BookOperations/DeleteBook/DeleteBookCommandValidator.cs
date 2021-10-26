using System;
using FluentValidation;
using WebApi.BookOperations.DeleteBook;

namespace WebApi.BookOperations.CreateBook
{
    public class DeleteBookCommandValidator : AbstractValidator<DeleteBookCommand>{

        public DeleteBookCommandValidator(){
            RuleFor(command => command.bookId).GreaterThan(0);   
        }

    }

}

