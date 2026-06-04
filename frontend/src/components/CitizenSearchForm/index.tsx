import { useState } from 'react';

interface CitizenSearchFormProps {
    onSearchCitizen: (searchTerm: string) => void;
}

const CitizenSearchForm = ({ onSearchCitizen }: CitizenSearchFormProps) => {
    const [searchTerm, setSearchTerm] = useState('');

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
                    onChange={(event) => setSearchTerm(event.target.value)}
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