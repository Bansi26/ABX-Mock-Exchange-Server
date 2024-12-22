# ABX Exchange Client

This project contains a C# client that connects to the ABX Mock Exchange server to request and receive stock ticker data, handle missing sequences, and save the data as a JSON file.

## Prerequisites

Before running the client application, ensure the following software is installed:

- **Node.js** (v16.17.0 or higher)  
  [Install Node.js](https://nodejs.org/)

- **.NET SDK** (v6.0 or higher)  
  [Install .NET SDK](https://dotnet.microsoft.com/download)

## Setup and Execution

### 1. **Run the ABX Mock Exchange Server**

1. Download and extract the `abx_exchange_server.zip` file.
2. Open a terminal and navigate to the folder containing `main.js`.
3. Run the server with the command:
   ```bash
   node main.js
   ```
   The server will start listening on `127.0.0.1:3000`.

### 2. **Build and Run the C# Client**

1. Download and extract the C# project files.
2. Open a terminal and navigate to the project directory.
3. Build the project:
   ```bash
   dotnet build
   ```
4. Run the C# client application:
   ```bash
   dotnet run
   ```
   The client will connect to the ABX server, request all stock ticker data, handle missing sequences, and save the data as `output.json`.

### 3. **Verify the Output**

- Check the project directory for the `output.json` file. It contains the stock ticker data retrieved from the server.

## Troubleshooting

- **Connection Error**: Ensure the ABX server is running and accessible at `127.0.0.1:3000`.
- **Missing Output File**: Check for errors in the C# client application, especially related to network issues or server connectivity.

## License

This project is open source and available under the MIT License.

---

This `README.md` provides a clear guide to running the ABX Mock Exchange client and ensures users can easily set up and test the application.
