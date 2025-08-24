# Cadastro de Pessoas - API REST

**Desenvolvido por:** Euclides Marques

## Descrição do Projeto

Esta é uma API REST desenvolvida em .NET para gerenciamento de cadastro de pessoas. A aplicação permite realizar operações CRUD (Criar, Ler, Atualizar e Deletar) de registros de pessoas, fornecendo endpoints para manipulação dos dados.

## Pré-requisitos

- [.NET 6.0 SDK](https://dotnet.microsoft.com/download/dotnet/6.0) ou superior
- [SQL Server](https://www.microsoft.com/pt-br/sql-server/sql-server-downloads)
- [Git](https://git-scm.com/)

## Como Executar o Projeto no Visual Studio

1. **Clone o repositório**
   - Abra o Visual Studio
   - Selecione "Clonar um repositório"
   - Cole a URL: `https://github.com/Euclides-Marques/CadastroPessoas-Web-API-CSharp.git`
   - Escolha uma pasta local para salvar o projeto
   - Clique em "Clonar"

2. **Configure a conexão com o banco de dados**
   - No Gerenciador de Soluções, navegue até o projeto `CadastroPessoas`
   - Localize e abra o arquivo `appsettings.json`
   - Atualize a string de conexão `DefaultConnection` com as credenciais do seu SQL Server
   - Salve o arquivo

3. **Aplique as migrações do banco de dados**
   - No menu superior, vá em "Ferramentas" > "Gerenciador de Pacotes do NuGet" > "Console do Gerenciador de Pacotes"
   - Execute o comando: `Update-Database`

4. **Execute a aplicação**
   - Certifique-se de que o projeto `CadastroPessoas` está definido como projeto de inicialização
   - Clique no botão de executar (seta verde) para iniciar a depuração

5. **Acesse a API**
   - A API será aberta automaticamente no navegador padrão

## Endpoints Disponíveis

- `GET /Pessoas` - Lista todas as pessoas cadastradas
- `GET /PessoasByNome` - Obtém uma pessoa específica pelo nome
- `GET /PessoasBy/{codigo}` - Obtém uma pessoa específica pelo código
- `POST /CreatePessoas` - Cadastra uma nova pessoa
- `PUT /UpdatePessoa/{codigo}` - Atualiza os dados de uma pessoa existente pelo código
- `DELETE /DeletePessoa/{codigo}` - Remove uma pessoa do cadastro pelo código

## Tecnologias Utilizadas

- .NET 6.0
- Entity Framework Core
- SQL Server
- Swagger/OpenAPI
- AutoMapper
- FluentValidation