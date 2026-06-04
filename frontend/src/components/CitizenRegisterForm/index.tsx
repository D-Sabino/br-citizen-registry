const CitizenRegisterForm = () => {
    return (
        <div className="form-container sign-up">
            <form>
                <h1>Cadastrar Cidadão</h1>

                <span>Preencha os dados obrigatórios do cidadão brasileiro</span>

                <input
                    type="text"
                    placeholder="Nome completo"
                />

                <input
                    type="text"
                    placeholder="CPF"
                />

                <button type="button">
                    Cadastrar
                </button>
            </form>
        </div>
    );
};

export default CitizenRegisterForm;