
FROM mcr.microsoft.com/dotnet/aspnet:8.0-nanoserver-1809 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0-nanoserver-1809 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["JobService.API/JobService.API.csproj", "JobService.API/"]
COPY ["JobService.BAL/BusinessLayer/BusinessLayer.csproj", "JobService.BAL/BusinessLayer/"]
COPY ["JobService.DAL/DataAccess/DataAccess.csproj", "JobService.DAL/DataAccess/"]
COPY ["Shared/EventShared/EventShared.csproj", "Shared/EventShared/"]
RUN dotnet restore "./JobService.API/JobService.API.csproj"
COPY . .
WORKDIR "/src/JobService.API"
RUN dotnet build "./JobService.API.csproj" -c %BUILD_CONFIGURATION% -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./JobService.API.csproj" -c %BUILD_CONFIGURATION% -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "JobService.API.dll"]