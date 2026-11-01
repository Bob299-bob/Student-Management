# Stage 1: Build application
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build

WORKDIR /src

COPY Student.Api.csproj ./

RUN dotnet restore

COPY . .

RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

# Stage 2: Run application
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final

WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 10000

ENTRYPOINT ["dotnet", "Student.Api.dll"]