using FluentValidation;

namespace Application.Features.Task.Commands.CreateTaskCommand
{
    public class CreateTaskCommandValidator : AbstractValidator<CreateTaskCommand>
    {
        public CreateTaskCommandValidator()
        {
            RuleFor(p => p.Description)
                .NotEmpty().WithMessage("{PropertyName} no puede ser vacio")
                .MaximumLength(80).WithMessage("{PropertyName} no debe exceder de {MaxLength} caracteres"); ;

            RuleFor(p => p.PositionId)
                .NotEmpty().WithMessage("{PropertyName} no puede ser vacio");
        }
    }
}
