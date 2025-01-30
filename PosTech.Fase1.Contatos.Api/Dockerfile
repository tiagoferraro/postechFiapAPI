# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["PosTech.Fase1.Contatos.Api/PosTech.Fase1.Contatos.Api.csproj", "PosTech.Fase1.Contatos.Api/"]
COPY ["PosTech.Fase1.Contatos.Application/PosTech.Fase1.Contatos.Application.csproj", "PosTech.Fase1.Contatos.Application/"]
COPY ["PosTech.Fase1.Contatos.Infra/PosTech.Fase1.Contatos.Infra.csproj", "PosTech.Fase1.Contatos.Infra/"]
COPY ["PosTech.Fase1.Contatos.Domain/PosTech.Fase1.Contatos.Domain.csproj", "PosTech.Fase1.Contatos.Domain/"]
COPY ["PosTech.Fase1.Contatos.IoC/PosTech.Fase1.Contatos.IoC.csproj", "PosTech.Fase1.Contatos.IoC/"]
RUN dotnet restore "./PosTech.Fase1.Contatos.Api/PosTech.Fase1.Contatos.Api.csproj"
COPY . .
WORKDIR "/src/PosTech.Fase1.Contatos.Api"
RUN dotnet build "./PosTech.Fase1.Contatos.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./PosTech.Fase1.Contatos.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PosTech.Fase1.Contatos.Api.dll"]