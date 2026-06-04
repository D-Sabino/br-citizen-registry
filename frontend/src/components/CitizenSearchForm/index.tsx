const CitizenSearchForm = () => {
    return (
        <div className="form-container sign-in">
            <form>
                <h1>Pesquisar Cidadão</h1>

                <span>Informe o nome ou CPF para consultar o cadastro</span>

                <input
                    type="text"
                    placeholder="Nome ou CPF"
                />

                <button type="button">
                    Pesquisar
                </button>

                <div className="result-card">
                    <strong>Resultado</strong>
                    <p>Cidadão não encontrado.</p>
                </div>
            </form>
        </div>
    );
};

export default CitizenSearchForm;