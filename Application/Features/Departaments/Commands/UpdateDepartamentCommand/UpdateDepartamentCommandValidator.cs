using FluentValidation;

namespace Application.Features.Departaments.Commands.UpdateDepartamentCommand
{
    public class UpdateDepartamentCommandValidator : AbstractValidator<UpdateDepartamentCommand>
    {
        public UpdateDepartamentCommandValidator()
        {
            RuleFor(p => p.DepartamentCode)
                .NotEmpty().WithMessage("{PropertyName} no puede ser vacio")
                .MaximumLength(80).WithMessage("{PropertyName} no debe exceder de {MaxLength} caracteres");

            RuleFor(p => p.Description)
                .NotEmpty().WithMessage("{PropertyName} no puede ser vacio")
                .MaximumLength(80).WithMessage("{PropertyName} no debe exceder de {MaxLength} caracteres");
        }
    }
}
