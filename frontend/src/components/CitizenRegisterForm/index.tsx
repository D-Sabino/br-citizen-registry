import { useState } from 'react';
import type { Citizen } from '../../types/Citizen';
import { formatCpf } from '../../utils/cpfUtils';

interface CitizenRegisterFormProps {
    onRegisterCitizen: (citizen: Omit<Citizen, 'id'>) => void;
}

const CitizenRegisterForm = ({ onRegisterCitizen }: CitizenRegisterFormProps) => {
    const [fullName, setFullName] = useState('');
    const [cpf, setCpf] = useState('');

    const handleCpfChange = (value: string) => {
        setCpf(formatCpf(value));
    };

    const handleSubmit = (event: React.FormEvent<HTMLFormElement>) => {
        event.preventDefault();

        if (!fullName.trim() || !cpf.trim()) {
            return;
        }

        onRegisterCitizen({
            fullName: fullName.trim(),
            cpf
        });

        setFullName('');
        setCpf('');
    };

    return (
        <div className="form-container sign-up">
            <form onSubmit={handleSubmit}>
                <h1>Cadastrar Cidadão</h1>

                <span>Preencha os dados obrigatórios do cidadão brasileiro</span>

                <input
                    type="text"
                    placeholder="Nome completo"
                    value={fullName}
                    onChange={(event) => setFullName(event.target.value)}
                    required
                />

                <input
                    type="text"
                    placeholder="CPF"
                    value={cpf}
                    onChange={(event) => handleCpfChange(event.target.value)}
                    maxLength={14}
                    required
                />

                <button type="submit">
                    Cadastrar
                </button>
            </form>
        </div>
    );
};

export default CitizenRegisterForm;