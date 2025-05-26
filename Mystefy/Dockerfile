# Use the official .NET runtime image as base
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS base
WORKDIR /app
EXPOSE 5000
EXPOSE 5001

# Use the SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["<Your .csproj file>", "<Your App Folder>"]
RUN dotnet restore "<Your .csproj file>"
COPY . .
WORKDIR "/src/<app folder>"
RUN dotnet publish -c Release -o /app/publish

# Final runtime image
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
CMD ["dotnet", "<app folder>.dll"]
