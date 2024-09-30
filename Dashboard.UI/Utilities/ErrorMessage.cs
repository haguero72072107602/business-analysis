using FluentValidation.Results;
using MudBlazor;

namespace Dashboard.UI.Utilities;

public static class ShowMessageError
{
    public static void ShowError(this ISnackbar Snackbar, List<ValidationFailure> validationFailures)
    {
        foreach (var validationFailure in validationFailures)
        {
            Snackbar?.Add(validationFailure.ErrorMessage, Severity.Error);
        }
    }
}