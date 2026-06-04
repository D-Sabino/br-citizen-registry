namespace BrCitizenRegistry.Api.Validators;

public static class CpfValidator
{
    public static string RemoveMask(string cpf)
    {
        return new string(cpf.Where(char.IsDigit).ToArray());
    }

    public static bool IsValid(string cpf)
    {
        var cleanCpf = RemoveMask(cpf);

        if (cleanCpf.Length != 11)
        {
            return false;
        }

        if (cleanCpf.Distinct().Count() == 1)
        {
            return false;
        }

        var cpfNumbers = cleanCpf.Select(character => int.Parse(character.ToString())).ToArray();

        var firstDigitSum = 0;

        for (var index = 0; index < 9; index++)
        {
            firstDigitSum += cpfNumbers[index] * (10 - index);
        }

        var firstDigitRemainder = firstDigitSum % 11;
        var firstVerifierDigit = firstDigitRemainder < 2 ? 0 : 11 - firstDigitRemainder;

        if (cpfNumbers[9] != firstVerifierDigit)
        {
            return false;
        }

        var secondDigitSum = 0;

        for (var index = 0; index < 10; index++)
        {
            secondDigitSum += cpfNumbers[index] * (11 - index);
        }

        var secondDigitRemainder = secondDigitSum % 11;
        var secondVerifierDigit = secondDigitRemainder < 2 ? 0 : 11 - secondDigitRemainder;

        return cpfNumbers[10] == secondVerifierDigit;
    }
}