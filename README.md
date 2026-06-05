# BR Citizen Registry

Aplicação full stack para cadastro e consulta de cidadãos brasileiros, utilizando nome completo e CPF como dados principais.

O projeto tem como objetivo demonstrar uma implementação organizada de cadastro, validação e consulta de dados, utilizando backend em C#, frontend em React com TypeScript, banco de dados MySQL e uma arquitetura baseada em Ports and Adapters.

---

## Sobre o projeto

O sistema permite cadastrar cidadãos brasileiros informando nome completo e CPF.

Antes de salvar um cadastro, o CPF é validado automaticamente. Também é feita uma verificação para impedir o cadastro duplicado de um mesmo CPF.

Além do cadastro, a aplicação permite consultar cidadãos já registrados utilizando nome ou CPF.

Quando a busca encontra registros, o sistema exibe o nome e o CPF dos cidadãos encontrados. Caso nenhum registro seja localizado, a aplicação informa que o cidadão não foi encontrado.

---

## Funcionalidades

* Cadastro de cidadãos brasileiros;
* Campos obrigatórios para nome completo e CPF;
* Máscara de CPF no frontend;
* Validação de CPF no frontend;
* Validação de CPF no backend;
* Bloqueio de CPF inválido;
* Bloqueio de CPF duplicado;
* Persistência dos dados em banco MySQL;
* Pesquisa de cidadãos por CPF;
* Pesquisa de cidadãos por nome;
* Retorno de múltiplos cidadãos quando há nomes iguais;
* Exibição de mensagens de sucesso, erro e alerta;
* Interface web amigável;
* API documentada com Swagger;
* Testes automatizados para regras de CPF;
* Testes automatizados para casos de uso da aplicação.

---

## Tecnologias utilizadas

### Backend

* C#
* .NET 9
* ASP.NET Core Web API
* Entity Framework Core
* MySQL
* Pomelo Entity Framework Core MySQL Provider
* Swagger / Swashbuckle
* xUnit
* NSubstitute
* FluentAssertions

### Frontend

* React
* TypeScript
* Vite
* CSS

### Banco de dados

* MySQL 8

### Gerenciadores de pacotes

* NuGet
* npm

---

## Arquitetura

O backend foi organizado seguindo uma abordagem baseada em Arquitetura Hexagonal, também conhecida como Ports and Adapters.

Essa abordagem ajuda a separar a regra de negócio das tecnologias externas, como banco de dados, HTTP, controllers, frameworks e ferramentas de persistência.

A estrutura principal do backend está organizada em quatro partes:

```text
backend/
├── BrCitizenRegistry.Api/
├── src/
│   ├── BrCitizenRegistry.Domain/
│   ├── BrCitizenRegistry.Application/
│   └── BrCitizenRegistry.Infrastructure/
└── tests/
    └── BrCitizenRegistry.Tests/
```

### BrCitizenRegistry.Api

Projeto responsável pela entrada HTTP da aplicação.

Contém:

* Controllers;
* Configuração da API;
* Swagger;
* CORS;
* Injeção de dependência;
* Arquivos de configuração.

Essa camada recebe as requisições HTTP e encaminha as operações para os casos de uso da aplicação.

---

### BrCitizenRegistry.Domain

Projeto responsável pelas regras centrais do domínio.

Contém:

* Entidade `Citizen`;
* Validador de CPF.

Essa camada não depende de banco de dados, API, Entity Framework, Swagger ou qualquer detalhe externo.

---

### BrCitizenRegistry.Application

Projeto responsável pelos casos de uso da aplicação.

Contém:

* DTOs;
* Portas de entrada;
* Portas de saída;
* Caso de uso de cidadãos.

Essa camada define as operações principais da aplicação, como cadastrar e pesquisar cidadãos.

---

### BrCitizenRegistry.Infrastructure

Projeto responsável pelos detalhes técnicos externos.

Contém:

* `AppDbContext`;
* Configuração do Entity Framework Core;
* Migrations;
* Repositório concreto usando MySQL.

Essa camada implementa a persistência dos dados e adapta o banco MySQL para as portas definidas na camada Application.

---

### BrCitizenRegistry.Tests

Projeto responsável pelos testes automatizados.

Contém testes para:

* Validação de CPF;
* Formatação de CPF;
* Cadastro de cidadão;
* Bloqueio de CPF inválido;
* Bloqueio de CPF duplicado;
* Pesquisa por nome;
* Pesquisa por CPF;
* Pesquisa sem resultados.

---

## Estrutura do projeto

