# Use the official .NET runtime image as base
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
# FROM mcr.microsoft.com/dotnet/sdk:9.0 AS base
WORKDIR /app
EXPOSE 5000
EXPOSE 5001

# Use the SDK image to build the app
# FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
# FROM --platform=linux/arm64 mcr.microsoft.com/dotnet/sdk:9.0 AS build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["Mystefy/Mystefy.csproj", "Mystefy/"]
RUN dotnet restore "Mystefy/Mystefy.csproj"

COPY . .
WORKDIR "/src/Mystefy"
RUN dotnet publish -c Release -o /app/publish

# Final runtime image
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
CMD ["dotnet", "Mystefy.dll"]
