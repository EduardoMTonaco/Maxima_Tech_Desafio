# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia os projetos necessários
COPY Maxima_Tech_API/ Maxima_Tech_API/
COPY Maxima_Tech_Web/ Maxima_Tech_Web/
COPY Maxima_Tech_RabbitMQ/ Maxima_Tech_RabbitMQ/
COPY Utility/ Utility/
COPY Maxima_Tech_Desafio.sln ./

# Restaura somente o projeto da API
RUN dotnet restore Maxima_Tech_API/Maxima_Tech_API.csproj

# Publica somente a API
RUN dotnet publish Maxima_Tech_API/Maxima_Tech_API.csproj -c Release -o /app/publish

# Etapa de runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

# Copia somente a saída da API
COPY --from=build /app/publish .


# Define o ponto de entrada
ENTRYPOINT ["dotnet", "Maxima_Tech_API.dll"]