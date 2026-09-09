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
