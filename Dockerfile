FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 7076

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Development
WORKDIR /src
COPY ["ShoesAndBlouse.API/ShoesAndBlouse.API.csproj", "ShoesAndBlouse.API/"]
COPY ["ShoesAndBlouse.Domain/ShoesAndBlouse.Domain.csproj", "ShoesAndBlouse.Domain/"]
COPY ["ShoesAndBlouse.Application/ShoesAndBlouse.Application.csproj", "ShoesAndBlouse.Application/"]
COPY ["ShoesAndBlouse.Infrastructure/ShoesAndBlouse.Infrastructure.csproj", "ShoesAndBlouse.Infrastructure/"]
RUN dotnet restore "ShoesAndBlouse.API/ShoesAndBlouse.API.csproj"
COPY . .
WORKDIR "/src/ShoesAndBlouse.API"
RUN dotnet build "ShoesAndBlouse.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Development
RUN dotnet publish "ShoesAndBlouse.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ShoesAndBlouse.API.dll"]
