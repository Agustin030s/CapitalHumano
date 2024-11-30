using FluentValidation;

namespace Application.Features.Employees.Command.DeleteEmployeeCommand
{
    public class DeleteEmployeeCommandValidator : AbstractValidator<DeleteEmployeeCommand>
    {
        public DeleteEmployeeCommandValidator()
        {
            RuleFor(p => p.Id)
                .NotEmpty()
                .WithMessage("{PropertyName} no puede ser vacio");
        }
    }
}
