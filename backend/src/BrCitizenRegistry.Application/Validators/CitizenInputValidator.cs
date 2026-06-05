using System.Text.RegularExpressions;

namespace BrCitizenRegistry.Application.Validators;

public static partial class CitizenInputValidator
{
    private static readonly string[] ForbiddenPatterns =
    [
        "<",
        ">",
        "{",
        "}",
        "[",
        "]",
        "(",
        ")",
        ";",
        "=",
        "--",
        "/*",
        "*/",
        "script",
        "javascript:",
        "onerror",
        "onload",
        "select ",
        "insert ",
        "update ",
        "delete ",
        "drop ",
        "alter ",
        "truncate ",
        "union "
    ];

    public static bool IsValidFullName(string fullName)
    {
        var normalizedName = fullName.Trim();

        if (string.IsNullOrWhiteSpace(normalizedName))
        {
            return false;
        }

        if (normalizedName.Length < 3 || normalizedName.Length > 150)
        {
            return false;
        }

        if (ContainsForbiddenPattern(normalizedName))
        {
            return false;
        }

        return FullNameRegex().IsMatch(normalizedName);
    }

    public static bool IsValidSearchTerm(string term)
    {
        var normalizedTerm = term.Trim();

        if (string.IsNullOrWhiteSpace(normalizedTerm))
        {
            return false;
        }

        if (normalizedTerm.Length > 150)
        {
            return false;
        }

        if (ContainsForbiddenPattern(normalizedTerm))
        {
            return false;
        }

        return SearchTermRegex().IsMatch(normalizedTerm);
    }

    private static bool ContainsForbiddenPattern(string value)
    {
        var normalizedValue = value.ToLowerInvariant();

        return ForbiddenPatterns.Any(normalizedValue.Contains);
    }

    [GeneratedRegex(@"^[A-Za-zÀ-ÖØ-öø-ÿ\s'-]+$")]
    private static partial Regex FullNameRegex();

    [GeneratedRegex(@"^[A-Za-zÀ-ÖØ-öø-ÿ0-9\s.\-']+$")]
    private static partial Regex SearchTermRegex();
}