```text
br-citizen-registry/
├── README.md
├── .gitignore
├── .gitattributes
│
├── backend/
│   ├── BrCitizenRegistry.sln
│   │
│   ├── BrCitizenRegistry.Api/
│   │   ├── Controllers/
│   │   │   └── CitizensController.cs
│   │   ├── Program.cs
│   │   ├── appsettings.json
│   │   ├── appsettings.Development.json
│   │   └── Properties/
│   │       └── launchSettings.json
│   │
│   ├── src/
│   │   ├── BrCitizenRegistry.Domain/
│   │   │   ├── Entities/
│   │   │   │   └── Citizen.cs
│   │   │   └── ValueObjects/
│   │   │       └── CpfValidator.cs
│   │   │
│   │   ├── BrCitizenRegistry.Application/
│   │   │   ├── DTOs/
│   │   │   │   ├── CitizenResponse.cs
│   │   │   │   └── CreateCitizenRequest.cs
│   │   │   ├── Ports/
│   │   │   │   ├── In/
│   │   │   │   │   └── ICitizenUseCase.cs
│   │   │   │   └── Out/
│   │   │   │       └── ICitizenRepository.cs
│   │   │   └── UseCases/
│   │   │       └── CitizenUseCase.cs
│   │   │
│   │   └── BrCitizenRegistry.Infrastructure/
│   │       └── Persistence/
│   │           ├── AppDbContext.cs
│   │           ├── Migrations/
│   │           └── Repositories/
│   │               └── MySqlCitizenRepository.cs
│   │
│   └── tests/
│       └── BrCitizenRegistry.Tests/
│           ├── Application/
│           │   └── CitizenUseCaseTests.cs
│           └── Domain/
│               └── CpfValidatorTests.cs
│
└── frontend/
    ├── .env.example
    ├── index.html
    ├── package.json
    ├── package-lock.json
    ├── vite.config.ts
    ├── tsconfig.json
    ├── tsconfig.app.json
    ├── tsconfig.node.json
    ├── eslint.config.js
    ├── public/
    │   └── favicon.svg
    └── src/
        ├── components/
        │   ├── CitizenRegisterForm.tsx
        │   ├── CitizenSearchForm.tsx
        │   ├── FeedbackDialog.tsx
        │   └── TogglePanel.tsx
        ├── pages/
        │   └── Home.tsx
        ├── services/
        │   └── citizenService.ts
        ├── types/
        │   ├── Citizen.ts
        │   └── FeedbackMessage.ts
        ├── utils/
        │   └── cpfUtils.ts
        ├── App.tsx
        ├── index.css
        └── main.tsx
```

---

## Pré-requisitos

Antes de executar o projeto, é necessário ter instalado:

* Git;
* .NET SDK 9;
* Node.js;
* npm;
* MySQL Server 8;
* MySQL Workbench, DBeaver ou outro cliente MySQL opcional.

Para conferir as versões instaladas:

```bash
dotnet --version
```

```bash
node -v
```

```bash
npm -v
```

---

## Como executar o projeto

### 1. Clonar o repositório

```bash
git clone https://github.com/SEU-USUARIO/br-citizen-registry.git
```

Entre na pasta do projeto:

```bash
cd br-citizen-registry
```

---

## Configuração do banco de dados MySQL

### 1. Verificar se o MySQL está rodando

No Windows, é possível verificar o serviço do MySQL pelo PowerShell:

```powershell
Get-Service | Where-Object { $_.Name -like "*mysql*" -or $_.DisplayName -like "*mysql*" }
```

O serviço pode aparecer com nomes como:

```text
MySQL80
MySQL84
```

Se o serviço estiver parado, inicie usando o nome correspondente.

Exemplo:

```powershell
Start-Service MySQL80
```

ou:

```powershell
net start MySQL80
```

---

### 2. Acessar o MySQL

Caso o comando `mysql` esteja configurado no PATH:

```bash
mysql -u root -p
```

Caso não esteja configurado, no Windows é possível acessar pelo caminho da instalação:

```powershell
cd "C:\Program Files\MySQL\MySQL Server 8.0\bin"
.\mysql.exe -u root -p
```

Digite a senha do usuário `root`.

---

### 3. Criar o banco de dados

Dentro do terminal do MySQL, execute:

```sql
CREATE DATABASE br_citizen_registry
CHARACTER SET utf8mb4
COLLATE utf8mb4_unicode_ci;
```

Confira se o banco foi criado:

```sql
SHOW DATABASES;
```

Saia do MySQL:

```sql
exit;
```

---

## Configuração do backend

### 1. Ajustar a connection string

Abra o arquivo:

```text
backend/BrCitizenRegistry.Api/appsettings.json
```

