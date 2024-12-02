using FluentValidation;

namespace Application.Features.Skills.Commands.DeleteSkillCommand
{
    public class DeleteSkillCommandValidator : AbstractValidator<DeleteSkillCommand>
    {
        public DeleteSkillCommandValidator()
        {
            RuleFor(p => p.Id)
                .NotEmpty()
                .WithMessage("{PropertyName} no puede ser vacio");
        }
    }
}
