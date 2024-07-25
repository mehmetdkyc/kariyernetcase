using BusinessLayer.Dtos;
using FluentValidation;

namespace BusinessLayer.Validators.JobValidator
{
    public class CreateJobDtoValidator : AbstractValidator<JobInsertDto>
    {
        public CreateJobDtoValidator()
        {
            RuleFor(x => x.Role).NotEmpty().WithMessage("{PropertyName} zorunlu bir alandır.");
            RuleFor(x => x.JobDescription).NotEmpty().WithMessage("{PropertyName} zorunlu bir alandır.");
        }
    }
}
