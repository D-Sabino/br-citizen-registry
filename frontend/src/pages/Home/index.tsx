import { useState } from 'react';
import CitizenRegisterForm from '../../components/CitizenRegisterForm';
import CitizenSearchForm from '../../components/CitizenSearchForm';
import TogglePanel from '../../components/TogglePanel';

const Home = () => {
    const [isRegisterActive, setIsRegisterActive] = useState(false);

    const showRegister = () => {
        setIsRegisterActive(true);
    };

    const showSearch = () => {
        setIsRegisterActive(false);
    };

    return (
        <main className="page-container">
            <section className={`container ${isRegisterActive ? 'active' : ''}`}>
                <CitizenSearchForm />
                <CitizenRegisterForm />

                <TogglePanel
                    onShowRegister={showRegister}
                    onShowSearch={showSearch}
                />
            </section>
        </main>
    );
};

export default Home;