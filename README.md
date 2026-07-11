# Gestão Financeira

A C# console application for personal finance management with secure authentication, income and expense tracking, category management, and financial reports.

The system allows users to register and manage their transactions organized by custom categories, view detailed financial reports, and track their balance over time, all stored in a MySQL database.

## Features

- Secure login and registration with encrypted passwords
- Create and manage custom categories for income and expenses
- Add and list financial transactions
- View full transaction history
- Filter transactions by date period
- Financial reports by category
- Balance summary with total income, expenses, and current balance

## Technologies

- C#
- .NET 8
- MySQL
- BCrypt.Net (password hashing)
- MySql.Data (database connector)
- DotNetEnv (environment variables)

## Database Setup

Create a MySQL database called `gestao_financeira` and run the SQL script located in `Database/schema.sql` to create the required tables.

## Configuration

Clone the repository, create a `.env` file in the project root based on `.env.example`, and fill in your database credentials:
DB_HOST=localhost
DB_PORT=3306
DB_NAME=gestao_financeira
DB_USERNAME=root
DB_PASSWORD=

## Author

Vitor Miranda Jeremias — [GitHub](https://github.com/Vitormjere)
