interface TogglePanelProps {
    onShowRegister: () => void;
    onShowSearch: () => void;
}

const TogglePanel = ({
    onShowRegister,
    onShowSearch
}: TogglePanelProps) => {
    return (
        <div className="toggle-container">
            <div className="toggle">
                <div className="toggle-panel toggle-left">
                    <h1>Consulta rápida</h1>
                    <p>Pesquise cidadãos já cadastrados pelo nome ou CPF.</p>

                    <button
                        type="button"
                        className="hidden"
                        onClick={onShowSearch}
                    >
                        Pesquisar
                    </button>
                </div>

                <div className="toggle-panel toggle-right">
                    <h1>Novo cadastro</h1>
                    <p>Cadastre cidadãos brasileiros com validação automática de CPF.</p>

                    <button
                        type="button"
                        className="hidden"
                        onClick={onShowRegister}
                    >
                        Cadastrar
                    </button>
                </div>
            </div>
        </div>
    );
};

export default TogglePanel;