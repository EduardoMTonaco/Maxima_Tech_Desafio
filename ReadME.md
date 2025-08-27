# 🧪 Maxima Tech - Desafio Técnico (.NET 8 + Blazor + RabbitMQ + MySQL + Docker)

Este projeto é uma solução para um desafio técnico, desenvolvida em .NET 8, utilizando conceitos modernos como autenticação via JWT, mensageria com RabbitMQ, interface em Blazor Server, persistência com MySQL via ADO.NET e Docker para orquestração dos serviços.

---

## 🧩 Estrutura da Solução

A solução é composta por **quatro projetos** principais:

### 1. **Maxima_Tech_API** - `API REST em .NET 8`

- Gerencia **departamentos**, **produtos** e **usuários**.
- Utiliza **JWT** para autenticação e autorização.
- Conecta ao **MySQL** usando **ADO.NET**.
- Operações CRUD (com Soft Delete para produtos).
- Dispara um **evento via RabbitMQ** ao cadastrar produto.

**Endpoints:**

- `GET /DepartamentosSelect`
- `GET /ProdutosSelect`
- `POST /ProdutosInsert`
- `PUT /produtos/{id}`
- `DELETE /ProdutosDelete`
- `POST /usuarios/cadastrar`
- `POST /login`
- `GET /login/authorized` (validação de token)

---

### 2. **Maxima_Tech_Web** - `Frontend em Blazor Server`

- Tela inicial de **login** integrada à API.
- Salva e reutiliza o token **JWT** para navegação segura.
- Página de **cadastro e edição de produtos**.
- Página de **listagem de produtos**.
- Tela **Home** apenas para apresentação.

---

### 3. **Maxima_Tech_RabbitMQ** - `Mensageria`

- Serviço console com **MassTransit + RabbitMQ**.
- Consome o evento `ProdutoCadastradoEvent`, disparado pela API.

---

### 4. **Utility** - `Biblioteca Compartilhada`

- Contém:
  - `ProdutoCadastradoEvent` (compartilhado entre API e consumer)
  - `PasswordHelper` para **criptografia de senha** com comparação segura
- A API salva senhas **criptografadas** no banco.

---

## 🗃️ Banco de Dados

- Banco: **MySQL 8**
- Scripts de criação de tabelas e inserts iniciais automatizados via Docker Compose.
- ADO.NET com comandos SQL gerados dinamicamente por atributos.

---

## 🐳 Docker & Docker Compose

Todos os serviços rodam com Docker, usando **Docker Compose**:

- `MySQL`
- `RabbitMQ` (com interface de gerenciamento)
- `API`
- `Blazor`

---

### ▶️ Como rodar o projeto

1. Clone o repositório:
   ```bash
   git clone https://github.com/seu-usuario/seu-repo.git
   cd seu-repo


2. Suba os containers:

docker-compose up --build


3. Acesse os serviços:

Serviço	URL
API	http://localhost:7020

Blazor	http://localhost:5247

RabbitMQ	http://localhost:15672

Login RabbitMQ:

- Usuário: guest

- Senha: guest

---

### 🔐 Senhas criptografadas

As senhas dos usuários são criptografadas usando a classe PasswordHelper, que gera hashes seguros e compara as senhas sem necessidade de desencriptação.

---

### 📦 Tecnologias Utilizadas

- .NET 8.0

- Blazor Server

- MySQL 8

- MassTransit

- RabbitMQ

- JWT Authentication

- Docker / Docker Compose

---

- #### ✅ Funcionalidades Concluídas

- #### ✅ Login com JWT

- #### ✅ Cadastro, edição e soft delete de produtos

- #### ✅ Cadastro e listagem de departamentos

- #### ✅ Cadastro de usuários com senha criptografada

- #### ✅ Evento de produto cadastrado via RabbitMQ

- #### ✅ Frontend com Blazor

- #### ✅ Orquestração com Docker Compose