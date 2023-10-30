using FluentValidation;

namespace Api.Contracts.Todos;

public class SaveTodoRequestValidator : AbstractValidator<SaveTodoRequest>
{
    public SaveTodoRequestValidator()
    {
        RuleFor(x => x.Title).NotEmpty().MinimumLength(2).MinimumLength(255);
    }
}
