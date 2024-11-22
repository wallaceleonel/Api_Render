
# Gestão de Insumos API

**Gestão de Insumos API** é uma aplicação desenvolvida em .NET 8, projetada para gerenciar insumos de forma eficiente e escalável. Este projeto inclui suporte para deploy em ambientes Docker e Render, seguindo boas práticas de desenvolvimento e arquitetura.

---

## **Índice**
- [Recursos Principais](#recursos-principais)
- [Tecnologias Utilizadas](#tecnologias-utilizadas)
- [Pré-requisitos](#pré-requisitos)
- [Configuração do Ambiente](#configuração-do-ambiente)
- [Docker](#docker)
- [Deploy no Render](#deploy-no-render)
- [Endpoints](#endpoints)
- [Contribuição](#contribuição)
- [Licença](#licença)

---

## **Recursos Principais**

- Gerenciamento de insumos via API RESTful.
- Suporte a integração com bancos de dados PostgreSQL usando Dapper e Entity Framework Core.
- Compatível com deploy via Docker e Render.
- Configuração otimizada para ambientes de produção.
- Segue boas práticas de arquitetura: Clean Architecture e separação de camadas.

---

## **Tecnologias Utilizadas**

- **.NET 8**
- **C#**
- **PostgreSQL**
- **Dapper**
- **Entity Framework Core**
- **Docker**
- **Render (PaaS)**
- **Swagger** para documentação automática.

---

## **Pré-requisitos**

Antes de iniciar, garanta que as seguintes ferramentas estejam instaladas:

- [.NET 8 SDK](https://dotnet.microsoft.com/)
- [Docker](https://www.docker.com/)
- [Git](https://git-scm.com/)
- Uma IDE como o [Visual Studio](https://visualstudio.microsoft.com/) ou [Visual Studio Code](https://code.visualstudio.com/).

---

## **Configuração do Ambiente**

1. **Clone o repositório**:

   ```bash
   git clone https://github.com/usuario/gestao-de-insumos-api.git
   cd gestao-de-insumos-api
   ```

2. **Restaure as dependências**:

   ```bash
   dotnet restore
   ```

3. **Configure a string de conexão no arquivo `appsettings.json`**:

   ```json
   {
       "ConnectionStrings": {
           "DefaultConnection": "Host=localhost;Database=GestaoInsumos;Username=postgres;Password=senha"
       }
   }
   ```

4. **Rode as migrações para configurar o banco de dados**:

   ```bash
   dotnet ef database update
   ```

5. **Rode a aplicação localmente**:

   ```bash
   dotnet run --project src/GestaoDeInsumos/GestaoDeInsumos.csproj
   ```

6. **Acesse a API** em:  
   [http://localhost:8080](http://localhost:8080)

---

## **Docker**

### **Build e Run Localmente**

1. **Construa a imagem Docker**:

   ```bash
   docker build -t gestao-insumos-api .
   ```

2. **Rode o container**:

   ```bash
   docker run -p 8080:8080 gestao-insumos-api
   ```

3. **Acesse a API** em:  
   [http://localhost:8080](http://localhost:8080)

---

### **Usando Docker Compose**

1. **Crie o arquivo `docker-compose.yml` com o seguinte conteúdo**:

   ```yaml
   version: "3.7"
   services:
     api:
       container_name: gestao-insumos-api
       build:
         context: .
         dockerfile: Dockerfile
       ports:
         - "8080:8080"
       environment:
         - ConnectionStrings__DefaultConnection=Host=postgres;Database=GestaoInsumos;Username=postgres;Password=senha

     postgres:
       image: postgres:15
       container_name: postgres-db
       environment:
         POSTGRES_USER: postgres
         POSTGRES_PASSWORD: senha
         POSTGRES_DB: GestaoInsumos
       ports:
         - "5432:5432"
   ```

2. **Execute o Docker Compose**:

   ```bash
   docker-compose up --build
   ```

3. **Acesse a API** em:  
   [http://localhost:8080](http://localhost:8080)

---

## **Deploy no Render**

### **Configuração no Render**

1. **Faça login no Render**.

2. **Crie um novo Web Service**.

3. **Configure as seguintes opções**:

   - **Build Command**:  
     ```bash
     dotnet publish -c Release -o /app/publish
     ```

   - **Start Command**:  
     ```bash
     dotnet /app/publish/GestaoDeInsumos.dll
     ```

   - **Environment**: Docker.

4. **Crie um banco de dados PostgreSQL no Render**.

5. **Adicione a URL de conexão como uma variável de ambiente** chamada `ConnectionStrings__DefaultConnection`.

---

## **Endpoints**

Os endpoints disponíveis são descritos abaixo. Para mais detalhes, acesse o Swagger em `/swagger` após rodar a aplicação.

### **Exemplos de Endpoints**

- **GET /api/insumos**  
  Lista todos os insumos cadastrados.

- **POST /api/insumos**  
  Cadastra um novo insumo.  
  **Body**:

  ```json
  {
      "nome": "Insumo A",
      "quantidade": 100,
      "unidade": "kg"
  }
  ```

- **PUT /api/insumos/{id}**  
  Atualiza as informações de um insumo.  
  **Body**:

  ```json
  {
      "nome": "Insumo B",
      "quantidade": 50,
      "unidade": "kg"
  }
  ```

- **DELETE /api/insumos/{id}**  
  Remove um insumo pelo ID.

---

## **Contribuição**

Contribuições são bem-vindas! Sinta-se à vontade para abrir issues e pull requests.

---

## **Licença**

Este projeto está licenciado sob a Licença MIT. Consulte o arquivo `LICENSE` para mais informações.
