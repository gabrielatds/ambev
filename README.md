# Project Documentation: Order Management

This document outlines the steps to configure and utilize the order management functionality of this project.

## Prerequisites

* Ensure you have a running PostgreSQL instance.
* The project must be configured with Entity Framework Core.

## Step 1: Connection String Configuration

For the application to connect to your PostgreSQL database, you need to configure the connection string in the development configuration file.

1.  **Locate the `appsettings.Development.json` file** in the root of your project.
2.  **Edit the file** to include your PostgreSQL connection string within the `ConnectionStrings` section. The structure should resemble this:

    ```json
    {
      "ConnectionStrings": {
        "DefaultConnection": "Host={YOUR_HOST};Port={YOUR_PORT};Database={YOUR_DATABASE};Username={YOUR_USERNAME};Password={YOUR_PASSWORD};"
      }
    }
    ```

    **Important:** Replace the placeholders `{YOUR_HOST}`, `{YOUR_PORT}`, `{YOUR_DATABASE}`, `{YOUR_USERNAME}`, and `{YOUR_PASSWORD}` with the correct credentials of your PostgreSQL instance.

## Step 2: Updating the Database with Entity Framework

After configuring the connection string, you need to apply the Entity Framework Core migrations to create or update your database schema.

1.  **Open the terminal** in the root of your project.
2.  **Execute the following command** to apply any pending migrations:

    ```bash
    dotnet ef database update
    ```

    This command will read the connection settings and the migrations defined in your project to ensure your database is up-to-date with your application's data model.

## Step 3: Using the Orders API (api/orders)

The project provides endpoints at the `/api/orders` route to perform CRUD (Create, Read, Update, Delete) operations on orders.

### Available Endpoints

* **`GET /api/orders/{id}`**: Returns a specific order with the provided ID.
* **`POST /api/orders`**: Creates a new order. The request body should contain the order data in JSON format.
* **`PUT /api/orders/{id}`**: Updates an existing order with the provided ID. The request body should contain the updated order data in JSON format.
* **`DELETE /api/orders/{id}`**: Deletes the order with the provided ID.
