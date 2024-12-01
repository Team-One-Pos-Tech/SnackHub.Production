# SnackHub.Production
[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=Team-One-Pos-Tech_SnackHub.Production&metric=alert_status)](https://sonarcloud.io/summary/new_code?id=Team-One-Pos-Tech_SnackHub.Production)
[![Coverage](https://sonarcloud.io/api/project_badges/measure?project=Team-One-Pos-Tech_SnackHub.Production&metric=coverage)](https://sonarcloud.io/summary/new_code?id=Team-One-Pos-Tech_SnackHub.Production)

## Overview
SnackHub.Production is a .NET service designed to manage production orders for a Snack Hub restaurant. This project includes an API, database context, and behavior tests to ensure the functionality of the system.

More details about SnackHub Project can be found [here](https://github.com/Team-One-Pos-Tech/SnackHub/wiki)

### The main gateway for the SnackHub project can be found [here](https://github.com/Team-One-Pos-Tech/SnackHub.ApiGateway)

## Getting Started

### Prerequisites

- .NET 8.0 SDK or later
- PostgreSQL
- Docker

### Installation

1. Clone the repository:
    ```sh
    git clone https://github.com/Team-One-Pos-Tech/SnackHub.Production.git
    cd SnackHub.Production
    ```

2. Install dependencies:
    ```sh
    dotnet restore
    ```

### Running the Application

To run the application locally, use the following command:
```sh
cd /src/SnackHub.Production.Api
dotnet run
```

### Running With Docker
Running with PostgreSQL and RabbitMQ
```sh
cd deploy
docker compose up
```

### Running Tests

To run the tests, use the following command:
Ensure you are running Docker locally.
```sh
dotnet test
```

## Contributing

Contributions are welcome! Please open an issue or submit a pull request.

## License

This project is licensed under the MIT License.

