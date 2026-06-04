import { useState } from 'react';
import { formatCpf } from '../../utils/cpfUtils';

interface CitizenSearchFormProps {
    onSearchCitizen: (searchTerm: string) => void;
}

const CitizenSearchForm = ({ onSearchCitizen }: CitizenSearchFormProps) => {
    const [searchTerm, setSearchTerm] = useState('');

    const handleSearchTermChange = (value: string) => {
        const hasLetters = /[a-zA-ZÀ-ÿ]/.test(value);

        if (hasLetters) {
            setSearchTerm(value);
            return;
        }

        setSearchTerm(formatCpf(value));
    };

    const handleSearch = (event: React.FormEvent<HTMLFormElement>) => {
        event.preventDefault();

        if (!searchTerm.trim()) {
            return;
        }

        onSearchCitizen(searchTerm);
    };

    return (
        <div className="form-container sign-in">
            <form onSubmit={handleSearch}>
                <h1>Pesquisar Cidadão</h1>

                <span>Informe o nome ou CPF para consultar o cadastro</span>

                <input
                    type="text"
                    placeholder="Nome ou CPF"
                    value={searchTerm}
                    onChange={(event) => handleSearchTermChange(event.target.value)}
                    required
                />

                <button type="submit">
                    Pesquisar
                </button>
            </form>
        </div>
    );
};

export default CitizenSearchForm;