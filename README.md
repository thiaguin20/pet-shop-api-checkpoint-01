# Pet Shop API

## Documentação do Checkpoint 1

## Sobre o projeto

A Pet Shop API é uma aplicação criada para organizar clientes, pets e agendamentos de serviços de um pet shop. Neste Checkpoint 1, a API possui a estrutura inicial do projeto, as entidades principais e endpoints de consulta para clientes.

## Problema

Um pet shop precisa manter organizadas as informações dos clientes, de seus pets e dos agendamentos realizados. Sem uma estrutura centralizada, consultar esses dados pode se tornar confuso.

## Objetivo

Disponibilizar uma API para apoiar o cadastro e a consulta de clientes, pets e agendamentos de um pet shop.

## Funcionalidades previstas

- Consultar clientes cadastrados.
- Consultar um cliente pelo identificador.
- Manter os dados dos pets vinculados aos clientes.
- Manter os agendamentos vinculados aos pets.

## Regras de negócio iniciais

- Cada pet pertence a um cliente.
- Cada agendamento pertence a um pet.
- Um cliente pode possuir mais de um pet.
- Um pet pode possuir mais de um agendamento.

## Entidades

### Cliente

- Id
- Nome
- Telefone
- Email

### Pet

- Id
- Nome
- Espécie
- Raça
- ClienteId

### Agendamento

- Id
- PetId
- DataHora
- Serviço
- Status

## Relacionamentos

```text
Cliente 1:N Pet
Pet 1:N Agendamento
```

Um cliente pode possuir vários pets, mas cada pet pertence a apenas um cliente. Um pet pode possuir vários agendamentos, mas cada agendamento pertence a apenas um pet.

## Recursos planejados

```text
/api/clientes
/api/pets
/api/agendamentos
```

## Operações planejadas para clientes

```text
GET    /api/clientes
GET    /api/clientes/{id}
POST   /api/clientes
PUT    /api/clientes/{id}
DELETE /api/clientes/{id}
```

## Implementação realizada no Checkpoint 1

- Projeto ASP.NET Core Web API criado.
- Pastas Controllers e Models organizadas.
- Classes Cliente, Pet e Agendamento criadas.
- ClientesController criado.
- Dados de clientes armazenados temporariamente em memória.
- `GET /api/clientes` retorna `200 OK`.
- `GET /api/clientes/{id}` retorna `200 OK` quando o cliente é encontrado.
- `GET /api/clientes/{id}` retorna `404 Not Found` quando o cliente não é encontrado.

## Como executar e testar

1. Abra a pasta do projeto no Visual Studio Code.
2. Clique em Executar ou Iniciar Depuração para iniciar a API.
3. Copie a URL exibida pelo projeto em execução.
4. No Insomnia, faça as requisições abaixo:

```text
GET [URL DA API]/api/clientes
GET [URL DA API]/api/clientes/1
GET [URL DA API]/api/clientes/999
```

A primeira requisição lista os clientes, a segunda consulta o cliente de ID 1 e a terceira deve retornar `404 Not Found`.

---

## Documentação do Checkpoint 2

No Checkpoint 2, a estrutura criada anteriormente foi ampliada com persistência em SQL Server, Entity Framework Core, relacionamentos, operações CRUD completas, filtros e validações.

### Tecnologias

- ASP.NET Core Web API com .NET 8.
- Entity Framework Core 8.0.3.
- SQL Server.
- Swagger para visualizar e testar os endpoints.
- Insomnia ou arquivo `Trabalho_API.http` para testes.

### Evolução em relação ao Checkpoint 1

- Os dados deixaram de ser armazenados em uma lista estática.
- Foi criado o `PetShopContext` para acessar o banco.
- Foram criadas migrations para gerar as tabelas.
- Clientes, pets e agendamentos receberam CRUD completo.
- Foram adicionados filtros, validações e regras de negócio.

### Organização do projeto

```text
Controllers/ClientesController.cs
Controllers/PetsController.cs
Controllers/AgendamentosController.cs
Data/PetShopContext.cs
Models/Cliente.cs
Models/Pet.cs
Models/Agendamento.cs
Migrations/
Program.cs
appsettings.json
```

### Banco de dados e relacionamentos

O projeto utiliza o banco `PetShopDb` e mantém os relacionamentos definidos no Checkpoint 1:

```text
Cliente 1:N Pet
Pet 1:N Agendamento
```

- `Pet.ClienteId` é chave estrangeira para `Cliente`.
- `Agendamento.PetId` é chave estrangeira para `Pet`.
- O e-mail do cliente possui índice único.
- A exclusão em cascata foi desativada para proteger registros relacionados.

### Operações CRUD

```text
GET    /api/clientes
GET    /api/clientes/{id}
POST   /api/clientes
PUT    /api/clientes/{id}
DELETE /api/clientes/{id}

GET    /api/pets
GET    /api/pets/{id}
POST   /api/pets
PUT    /api/pets/{id}
DELETE /api/pets/{id}

GET    /api/agendamentos
GET    /api/agendamentos/{id}
POST   /api/agendamentos
PUT    /api/agendamentos/{id}
DELETE /api/agendamentos/{id}
```

### Filtros

- Clientes por nome: `GET /api/clientes?nome=Ana`.
- Clientes por e-mail: `GET /api/clientes?email=ana@email.com`.
- Pets por espécie: `GET /api/pets?especie=Cachorro`.
- Pets por cliente: `GET /api/pets?clienteId=1`.
- Agendamentos por status: `GET /api/agendamentos?status=Agendado`.
- Agendamentos por data: `GET /api/agendamentos?data=2026-12-15`.

### Validações e regras de negócio

- Nome, telefone e e-mail do cliente são obrigatórios.
- O e-mail deve ter formato válido e não pode ser repetido.
- Nome e espécie do pet são obrigatórios.
- O cliente informado no cadastro do pet precisa existir.
- O pet informado no agendamento precisa existir.
- A data e a hora do agendamento devem estar no futuro.
- O status deve ser `Agendado`, `Concluido` ou `Cancelado`.
- Um cliente com pets não pode ser excluído.
- Um pet com agendamentos não pode ser excluído.

### Status Codes

- `200 OK`: consulta realizada com sucesso.
- `201 Created`: cadastro realizado com sucesso.
- `204 No Content`: alteração ou exclusão realizada com sucesso.
- `400 Bad Request`: dados inválidos ou regra de negócio não atendida.
- `404 Not Found`: registro não encontrado.

### Como executar em outro computador

1. Instale o .NET 8 SDK e o SQL Server.
2. Abra `Trabalho_API.sln` no Visual Studio.
3. Abra `Trabalho_API/appsettings.json`.
4. Na conexão `PetShopConnection`, substitua `THIAGO-SERVER\MSSQL` pela instância SQL Server da máquina utilizada.
5. Abra o Console do Gerenciador de Pacotes do NuGet e selecione `Trabalho_API` como projeto padrão.
6. Execute `Update-Database` para criar o banco `PetShopDb` e suas tabelas.
7. Defina `Trabalho_API` como projeto de inicialização e execute a aplicação.
8. Teste pelo Swagger, Insomnia ou arquivo `Trabalho_API.http`.

Exemplo de conexão com SQL Server Express:

```text
Server=localhost\SQLEXPRESS;Database=PetShopDb;Trusted_Connection=True;TrustServerCertificate=True;
```

O arquivo `Trabalho_API.http` contém apenas exemplos de requisições para testar a API. Ele não é uma segunda aplicação e pode ser ignorado quando os testes são realizados pelo Swagger ou Insomnia.
