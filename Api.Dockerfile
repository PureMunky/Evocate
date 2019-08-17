FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build-env
WORKDIR /app

# Copy everything else and build
COPY . ./
RUN dotnet publish -c Release -o out Tete.Api/Tete.Api.csproj

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:2.2
EXPOSE 80

ENV SQLCONNSTR_DefaultConnection "Server=tcp:tete-db-svc,1433; Database=Tete; User Id=sa; Password=tetePassword!;"

WORKDIR /app
COPY --from=build-env /app/Tete.Api/out .
ENTRYPOINT ["dotnet", "Tete.Api.dll"]