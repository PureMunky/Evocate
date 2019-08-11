FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build-env
WORKDIR /app

# Copy everything else and build
COPY . ./

RUN dotnet publish -c Release -o out Tete.Job/Tete.Job.csproj

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:2.2
WORKDIR /app
COPY --from=build-env /app/Tete.Job/out .
ENTRYPOINT ["dotnet", "Tete.Job.dll"]