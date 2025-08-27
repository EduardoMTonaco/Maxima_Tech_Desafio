# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia os projetos necessários
COPY Maxima_Tech_Web/ Maxima_Tech_Web/
COPY Utility/ Utility/
COPY Maxima_Tech_Desafio.sln ./

# Restaura somente o projeto Web
RUN dotnet restore Maxima_Tech_Web/Maxima_Tech_Web.csproj

# Publica o projeto Web
RUN dotnet publish Maxima_Tech_Web/Maxima_Tech_Web.csproj -c Release -o /app/publish

# Etapa de runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

# Copia apenas o build publicado
COPY --from=build /app/publish .

# Define o ponto de entrada
ENTRYPOINT ["dotnet", "Maxima_Tech_Web.dll"]