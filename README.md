# KwikLinks API - URL Shortener API

This is a URL Shortener API built with ASP.NET Core and Dapper for database interactions. This API allows users to create shortened URLs and handle redirections to the original URLs.

## Table of Contents

- [Getting Started](#getting-started)
- [API Endpoints](#api-endpoints)
  - [Create Short URL](#create-short-url)
  - [Get Original URL and Redirect](#get-original-url-and-redirect)
  - [Get Original URL](#get-original-url)
- [Database Configuration](#database-configuration)
- [License](#license)

## Getting Started

### Prerequisites

- [.NET 6 SDK](https://dotnet.microsoft.com/download)
- [PostgreSQL](https://www.postgresql.org/download/) (or your preferred SQL database)
- [Azure CLI](https://docs.microsoft.com/en-us/cli/azure/install-azure-cli)

### Installation

1. Clone the repository:

    ```bash
    git clone https://github.com/yourusername/url-shortener-api.git
    cd url-shortener-api
    ```

2. Install dependencies:

    ```bash
    dotnet restore
    ```

3. Update `appsettings.json` with your database connection string:

    ```json
    {
      "ConnectionStrings": {
        "DefaultConnection": "Host=localhost;Database=urlshortener;Username=yourusername;Password=yourpassword"
      }
    }
    ```

4. Run the application:

    ```bash
    dotnet run
    ```

The API will be running at `http://localhost:5284`.

## API Endpoints

### Create Short URL

- **URL**: `/api/ShortUrl`
- **Method**: `POST`
- **Request Body**:
    ```json
    {
      "originalUrl": "https://example.com",
      "expiryInDays": 30
    }
    ```
- **Response**:
    ```json
    {
      "shortenedUrl": "abc123"
    }
    ```
- **Description**: This endpoint creates a shortened URL for the provided original URL and sets the expiration date based on the number of days provided.

### Get Original URL and Redirect

- **URL**: `/api/ShortUrl/rd/{shortenedUrl}`
- **Method**: `GET`
- **Response**: Redirects to the original URL.
- **Description**: This endpoint retrieves the original URL associated with the provided shortened URL and redirects the user to the original URL.

### Get Original URL

- **URL**: `/api/ShortUrl/{shortenedUrl}`
- **Method**: `GET`
- **Response**:
    ```json
    {
      "originalUrl": "https://example.com"
    }
    ```
- **Description**: This endpoint retrieves the original URL associated with the provided shortened URL without redirecting.

## Database Configuration

This project uses Dapper for database operations. The following SQL script can be used to set up the necessary database table:

```sql
CREATE TABLE ShortUrls (
    Id SERIAL PRIMARY KEY,
    OriginalUrl TEXT NOT NULL,
    ShortenedUrl TEXT NOT NULL,
    CreatedAt TIMESTAMP NOT NULL,
    ExpiryDate TIMESTAMP NOT NULL
);
