# PALAVEN - Project Documentation

## 1. Introduction
This project leverages .NET 6 (Long Term Support) to create a backend-focused solution incorporating OpenAI's Chat and Embeddings services, Pinecone's HTTP API, and Azure AI for PDF text extraction. This phase does not include web APIs or Azure Functions but features three console applications for data ingestion, vector index creation, and testing interactions with OpenAI's Chat API.

## 2. Requirements and Setup
### System and Software Requirements
- .NET 6 (LTS) environment.
- Access to OpenAI APIs.
- Pinecone HTTP API setup.
- Azure AI services for text extraction.

## 3. System Architecture
![Palaven-vBeta-Deployment - Deployment](https://github.com/erickbr15/palaven-llm-sat/assets/72543531/b1cc5822-9385-4139-90b6-a43fd229cb3d)


## 4. OpenAI API Integration
### Chat and Embeddings Services
The project integrates OpenAI's API through HTTP requests, utilizing the .NET `IHttpClient` for connectivity. Key components of this integration include:
- **Endpoints**:
  - Chat completion endpoint: Utilized for 1. Extracting the article text, 2. Generating the synthetic questions about each ingested article, 3. Generating responses based on user inputs
  - Embeddings creation endpoint: Employed for generating embeddings from text data.
- **Models**:  
  - `gpt-4`: Employed for state-of-the-art language understanding and generation capabilities in chat completions.
  - `text-embedding-ada-002`: Used for creating embeddings that capture semantic meanings of texts.

## 5. Interacting with Pinecone
### Using Pinecone's HTTP API
Pinecone's capabilities are harnessed in this project using its HTTP API, with connectivity managed via the .NET `IHttpClient`. This integration facilitates efficient data management and retrieval using vector search techniques.

### Pinecone HTTP API Integration
- **Connection Setup**:
  - Custom setup using .NET `IHttpClient` for a robust and secure connection to Pinecone's API.
- **Endpoints Usage**:
  - **Upsert Endpoint**: Employed for adding or updating data in the index. This process is crucial for maintaining an up-to-date and relevant dataset for vector searches.
  - **Query Endpoint**: Utilized for executing vector searches. This includes the use of cosine distance measures to find the most relevant vectors in response to queries.

## 6. Data Ingestion Process
In this section, we describe the process of ingesting data into the system, which is a critical initial step for building our dataset and enabling efficient search and retrieval operations.

### Ingestion Workflow
![Palaven-vBeta-Deployment - Data](https://github.com/erickbr15/palaven-llm-sat/assets/72543531/41b14620-f513-4817-899a-213e798309f9)


## 7. Document Structure for Ingestion
This section outlines the structure of the documents used in the data ingestion process, providing clarity on how data is organized and prepared during the ingestion process.

### Document Format
- The Datasource documents are all PDF, same format
- All the working documents are in JSON format

![Palaven-vBeta-Deployment - Documents](https://github.com/erickbr15/palaven-llm-sat/assets/72543531/f8c10d0a-1153-46cc-b3db-03d6114416dc)


## 8. RAG Implementation
### Scenario 1
![Palaven-vBeta-Deployment - RAG-1](https://github.com/erickbr15/palaven-llm-sat/assets/72543531/8c864803-7708-4a4f-868d-65a84e6c29fd)

### Scenario 2
![Palaven-vBeta-Deployment - RAG - 2](https://github.com/erickbr15/palaven-llm-sat/assets/72543531/3976a083-f5b2-4363-8581-70443c0b831d)


## 11. Licensing and Copyright
This project is licensed under the MIT License. For full license details, please refer to the [LICENSE.md](./LICENSE.md) file in this repository.

© 2024 Erick Badillo Rangel. All rights reserved.
