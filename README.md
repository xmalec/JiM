# Jim Starter project template

## WebApi .NET Core Supports

- Identity management
- Azure Cognitive search integration
- Image processor
- Memory cache
- Authentication & Authorization
- Automapper configuration
- Entity Framework ORM
- DI configuration
- Schedule tasks using Quartz.NET with db loging and run control
- Service Factory
- Logging setup
- NUnit test project
- Github Action to build project and run the tests

## React Frontend Supports

- Blank react project
- Basic ESLint && Prettier configuration setup
- Tailwind setup

## TODO

- Build frontend GitHub Actions
- Advanced frontend template
- Create Pipeline to generate idempotent sql script
  - dotnet ef migrations script --idempotent | Set-Content -Path ./migration-idempotent.sql
