using FluentValidation;

namespace Application.Features.Positions.Commands.AddSkillToPositionCommand
{
    public class AddSkillToPositionCommandValidator : AbstractValidator<AddSkillToPositionCommand>
    {
        public AddSkillToPositionCommandValidator()
        {
            RuleFor(p => p.PositionId)
                .NotEmpty()
                .WithMessage("{PropertyName} no puede ser vacio");

            RuleFor(p => p.SkillId)
                .NotEmpty()
                .WithMessage("{PropertyName} no puede ser vacio");
        }
    }
}
