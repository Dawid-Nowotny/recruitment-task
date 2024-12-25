# Hotel Booking Management Application

A simple C# console application for managing hotel data and bookings. This project demonstrates loading hotel and booking information from JSON files and provides basic functionality for checking room availability.

## Getting Started

These instructions will get you a copy of the project up and running on your local machine.

### Prerequisites
- .NET 8

### Running the project
1. Clone the repository:
```bash
git clone https://github.com/Dawid-Nowotny/recruitment-task.git
cd .\recruitment-task\RecruitmentTask\
```

2. Restore the required packages:
  ```bash
  dotnet restore
  ```

3. Build the Application:<br>
  Build the project to ensure all dependencies are properly configured:
  ```bash
  dotnet build
  ```

4. Run the Application:<br>
To run the application, use the following command:
  ```bash
  dotnet run
  ```

### Running Tests
1. Navigate to the repository folder:
  ```bash
  cd recruitment-task
  ```

2. Run all tests:
  ```bash
  dotnet test .\Tests\
  ```

### Dependencies
This project uses:
- Newtonsoft.Json (13.0.3) for JSON handling.
- xUnit (2.9.0) for unit testing.
