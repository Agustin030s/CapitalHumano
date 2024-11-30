using FluentValidation;

namespace Application.Features.Employees.Command.UpdateEmployeePositionCommand
{
    public class UpdateEmployeePositionCommandValidator : AbstractValidator<UpdateEmployeePositionCommand>
    {
        public UpdateEmployeePositionCommandValidator()
        {
            RuleFor(p => p.EmployeeId)
                .NotEmpty()
                .WithMessage("{PropertyName} no puede ser vacio");

            RuleFor(p => p.Observations)
                .MaximumLength(240).WithMessage("{PropertyName} no debe exceder de {MaxLength} caracteres.");
        }
    }
}
