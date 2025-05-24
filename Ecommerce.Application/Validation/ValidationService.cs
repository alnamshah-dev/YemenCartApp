using Ecommerce.Application.DTOs.Response;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Validation
{
    public class ValidationService : IValidationService
    {
        public async Task<ServiceResponse> ValidateAsync<T>(T model, IValidator<T> Validator)
        {
            var validationResult = await Validator.ValidateAsync(model);
            if (!validationResult.IsValid)
            {
                var errors=validationResult.Errors.Select(x => x.ErrorMessage).ToList();
                var errorToString=string.Join("; ", errors);
                return new ServiceResponse(message: errorToString);
            }
            return new ServiceResponse(Flag:true);
        }
    }
}
