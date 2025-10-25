# Stage 1: Build the application
# Use the .NET 8.0 SDK (your project targets .NET 8)
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src

# Copy the solution and project files first
COPY "Basic Task Manager.sln" .
COPY "Basic Task Manager.csproj" .

# Restore dependencies
RUN dotnet restore "Basic Task Manager.sln"

# Copy the rest of the source code
COPY . .

# Build and publish the application
RUN dotnet publish "Basic Task Manager.csproj" -c Release -o /app/publish

# Stage 2: Create the final runtime image
# Use the smaller ASP.NET runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final

WORKDIR /app
COPY --from=build /app/publish .

# IMPORTANT: Render provides a PORT environment variable.
# This command tells your .NET app to listen on the port Render expects.
ENTRYPOINT ["dotnet", "Basic Task Manager.dll", "--urls", "http://0.0.0.0:${PORT}"]