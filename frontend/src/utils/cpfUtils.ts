export const removeCpfMask = (cpf: string): string => {
    return cpf.replace(/\D/g, '');
};

export const formatCpf = (cpf: string): string => {
    const onlyNumbers = removeCpfMask(cpf).slice(0, 11);

    return onlyNumbers
        .replace(/^(\d{3})(\d)/, '$1.$2')
        .replace(/^(\d{3})\.(\d{3})(\d)/, '$1.$2.$3')
        .replace(/\.(\d{3})(\d)/, '.$1-$2');
};

export const isValidCpf = (cpf: string): boolean => {
    const cleanCpf = removeCpfMask(cpf);

    if (cleanCpf.length !== 11) {
        return false;
    }

    const hasAllEqualDigits = /^(\d)\1+$/.test(cleanCpf);

    if (hasAllEqualDigits) {
        return false;
    }

    const cpfNumbers = cleanCpf.split('').map(Number);

    const firstDigitSum = cpfNumbers
        .slice(0, 9)
        .reduce((sum, number, index) => {
            return sum + number * (10 - index);
        }, 0);

    const firstDigitRemainder = firstDigitSum % 11;
    const firstVerifierDigit = firstDigitRemainder < 2 ? 0 : 11 - firstDigitRemainder;

    if (cpfNumbers[9] !== firstVerifierDigit) {
        return false;
    }

    const secondDigitSum = cpfNumbers
        .slice(0, 10)
        .reduce((sum, number, index) => {
            return sum + number * (11 - index);
        }, 0);

    const secondDigitRemainder = secondDigitSum % 11;
    const secondVerifierDigit = secondDigitRemainder < 2 ? 0 : 11 - secondDigitRemainder;

    return cpfNumbers[10] === secondVerifierDigit;
};