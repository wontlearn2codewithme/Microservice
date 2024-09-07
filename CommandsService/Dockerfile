# Fase de construcción
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app


# Copiar el archivo de proyecto y restaurar las dependencias
COPY ["CommandsService.csproj", "./"]
RUN dotnet restore

# Copiar el resto de los archivos y construir la aplicación
COPY [".", "./"]
RUN dotnet publish -c Release -o /app/build

# Fase final - Imagen de runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

# Copiar los artefactos de la build desde la fase de construcción
COPY --from=build-env /app/build .

# Configurar el entrypoint
ENTRYPOINT ["dotnet", "CommandsService.dll"]
