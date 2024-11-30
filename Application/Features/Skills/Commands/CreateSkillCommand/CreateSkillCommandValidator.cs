using FluentValidation;

namespace Application.Features.Skills.Commands.CreateSkillCommand
{
    public class CreateSkillCommandValidator : AbstractValidator<CreateSkillCommand>
    {
        public CreateSkillCommandValidator()
        {
            RuleFor(p => p.Description)
                .NotEmpty().WithMessage("{PropertyName} no puede ser vacio")
                .MaximumLength(80).WithMessage("{PropertyName} no debe exceder de {MaxLength} caracteres");
        }
    }
}
