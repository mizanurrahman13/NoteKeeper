
# NoteKeeper API with .NET 9, Aspire, CQRS, Repository and UnitOfWork, MSSQL and NUnit Testing

This repository showcases a sample MVC Project built with .NET 9 that demonstrates the integration of .NET Aspire orchestration. The application utilizes Repository, UnitOfWork and CQRS Design Pattern. Together, these components provide a robust foundation for structured web Project.

## Table of Contents

- [Getting Started](#getting-started)
- [Features](#features)
- [Technologies Used](#technologies-used)
- [Contributing](#contributing)
- [License](#license)

## Getting Started

To get a local copy up and running, follow these simple steps.

### Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)

### Installation

1. Clone the repo
   ```sh
   git clone https://github.com/mizanurrahman13/NoteKeeper.git
   ```
2. Navigate to the project directory
   ```sh
   cd NoteKeeper
   ```
3. Restore dependencies:
   ```sh
   dotnet restore
   ```
4. Migrations:
   ```sh
   dotnet ef migrations add Initial_Migration -c ApplicationDbContext  -p .\src\libraries\PSADMIN.Persistence  -s .\src\applications\PSADMIN.Api -o Migrations
   ```
   ```sh
   dotnet ef database update -c ApplicationDbContext  -p .\src\libraries\PSADMIN.Persistence  -s .\src\applications\PSADMIN.Api
   ```

## Architecture Overview

This template follows the 3-Tier Architecture, here Repository UnitOfWork and CQRS Design Pattern organizes code by Layered, promoting high cohesion, low coupling and Separation of concern.

## Features

- **Built with .NET 9**: Utilizes the latest features for efficient development.
- **CQRS**: Separates reads and writes for improved performance.
- **Repository and UnitOfWork** : Provides abstraction, transaction management, and simplifies data access logic.
- **MSSQL**: Powerful relational database for data storage.
- **EF Core**: Popular .NET ORM.
- **NUnit Testing**: Ensures code reliability, supports TDD, and automates unit tests..

## Technologies Used

- **.NET 9**
- **CQRS Repository and UnitOfWork**
- **MSSQL**
- **EF Core**

## Contributing

Contributions are what make the open-source community such an amazing place to learn, inspire, and create. Any contributions you make are **greatly appreciated**.

1. Fork the Project
2. Create your Feature Branch (`git checkout -b feature/AmazingFeature`)
3. Commit your Changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the Branch (`git push origin feature/AmazingFeature`)
5. Create a Pull Request

## License

Distributed under the MIT License. See `LICENSE` for more information.
