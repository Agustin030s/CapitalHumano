using FluentValidation;

namespace Application.Features.Positions.Commands.DeletePositionCommand
{
    public class DeletePositionCommandValidator : AbstractValidator<DeletePositionCommand>
    {
        public DeletePositionCommandValidator()
        {
            RuleFor(p => p.Id)
                .NotEmpty()
                .WithMessage("{PropertyName} no puede ser vacio");
        }
    }
}
