# Use the official .NET 7 SDK as a build image
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app

# Copy the project file and restore dependencies
COPY *.csproj ./
RUN dotnet restore

# Copy the rest of the source code
COPY . ./

# Build the application
RUN dotnet publish -c Release -o out

# Use the official ASP.NET runtime image as a runtime image
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS runtime
WORKDIR /app
COPY --from=build /app/out ./

# Expose the port the application will listen on
EXPOSE 80

# Install the .NET SDK for running .NET CLI commands (needed for dotnet)
RUN dotnet --list-sdks

# Run the application
ENTRYPOINT ["dotnet", "RockPaperScissorsAPI.dll"]
