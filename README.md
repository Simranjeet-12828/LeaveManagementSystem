# Leave Management System

A robust, role-based web application for managing employee leave requests, approvals, and history. Built using ASP.NET MVC, C#, and SQL Server, this system streamlines leave management for organizations with dedicated portals for both managers and employees.

## Table of Contents
- [Features](#features)
- [Tech Stack](#tech-stack)
- [Getting Started](#getting-started)
- [Project Structure](#project-structure)
- [Usage](#usage)
- [Contributing](#contributing)
- [License](#license)

## Features

- **Role-Based Portals:** Separate dashboards for employees and managers.
- **Employee Dashboard:** Submit leave requests, track request status, view leave history.
- **Manager Dashboard:** Review, approve, or reject leave requests; analyze team leave patterns.
- **Authentication & Authorization:** Secure login system using ASP.NET Identity.
- **Data Management:** SQL Server with Entity Framework for efficient data storage and retrieval.
- **Scalable MVC Architecture:** Ensures maintainability and clear separation of concerns.

## Tech Stack

- **Backend:** .NET, C#, ASP.NET MVC, Entity Framework
- **Frontend:** HTML5, CSS3
- **Database:** SQL Server
- **Authentication:** ASP.NET Identity

## Getting Started

### Prerequisites

- [Visual Studio](https://visualstudio.microsoft.com/)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
- .NET Framework (version compatible with ASP.NET MVC)
- Required NuGet packages (Entity Framework, ASP.NET Identity, etc.)

### Installation Steps

1. **Clone the repository**
   ```bash
   git clone https://github.com/Simranjeet-12828/LeaveManagementSystem.git
   ```

2. **Open the project in Visual Studio**

3. **Restore NuGet Packages**
   - Right-click on the solution and select "Restore NuGet Packages" to install dependencies.

4. **Set up SQL Server Database**
   - Ensure SQL Server is installed and running.
   - Update the connection string in `Web.config` with your SQL Server details.
   - Run Entity Framework migrations (if applicable) to create database tables.

5. **Build and Run**
   - Press F5 or select "Start Debugging" in Visual Studio.

## Project Structure

- `Controllers/` – MVC controllers for handling web requests
- `Models/` – Entity Framework models representing data structure
- `Views/` – Razor views for UI rendering
- `Data/` – Database context and migration files
- `App_Start/` – Configuration files

## Usage

- **Employees:** Log in to submit leave requests and check status/history.
- **Managers:** Log in to review, approve, or reject requests and view analytics.

## Contributing

Contributions are welcome! Please fork the repository and submit a pull request.
- Ensure code quality and follow MVC architecture best practices.
- For major changes, open an issue first to discuss your ideas.

## License

This project is licensed under the [MIT License](LICENSE).

---

**Contact:** For questions or support, please contact [Simranjeet-12828](https://github.com/Simranjeet-12828).
