# JWT Token Implementation with ASP.NET Core Web API, React, and MSSQL

This GitHub repository provides a comprehensive example of JWT token implementation in a full-stack web application. The project is built with ASP.NET Core Web API, React, and MSSQL.

## Getting Started

To run the project, follow these steps:

### Backend Setup

1. Ensure you have the latest version of Visual Studio installed.
2. Open the backend solution in Visual Studio.
3. Navigate to the `appsettings.json` file and update the connection string with your MSSQL server details.
4. Create the necessary tables in your MSSQL database. Refer to the provided SQL scripts in the `DatabaseScripts` folder.
5. Query for Registration table 

CREATE TABLE [dbo].[Registration] (
    [ID]       INT           IDENTITY (1, 1) NOT NULL,
    [Username] VARCHAR (100) NULL,
    [Password] VARCHAR (100) NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC)
);


6. Query for Product table 

CREATE TABLE [dbo].[Product] (
    [Id]          INT           NOT NULL,
    [ProductName] VARCHAR (255) NULL,
    [Description] VARCHAR (255) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

### Frontend Setup

1. Make sure you have Visual Studio Code installed.
2. Open the frontend folder in Visual Studio Code.
3. Install the required dependencies by running `npm install` in the terminal.

## Running the Application

1. Start the backend by running the ASP.NET Core Web API project in Visual Studio.
2. Launch the frontend by running `npm start` in the terminal within the frontend folder.

You're now ready to explore the JWT token implementation project!

Please note that this project serves as a learning resource and can be customized according to your specific requirements.

If you encounter any issues or have questions, feel free to raise an issue in the repository.

Happy coding!
