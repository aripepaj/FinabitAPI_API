# Use the official .NET 8 SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj and restore as distinct layers
COPY *.sln ./
COPY FinabitAPI/*.csproj ./FinabitAPI/
RUN dotnet restore

# Copy the rest of the source code
COPY FinabitAPI/. ./FinabitAPI/

WORKDIR /src/FinabitAPI

# Build the application
RUN dotnet publish -c Release -o /app/publish --no-restore

# Use the official ASP.NET Core runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

# Expose port 80
EXPOSE 80

# Set environment variables if needed (example)
# ENV ASPNETCORE_ENVIRONMENT=Production

# Start the application
ENTRYPOINT ["dotnet", "FinabitAPI.dll"]

# Build the Docker image
# docker build -t finabitapi .