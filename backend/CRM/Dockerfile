FROM mcr.microsoft.com/dotnet/sdk:7.0-alpine AS build

WORKDIR /app

COPY *.sln .

COPY CRM.Api/*.csproj ./CRM.Api/
COPY CRM.Domain/*.csproj ./CRM.Domain/
COPY CRM.Infrastructure/*.csproj ./CRM.Infrastructure/

RUN dotnet restore -r alpine-x64

COPY CRM.Api/. ./CRM.Api/
COPY CRM.Domain/. ./CRM.Domain/
COPY CRM.Infrastructure/. ./CRM.Infrastructure/

RUN dotnet build

RUN dotnet publish -c Release -o /out -r alpine-x64

# Final stage/image
FROM mcr.microsoft.com/dotnet/runtime-deps:7.0-alpine AS publish

WORKDIR /app
COPY --from=build /out .

EXPOSE 8081
ENTRYPOINT ["./CRM.Api"]