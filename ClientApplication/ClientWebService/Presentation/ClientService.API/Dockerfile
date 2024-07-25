
FROM mcr.microsoft.com/dotnet/aspnet:8.0-nanoserver-1809 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0-nanoserver-1809 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Presentation/ClientService.API/ClientService.API.csproj", "Presentation/ClientService.API/"]
COPY ["Infrastructure/ClientService.Infrastructure/ClientService.Infrastructure.csproj", "Infrastructure/ClientService.Infrastructure/"]
COPY ["Core/Application/ClientService.Application.csproj", "Core/Application/"]
COPY ["Core/ClientService.Domain/ClientService.Domain.csproj", "Core/ClientService.Domain/"]
COPY ["Persistance/ClientService.Persistance/ClientService.Persistance.csproj", "Persistance/ClientService.Persistance/"]
COPY ["Shared/EventShared/EventShared.csproj", "Shared/EventShared/"]
RUN dotnet restore "./Presentation/ClientService.API/ClientService.API.csproj"
COPY . .
WORKDIR "/src/Presentation/ClientService.API"
RUN dotnet build "./ClientService.API.csproj" -c %BUILD_CONFIGURATION% -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ClientService.API.csproj" -c %BUILD_CONFIGURATION% -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ClientService.API.dll"]