Localize a configuração:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=127.0.0.1;Port=3306;Database=br_citizen_registry;User=root;Password=SUA_SENHA;"
}
```

Substitua `SUA_SENHA` pela senha do seu MySQL.

Exemplo:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=127.0.0.1;Port=3306;Database=br_citizen_registry;User=root;Password=123456;"
}
```

Verifique também o arquivo:

```text
backend/BrCitizenRegistry.Api/appsettings.Development.json
```

Caso ele possua `ConnectionStrings`, ajuste a senha nele também.

---

### 2. Restaurar pacotes do backend

Na pasta do backend:

```bash
cd backend
```

Execute:

```bash
dotnet restore BrCitizenRegistry.sln
```

---

### 3. Instalar ou atualizar a ferramenta do Entity Framework

Caso ainda não tenha o `dotnet-ef` instalado:

```bash
dotnet tool install --global dotnet-ef --version 9.0.16
```

Caso já tenha instalado e queira garantir a versão compatível:

```bash
dotnet tool update --global dotnet-ef --version 9.0.16
```

Confira a versão:

```bash
dotnet ef --version
```

---

### 4. Aplicar as migrations no banco

Ainda na pasta `backend`, execute:

```bash
dotnet ef database update --project src/BrCitizenRegistry.Infrastructure --startup-project BrCitizenRegistry.Api
```

Esse comando cria as tabelas necessárias no banco `br_citizen_registry`.

---

### 5. Executar o backend

Entre na pasta da API:

```bash
cd BrCitizenRegistry.Api
```

Execute:

```bash
dotnet run
```

A API será iniciada e o terminal exibirá os endereços disponíveis.

Exemplo:

```text
Now listening on: http://localhost:5125
```

Abra o Swagger no navegador:

```text
http://localhost:5125/swagger
```

A porta pode variar conforme o ambiente.

---

## Testando a API pelo Swagger

Com o backend rodando, acesse:

```text
http://localhost:PORTA/swagger
```

Substitua `PORTA` pela porta exibida no terminal.

---

### Cadastro de cidadão

Endpoint:

```http
POST /api/Citizens
```

Exemplo de corpo da requisição:

```json
{
  "fullName": "João da Silva",
  "cpf": "529.982.247-25"
}
```

Resposta esperada:

```json
{
  "id": "guid-gerado",
  "fullName": "João da Silva",
  "cpf": "529.982.247-25"
}
```

---

### Pesquisa por nome

Endpoint:

```http
GET /api/Citizens/search?term=João
```

Resposta esperada:

```json
[
  {
    "id": "guid-gerado",
    "fullName": "João da Silva",
    "cpf": "529.982.247-25"
  }
]
```

Caso existam múltiplos cidadãos com o mesmo nome, todos serão retornados.

---

### Pesquisa por CPF

Endpoint:

```http
GET /api/Citizens/search?term=52998224725
```

Também é possível pesquisar com máscara:

```http
GET /api/Citizens/search?term=529.982.247-25
```

---

### Cidadão não encontrado

Ao pesquisar por um nome ou CPF inexistente, a API retorna:

```json
{
  "message": "Cidadão não encontrado"
}
```

---

## Configuração do frontend

### 1. Instalar dependências

Abra outro terminal e entre na pasta do frontend:

```bash
cd frontend
```

Instale os pacotes:

```bash
npm install
```

---

### 2. Configurar a URL da API

Na pasta `frontend`, crie um arquivo chamado:

```text
.env
```

Use como base o arquivo:

```text
.env.example
```

Exemplo de conteúdo:

```env
VITE_API_BASE_URL=http://localhost:5125/api
```

A porta deve ser a mesma exibida ao executar o backend.

Exemplo:

```text
Now listening on: http://localhost:5125
```

Então o `.env` deve ficar:

```env
VITE_API_BASE_URL=http://localhost:5125/api
```

---

### 3. Executar o frontend

Na pasta `frontend`, execute:

```bash
npm run dev
```

O Vite exibirá o endereço local da aplicação.

Exemplo:

```text
http://localhost:5173
```

Abra esse endereço no navegador.

---

## Fluxo completo para testar a aplicação

Com backend e frontend rodando:

1. Acesse o frontend no navegador;
2. Clique em `Cadastrar`;
3. Informe um nome completo;
4. Informe um CPF válido;
5. Clique em `Cadastrar`;
6. A aplicação deve exibir uma mensagem de sucesso com nome e CPF;
7. Clique em `Pesquisar`;
8. Pesquise pelo nome cadastrado;
9. A aplicação deve exibir os dados do cidadão;
10. Pesquise pelo CPF cadastrado;
11. A aplicação deve exibir os dados do cidadão;
12. Pesquise por um nome inexistente;
13. A aplicação deve exibir `Cidadão não encontrado`.

