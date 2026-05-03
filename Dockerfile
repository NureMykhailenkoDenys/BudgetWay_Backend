# 1. Stage: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY *.sln ./
COPY BudgetWay.Backend/*.csproj ./BudgetWay.Backend/
RUN dotnet restore

COPY BudgetWay.Backend/. ./BudgetWay.Backend/

WORKDIR /src/BudgetWay.Backend
RUN dotnet publish -c Release -o /app

# 2. Stage: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app .

EXPOSE 5000
ENTRYPOINT ["dotnet", "BudgetWay.Backend.dll"]