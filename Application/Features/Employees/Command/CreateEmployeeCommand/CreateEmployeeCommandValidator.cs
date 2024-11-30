using FluentValidation;

namespace Application.Features.Employees.Command.CreateEmployeeCommand
{
    public class CreateEmployeeCommandValidator : AbstractValidator<CreateEmployeeCommand>
    {
        public CreateEmployeeCommandValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("{PropertyName} no puede ser vacio.")
                .MaximumLength(80).WithMessage("{PropertyName} no debe exceder de {MaxLength} caracteres.");

            RuleFor(p => p.LastName)
                .NotEmpty().WithMessage("{PropertyName} no puede ser vacio.")
                .MaximumLength(80).WithMessage("{PropertyName} no debe exceder de {MaxLength} caracteres.");

            RuleFor(p => p.Birthdate)
                .NotEmpty().WithMessage("{PropertyName} no puede ser vacio.");

            RuleFor(p => p.Phone)
                .MaximumLength(9).WithMessage("{PropertyName} no debe exceder de {MaxLength} caracteres.");

            RuleFor(p => p.Email)
                .NotEmpty().WithMessage("{PropertyName} no puede ser vacio.")
                .EmailAddress().WithMessage("{PropertyName} debe ser una direccion de email valida")
                .MaximumLength(320).WithMessage("{PropertyName} no debe exceder de {MaxLength} caracteres.");

            RuleFor(p => p.Address)
                .NotEmpty().WithMessage("{PropertyName} no puede ser vacio.")
                .MaximumLength(120).WithMessage("{PropertyName} no debe exceder de {MaxLength} caracteres.");

            RuleFor(p => p.PositionId)
                .NotEmpty()
                .WithMessage("{PropertyName} no puede ser vacio");
        }
    }
}
