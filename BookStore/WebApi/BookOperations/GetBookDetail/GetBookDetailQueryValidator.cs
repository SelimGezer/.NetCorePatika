using System;
using FluentValidation;
using WebApi.BookOperations.DeleteBook;
using WebApi.BookOperations.GetBookDetail;

namespace WebApi.BookOperations.GetBookDetail
{
    public class GetBookDetailQueryValidator : AbstractValidator<GetBookDetailQuery>{

        public GetBookDetailQueryValidator(){
            RuleFor(command => command.bookId).GreaterThan(0);   
        }

    }

}

