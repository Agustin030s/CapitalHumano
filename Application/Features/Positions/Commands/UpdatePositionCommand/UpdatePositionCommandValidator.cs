using FluentValidation;

namespace Application.Features.Positions.Commands.UpdatePositionCommand
{
    public class UpdatePositionCommandValidator : AbstractValidator<UpdatePositionCommand>
    {
        public UpdatePositionCommandValidator()
        {
            RuleFor(p => p.Id)
                .NotEmpty()
                .WithMessage("{PropertyName} no puede ser vacio");

            RuleFor(p => p.Description)
                .NotEmpty().WithMessage("{PropertyName} no puede ser vacio")
                .MaximumLength(80).WithMessage("{PropertyName} no debe exceder de {MaxLength} caracteres");

            RuleFor(p => p.GrossSalary)
                .NotEmpty()
                .WithMessage("{PropertyName} no puede ser vacio");
        }
    }
}
