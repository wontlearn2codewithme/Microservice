## Consulte https://aka.ms/customizecontainer para aprender a personalizar su contenedor de depuración y cómo Visual Studio usa este Dockerfile para compilar sus imágenes para una depuración más rápida.
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

# Imagen para compilar la solución
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /src

# Copiar el archivo de solución y todos los archivos de proyecto
COPY ["Microservices.sln", "."]
COPY ["PlatformService/PlatformService.csproj", "PlatformService/"]
COPY ["CommandsService/CommandsService.csproj", "CommandsService/"]
COPY ["PlatformService.Application/PlatformService.Application.csproj", "PlatformService.Application/"]
COPY ["PlatformService.Contracts/PlatformService.Contracts.csproj", "PlatformService.Contracts/"]
COPY ["PlatformService.Repository/PlatformService.Repository.csproj", "PlatformService.Repository/"]

# Restaurar dependencias (usa el archivo .sln para que las referencias entre proyectos se resuelvan)
RUN dotnet restore

# Copiar el resto de los archivos del proyecto
COPY . .

# Cambiar el directorio de trabajo a PlatformService para construir el proyecto principal
WORKDIR "/src/PlatformService"

# Construir la aplicación
RUN dotnet build "PlatformService.csproj" -c Release -o /app/build

# Publicar la aplicación
FROM build-env AS publish
RUN dotnet publish "PlatformService.csproj" -c Release -o /app/publish

# Imagen final que ejecutará la aplicación
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PlatformService.dll"]
