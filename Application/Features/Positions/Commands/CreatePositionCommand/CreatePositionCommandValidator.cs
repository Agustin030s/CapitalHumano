using FluentValidation;

namespace Application.Features.Positions.Commands.CreatePositionCommand
{
    public class CreatePositionCommandValidator : AbstractValidator<CreatePositionCommand>
    {
        public CreatePositionCommandValidator()
        {
            RuleFor(p => p.Description)
                .NotEmpty().WithMessage("{PropertyName} no puede ser vacio");

            RuleFor(p => p.DepartamentId)
                .NotEmpty().WithMessage("{PropertyName} no puede ser vacio");

            RuleFor(p => p.GrossSalary)
                .NotEmpty().WithMessage("{PropertyName} no puede ser vacio");
        }
    }
}
