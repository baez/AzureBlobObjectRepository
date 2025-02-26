# AzureBlobObjectRepository
A repository to store serialized objects in Azure Blob Storage.
Technologies used: C#, Asp.Net Core, .Net Standard and Azure Blob Storage

# Goal: 
Create a Repository of serialized objects on Azure Blob and expose this functionality as a WebApi Service. 
For the time being focused on JSON serialization only.  
The solution is designed to store serialized objects in Azure Blob Storage. This repository provides a Web API service that facilitates the storage and retrieval of JSON-serialized objects, leveraging technologies such as ASP.NET Core and .NET Standard.

## Features

- **Azure Blob Storage Integration**: Seamlessly store and manage serialized objects within Azure's scalable blob storage service.
- **Web API Service**: Exposes RESTful endpoints for interacting with the object repository, enabling easy integration with other applications.
- **JSON Serialization**: Focuses on JSON serialization for object storage, ensuring compatibility and ease of use.

## Project Structure

- `AzureBlobs/`: Contains the implementation of Azure Blob Storage interactions.
- `AzureBlobsCore/`: Core library defining interfaces and models for the repository.
- `UnitTests/`: Unit test projects to ensure the reliability and correctness of the repository's functionality.
- `.gitignore`: Specifies files and directories to be ignored by Git.
- `README.md`: This file, providing an overview of the project.

## Getting Started

### Prerequisites

- .NET SDK installed
- Visual Studio or another compatible C# IDE
- Azure subscription with access to Blob Storage
- Git (optional, for cloning the repository)

### Installation & Setup

1. **Clone the repository:**

   ```bash
   git clone https://github.com/baez/AzureBlobObjectRepository.git
   ```

2. **Open the solution file:**

   Navigate to the project directory and open the solution in Visual Studio.

3. **Configure Azure Storage:**

   - Set up an Azure Blob Storage account.
   - Update the application configuration with your Azure Storage connection string.

4. **Restore dependencies and build the solution:**

   Use Visual Studio's build feature to restore NuGet packages and compile the project.

5. **Run the application:**

   Start the Web API service using Visual Studio's debugging tools or CLI commands.

## Usage

- **Storing Objects:** Send a POST request to the API with the JSON object to store it in Azure Blob Storage.
- **Retrieving Objects:** Send a GET request to retrieve the stored JSON objects.

## License

This project is licensed under the Apache License 2.0 

---

For any inquiries or collaboration opportunities, feel free to reach out!

