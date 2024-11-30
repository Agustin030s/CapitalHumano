using FluentValidation;

namespace Application.Features.Departaments.Commands.DeleteDepartamentCommand
{
    public class DeleteDepartamentCommandValidator : AbstractValidator<DeleteDepartamentCommand>
    {
        public DeleteDepartamentCommandValidator()
        {
            RuleFor(p => p.Id)
                .NotEmpty()
                .WithMessage("{PropertyName} no puede ser vacio");
        }
    }
}
