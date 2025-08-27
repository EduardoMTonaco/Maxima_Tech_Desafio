# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia os arquivos da solução
COPY Maxima_Tech_Desafio.sln ./

# Copia os projetos separadamente
COPY Maxima_Tech_API/ Maxima_Tech_API/
COPY Maxima_Tech_Web/ Maxima_Tech_Web/
COPY Utility/ Utility/

# Restaura a solução (restaura todos os projetos)
RUN dotnet restore Maxima_Tech_Desafio.sln

# Publica API
RUN dotnet publish Maxima_Tech_API/Maxima_Tech_API.csproj -c Release -o /app/publish/api

# Publica Web
RUN dotnet publish Maxima_Tech_Web/Maxima_Tech_Web.csproj -c Release -o /app/publish/web

# Etapa de runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

# Copia API para runtime
COPY --from=build /app/publish/api ./api

# Copia Web para runtime
COPY --from=build /app/publish/web ./web

# Ajuste o ENTRYPOINT para a API (ou crie um script para iniciar ambos)
ENTRYPOINT ["dotnet", "api/Maxima_Tech_API.dll"]