using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace snglrtycrvtureofspce.Core.Filters;

public class RequestValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators) :
    IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    public Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var context = new ValidationContext<TRequest>(request);
        
        var list = validators
            .Select((Func<IValidator<TRequest>, ValidationResult>) (v => v.Validate(context)))
            .SelectMany((Func<ValidationResult, IEnumerable<ValidationFailure>>) (result => result.Errors))
            .Where((Func<ValidationFailure, bool>) (_ => true))
            .ToList();
        
        if (list.Count != 0)
        {
            throw new ValidationException(list);
        }
        
        return next();
    }
}