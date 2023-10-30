# Stox Todos Application

## Description

Project backend du test technique. L'architecture tente de suivre un certain standart de Clean Code,
largement inspiré de cette [playlist](https://www.youtube.com/watch?v=fhM0V2N1GpY&list=PLzYkqgWkHPKBcDIP5gzLfASkQyTdy0t4k)
(sans toutefois avoir tout suivi à la lettre).

- Roule sous C# / Asp.Net
- Utilise Yarn, seulement pour créer des alias de commandes longues et difficiles à retenir. Aucun package npm n'est installé.
- La base de donnée est une instance Postgres dans un container docker

## Requis
 - Yarn doit être installé globalement
 - Docker et Docker Compose doivent être installés pour rouler le project en développement

## Instructions
 - Lancer le container docker avec `docker-compose up -d` (ou `yarn pg:up`)
 - La base de données postgres sera accessible sous `host=localhost`, `port=54321`, `database=stox`, `user=stox`, `password=stox`
 - Rouler les migrations avec `yarn migrate:update`
 - Lancer le projet avec `yarn dev`. Celui-ci devrait être accessible sur https://localhost:7296

## Routes
 - `GET /todos`
 - `GET /todos/{id}`
 - `POST /todos`
 - `PUT /todos/{id}`
 - `DELETE /todos/{id}`

