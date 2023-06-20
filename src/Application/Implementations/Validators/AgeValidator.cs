namespace Application.Implementations.Validators;

public static class AgeValidator
{
    private const int MinimumAge = 16;

    public static bool ValidateMinimumAge(this DateTime birthDate)
    {
        var currentDate = DateTime.Today;
        var minimumDateOfBirth = currentDate.AddYears(-MinimumAge);

        return birthDate <= minimumDateOfBirth;
    }
}