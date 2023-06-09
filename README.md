# Lative

Lative is an application that processes sales data and imports it into a database. It provides functionalities for reading data from a CSV file, performing bulk insertions, and initializing database tables.

## Getting Started

To get started with the Lative application, follow the steps below:

### Prerequisites

Before running the application locally, ensure that you have the following software installed:

- [.NET 7.0 SDK](https://dotnet.microsoft.com/download/dotnet/7.0)
- PostgreSQL database

### Configuration

1. Configure the database connection string in the `appsettings.json` file and in the `docker-compose.yml`.
2. Update the CSV file path and bulk size configurations in the `appsettings.json` file if needed.

### Installation

1. Clone the repository using the command: `git clone https://github.com/<your-username>/Lative.git`
2. With a console, go to the root of the project and run the following command: `docker-compose up -d --build`

## Usage

The Lative application provides the following features:

- Reading sales data from a CSV file.
- Bulk insertion of sales and dimensions into the database.
- Database table initialization.

To run the application locally:

1. Build the solution to ensure all dependencies are resolved.
2. Run the application.
3. The application will read the sales data from the specified CSV file and perform the necessary database operations.

## Architecture

The Lative application follows the following architecture:

### N-Layer Architecture

The application is structured using the N-Layer architecture, which separates concerns into multiple layers:

- Presentation Layer: Contains the entry point of the application and interacts with the user.
- Application Layer: Handles application logic and orchestrates data processing.
- DataAccess Layer: Provides access to the database and performs data operations.
- Infrastructure Layer: Contains infrastructure-related implementations, such as table initialization.

### Technologies Used

The Lative application utilizes the following technologies:

- .NET 7.0
- PostgreSQL database
- NUnit for unit testing
- Docker

## Authors

* **Bonaventura Gabriel**  - [Linkedin](https://www.linkedin.com/in/gabriel-bonaventura) -  [GitHub](https://github.com/gabybonaventura)

## Acknowledgments

Thank you for using the Lative application. If you have any questions or need assistance, please feel free to reach out.