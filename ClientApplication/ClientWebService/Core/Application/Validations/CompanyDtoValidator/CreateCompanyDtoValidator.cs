using ClientService.Application.Dtos.Company;
using FluentValidation;

namespace ClientService.Application.Validations.CompanyDtoValidator
{
    public class CreateCompanyDtoValidator:AbstractValidator<CreateCompanyDto>
    {
        public CreateCompanyDtoValidator()
        {
            RuleFor(x => x.MobilePhone).NotEmpty().WithMessage("{PropertyName} zorunlu bir alandır.").Must(a=>a.All(char.IsDigit)).WithMessage("{PropertyName} sadece rakamlardan oluşur.");
            RuleFor(x => x.CompanyName).NotEmpty().WithMessage("{PropertyName} zorunlu bir alandır.");
            RuleFor(x => x.Adres).NotEmpty().WithMessage("{PropertyName} zorunlu bir alandır.");
        }
    }
}
