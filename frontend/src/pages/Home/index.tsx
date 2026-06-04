import { useEffect, useState } from 'react';
import CitizenRegisterForm from '../../components/CitizenRegisterForm';
import CitizenSearchForm from '../../components/CitizenSearchForm';
import FeedbackDialog from '../../components/FeedbackDialog';
import TogglePanel from '../../components/TogglePanel';
import type { Citizen } from '../../types/Citizen';
import type { FeedbackMessage } from '../../types/FeedbackMessage';
import { isValidCpf, removeCpfMask } from '../../utils/cpfUtils';

const Home = () => {
    const [isRegisterActive, setIsRegisterActive] = useState(false);
    const [citizens, setCitizens] = useState<Citizen[]>([]);
    const [feedbackMessage, setFeedbackMessage] = useState<FeedbackMessage | null>(null);

    useEffect(() => {
        if (!feedbackMessage) {
            return;
        }

        const timer = setTimeout(() => {
            setFeedbackMessage(null);
        }, 5000);

        return () => clearTimeout(timer);
    }, [feedbackMessage]);

    const showRegister = () => {
        setIsRegisterActive(true);
    };

    const showSearch = () => {
        setIsRegisterActive(false);
    };

    const handleRegisterCitizen = (citizenData: Omit<Citizen, 'id'>) => {
        if (!isValidCpf(citizenData.cpf)) {
            setFeedbackMessage({
                type: 'error',
                title: 'CPF inválido',
                description: 'Informe um CPF válido para realizar o cadastro.'
            });

            return;
        }

        const alreadyRegistered = citizens.some((citizen) => {
            return removeCpfMask(citizen.cpf) === removeCpfMask(citizenData.cpf);
        });

        if (alreadyRegistered) {
            setFeedbackMessage({
                type: 'warning',
                title: 'CPF já cadastrado',
                description: 'Já existe um cidadão cadastrado com este CPF.'
            });

            return;
        }

        const newCitizen: Citizen = {
            id: crypto.randomUUID(),
            fullName: citizenData.fullName,
            cpf: citizenData.cpf
        };

        setCitizens((currentCitizens) => [...currentCitizens, newCitizen]);

        setFeedbackMessage({
            type: 'success',
            title: 'Cadastro realizado com sucesso!',
            description: `Nome: ${newCitizen.fullName}`,
            details: `CPF: ${newCitizen.cpf}`
        });
    };

    const handleSearchCitizen = (searchTerm: string) => {
        const normalizedSearchTerm = searchTerm.trim().toLowerCase();
        const normalizedSearchCpf = removeCpfMask(searchTerm);

        const foundCitizen = citizens.find((citizen) => {
            const normalizedName = citizen.fullName.toLowerCase();
            const normalizedCpf = removeCpfMask(citizen.cpf);

            return (
                normalizedName.includes(normalizedSearchTerm) ||
                normalizedCpf === normalizedSearchCpf
            );
        });

        if (!foundCitizen) {
            setFeedbackMessage({
                type: 'warning',
                title: 'Cidadão não encontrado',
                description: 'Nenhum cadastro foi localizado com os dados informados.'
            });

            return;
        }

        setFeedbackMessage({
            type: 'success',
            title: 'Cidadão encontrado',
            description: `Nome: ${foundCitizen.fullName}`,
            details: `CPF: ${foundCitizen.cpf}`
        });
    };

    return (
        <main className="page-container">
            <section className={`container ${isRegisterActive ? 'active' : ''}`}>
                <CitizenSearchForm onSearchCitizen={handleSearchCitizen} />

                <CitizenRegisterForm onRegisterCitizen={handleRegisterCitizen} />

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