using FluentValidation;

namespace Application.Features.Task.Commands.UpdateTaskCommand
{
    public class UpdateTaskCommandValidator : AbstractValidator<UpdateTaskCommand>
    {
        public UpdateTaskCommandValidator()
        {
            RuleFor(p => p.Id)
                .NotEmpty()
                .WithMessage("{PropertyName} no puede ser vacio");

            RuleFor(p => p.Description)
                .NotEmpty().WithMessage("{PropertyName} no puede ser vacio")
                .MaximumLength(80).WithMessage("{PropertyName} no debe exceder de {MaxLength} caracteres"); ;
        }
    }
}
