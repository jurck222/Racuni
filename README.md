# Invoice Management System

A simple .NET application for managing invoices and invoice items. This application uses Entity Framework Core with PostgreSql database and an in-memory database for testing purposes.

## Features

- **Manage Invoices**: Read, and delete invoices.
- **Manage Invoice Items**: Add, retrieve, and delete items within invoices.
- **In-Memory Testing**: Utilize in-memory database for integration testing.

## Technologies

- **.NET 8**: The application is built using .NET 6.
- **Entity Framework Core**: ORM for data access.
- **xUnit**: Testing framework for unit and integration tests.
- **PostgreSql**: Used for data storage.
- **In-Memory Database**: Used for testing to avoid database dependency.

## Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) - Required to build and run the application.
- [Visual Studio Code](https://code.visualstudio.com/) or [Visual Studio](https://visualstudio.microsoft.com/) - IDE for development.

### Installation

1. **Clone the Repository**

   ```bash
   git clone https://github.com/jurck222/Racuni.git
   cd Racuni
   ```
2. **Install Dependencies**

   Restore the NuGet packages required for the project.

   ```bash
   dotnet restore
   ```

### Running the Application

1. **Run the Applicati	on**

   Start the application using the following command:

   ```bash
   dotnet run --project Racuni/Racuni.csproj
   ```

2. **Run the Application with Debugging**

   To run the application with debugging, you can use the following command:

   ```bash
   dotnet watch run --project Racuni/Racuni.csproj
   ```

   This command will watch for file changes and restart the application and open swagger OpenApi documentation for the api, which is useful during development.
