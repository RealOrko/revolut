FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /App

# Copy everything
COPY ./WebAPI/ ./
# Restore as distinct layers
RUN dotnet restore
# Build and publish a release
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0

ENV DataContext__Host=db
ENV DataContext__Port=5432
ENV DataContext__User=postgres
ENV DataContext__Password=postgres
ENV DataContext__Database=postgres

WORKDIR /App
COPY --from=build-env /App/out .
ENTRYPOINT ["dotnet", "WebAPI.dll"]
