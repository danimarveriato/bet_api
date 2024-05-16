# API REST .NET 8 e Swagger
O projeto Bet API é uma API REST desenvolvida para implementação na oficina prática sobre programação em .NET ministrada por mim na Tech Week 2024 do curso Superior de Tecnologia em Sistemas para Internet do Instituto Federal Farroupilha - Campus Panambi.

Nesta API, é implementada um CRUD utilizando a linguagem de programação .NET 8 e o PostgreSQL 16 como banco de dados. O projeto também inclui testes e documentação da API utilizando o Swagger.


## Funcionalidades
A API Bet permite:

- Criar (Create) uma time
- Ler (Read) informações sobre os times existentes
- Atualizar (Update) dados de um time existente
- Deletar (Delete) uma time

## Tecnologias Utilizadas
- .NET 8: Framework utilizado para o desenvolvimento da API.
- PostgreSQL 16: Banco de dados relacional utilizado para armazenar os dados das apostas.
- Swagger: Ferramenta utilizada para documentar e testar a API.
- Npgsql: Provedor de dados ADO.NET oficial para PostgreSQL.

## Pré-requisitos
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [PostgreSQL 16](https://www.postgresql.org/download/)

## Endpoints
Abaixo estão alguns dos principais endpoints da API:

- GET /api/times: Retorna todos os times
- GET /api/times/{codigo}: Retorna um time específico pelo Código
- POST /api/times: Cria um novo time
- PUT /api/times/{codigo}: Atualiza um time existente
- DELETE /api/times/{codigo}: Deleta um time pelo Código

## Instruções
- [Oficina de API com .NET e Swagger](https://danimarveriato.notion.site/Oficina-de-API-com-NET-e-Swagger-3041245a358341b0b36a08754aac6946)


  
  
