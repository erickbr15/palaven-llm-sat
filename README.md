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

## 6. RAG Implementation
### Role of RAG in the LLM
- Explanation of how RAG (Retrieval-Augmented Generation) is implemented.
- Specific examples showing the impact of RAG on the project.

## 11. Licensing and Copyright
This project is distributed under [specify license type].
© [Year] [Your Name or Organization's Name]. All rights reserved.

## 12. Contact and Support
For support or inquiries, please contact:
- Email: [your-email@example.com]
- GitHub: [GitHub link]
- LinkedIn: [LinkedIn profile link]
