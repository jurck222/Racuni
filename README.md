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
   
### Database setup
   - The app uses a PostgreSQL database for data storage so you have to install [PostgreSQL](https://www.postgresql.org/download/windows/) server on your local machine. I would also recommend using a gui client like [PgAdmin](https://www.pgadmin.org/download/).
   - Add a database called invoices through a gui client of your choice or by running the following sql query:
      ```sql
      CREATE DATABASE invoices
          WITH
          OWNER = "user"
          ENCODING = 'UTF8'
          LOCALE_PROVIDER = 'libc'
          CONNECTION LIMIT = -1
          IS_TEMPLATE = False;
      ```
   - Apply migrations in VisualStudio PackageManagerConsole with `Update-Database` command or by running the following command in terminal:
      ```bash
      dotnet ef database update
      ```
   - To seed the data for testing purposes use:
     ```bash
     dotnet run seeddata
     ```
### :warning:Disclaimer
The connection string in the app is set to `localhost` with `port 5434` using a user with `username=user` and `password=user`, and points to a database called `invoices`. If you want to change any of these options you can edit the DefaultConnection in `appsettings.json` file.
     