---

## CPFs para teste

CPF válido:

```text
529.982.247-25
```

CPF inválido:

```text
111.111.111-11
```

Outro CPF inválido:

```text
123.456.789-00
```

---

## Rodando os testes automatizados

Na pasta `backend`, execute:

```bash
dotnet test BrCitizenRegistry.sln
```

Os testes cobrem:

* CPF válido;
* CPF inválido;
* Remoção de máscara do CPF;
* Formatação de CPF;
* Cadastro de cidadão com dados válidos;
* Bloqueio de CPF inválido;
* Bloqueio de CPF duplicado;
* Pesquisa por nome;
* Pesquisa por CPF;
* Pesquisa sem resultado.

---

## Build do frontend

Para verificar se o frontend compila corretamente:

```bash
cd frontend
npm run build
```

---

## Build do backend

Para verificar se o backend compila corretamente:

```bash
cd backend
dotnet build BrCitizenRegistry.sln
```

---

## Observações sobre CPF

O CPF é armazenado no banco de dados sem máscara.

Exemplo:

```text
52998224725
```

Porém, é exibido na API e no frontend com máscara:

```text
529.982.247-25
```

Essa decisão evita duplicidades causadas por formatos diferentes de entrada.

---

## Observações sobre nomes duplicados

O sistema permite cadastrar cidadãos com o mesmo nome, desde que o CPF seja diferente.

Isso é necessário porque nomes e sobrenomes podem se repetir entre pessoas diferentes.

Ao pesquisar por nome, a aplicação retorna todos os cidadãos encontrados.

Ao pesquisar por CPF, o retorno esperado é um único cidadão, pois o CPF é único.

---

## Principais decisões técnicas

### C# e ASP.NET Core

O backend foi desenvolvido em C# com ASP.NET Core Web API.

### MySQL

O MySQL foi utilizado como banco de dados relacional da aplicação.

### Entity Framework Core

O Entity Framework Core foi utilizado para mapear as entidades da aplicação e gerenciar a persistência dos dados.

### Arquitetura Hexagonal

A aplicação foi organizada separando:

* Domínio;
* Casos de uso;
* Infraestrutura;
* Entrada HTTP.

Essa organização facilita testes, manutenção e substituição de tecnologias externas.

### Testes automatizados

Foram adicionados testes automatizados para validar as principais regras de negócio sem depender diretamente do banco de dados.

### React com TypeScript

O frontend foi desenvolvido com React, TypeScript e Vite, consumindo a API criada no backend.

### Identidade visual

A interface foi construída com uma paleta de cores inspirada na bandeira do Brasil, utilizando tons mais suaves de azul, verde e amarelo.

A escolha das cores teve como objetivo manter uma relação visual com o tema da aplicação, que trata do cadastro de cidadãos brasileiros, sem utilizar tons excessivamente fortes ou com aparência institucional demais.

A paleta busca transmitir:

* Azul: confiança, estabilidade e tecnologia;
* Verde: equilíbrio, organização e referência ao Brasil;
* Amarelo suave: destaque visual e acolhimento.

Além disso, o favicon da aplicação foi personalizado com um ícone inspirado na bandeira do Brasil, reforçando a identidade visual do sistema de forma simples e discreta.

---

## Possíveis problemas e soluções

### Erro: não foi possível conectar ao MySQL

Verifique se o serviço do MySQL está rodando:

```powershell
Get-Service | Where-Object { $_.Name -like "*mysql*" -or $_.DisplayName -like "*mysql*" }
```

Se estiver parado:

```powershell
Start-Service MySQL80
```

ou use o nome correspondente ao serviço instalado.

---

### Erro: porta do backend diferente da configurada no frontend

Verifique a porta exibida no terminal ao rodar:

```bash
dotnet run
```

Depois ajuste o arquivo:

```text
frontend/.env
```

Exemplo:

```env
VITE_API_BASE_URL=http://localhost:5125/api
```

---

### Erro ao aplicar migrations

Confira se:

* O MySQL está rodando;
* O banco `br_citizen_registry` foi criado;
* A senha da connection string está correta;
* O comando foi executado dentro da pasta `backend`.

Comando recomendado:

```bash
dotnet ef database update --project src/BrCitizenRegistry.Infrastructure --startup-project BrCitizenRegistry.Api
```

---

## Autor

Desenvolvido por Daniel Sabino.