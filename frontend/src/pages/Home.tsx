import { useEffect, useState } from 'react';
import CitizenRegisterForm from '../components/CitizenRegisterForm';
import CitizenSearchForm from '../components/CitizenSearchForm';
import FeedbackDialog from '../components/FeedbackDialog';
import TogglePanel from '../components/TogglePanel';
import { citizenService } from '../services/citizenService';
import type { Citizen } from '../types/Citizen';
import type { FeedbackMessage } from '../types/FeedbackMessage';
import { isValidCpf } from '../utils/cpfUtils';

const Home = () => {
    const [isRegisterActive, setIsRegisterActive] = useState(false);
    const [feedbackMessage, setFeedbackMessage] = useState<FeedbackMessage | null>(null);
    const [isRegistering, setIsRegistering] = useState(false);
    const [isSearching, setIsSearching] = useState(false);

    useEffect(() => {
        if (!feedbackMessage) {
            return;
        }

        const timer = setTimeout(() => {
            setFeedbackMessage(null);
        }, 30000);

        return () => clearTimeout(timer);
    }, [feedbackMessage]);

    const showRegister = () => {
        setIsRegisterActive(true);
    };

    const showSearch = () => {
        setIsRegisterActive(false);
    };

    const getErrorMessage = (error: unknown): string => {
        if (error instanceof Error) {
            return error.message;
        }

        return 'Ocorreu um erro inesperado.';
    };

    const handleRegisterCitizen = async (citizenData: Omit<Citizen, 'id'>): Promise<boolean> => {
        if (!isValidCpf(citizenData.cpf)) {
            setFeedbackMessage({
                type: 'error',
                title: 'CPF inválido',
                description: 'Informe um CPF válido para realizar o cadastro.'
            });

            return false;
        }

        try {
            setIsRegistering(true);

            const newCitizen = await citizenService.create(citizenData);

            setFeedbackMessage({
                type: 'success',
                title: 'Cadastro realizado com sucesso!',
                description: `Nome: ${newCitizen.fullName}`,
                details: `CPF: ${newCitizen.cpf}`
            });

            return true;
        } catch (error) {
            setFeedbackMessage({
                type: 'error',
                title: 'Não foi possível cadastrar',
                description: getErrorMessage(error)
            });

            return false;
        } finally {
            setIsRegistering(false);
        }
    };

    const handleSearchCitizen = async (searchTerm: string): Promise<void> => {
        try {
            setIsSearching(true);

            const foundCitizens = await citizenService.search(searchTerm);

            setFeedbackMessage({
                type: 'success',
                title: foundCitizens.length === 1
                    ? 'Cidadão encontrado'
                    : `${foundCitizens.length} cidadãos encontrados`,
                description: 'Resultado da pesquisa:',
                details: foundCitizens.map((citizen) => {
                    return `Nome: ${citizen.fullName}\nCPF: ${citizen.cpf}`;
                })
            });
        } catch (error) {
            const errorMessage = getErrorMessage(error);

            setFeedbackMessage({
                type: errorMessage === 'Cidadão não encontrado' ? 'warning' : 'error',
                title: errorMessage === 'Cidadão não encontrado'
                    ? 'Cidadão não encontrado'
                    : 'Não foi possível pesquisar',
                description: errorMessage === 'Cidadão não encontrado'
                    ? 'Nenhum cadastro foi localizado com os dados informados.'
                    : errorMessage
            });
        } finally {
            setIsSearching(false);
        }
    };

    return (
        <main className="page-container">
            <section className={`container ${isRegisterActive ? 'active' : ''}`}>
                <CitizenSearchForm
                    onSearchCitizen={handleSearchCitizen}
                    isSearching={isSearching}
                />

                <CitizenRegisterForm
                    onRegisterCitizen={handleRegisterCitizen}
                    isRegistering={isRegistering}
                />

                <TogglePanel
                    onShowRegister={showRegister}
                    onShowSearch={showSearch}
                />
            </section>

            <FeedbackDialog
                message={feedbackMessage}
                onClose={() => setFeedbackMessage(null)}
            />
        </main>
    );
};

export default Home;