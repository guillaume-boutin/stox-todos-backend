using FluentValidation.Results;

namespace Application.Errors;

public class ValidationError : Exception
{
    private readonly List<ValidationFailure> _failures;

    public List<ValidationFailure> Failures => _failures;

    public ValidationError(List<ValidationFailure> failures)
        : base("One or more validation errors occurred.")
    {
        _failures = failures;
    }
}
