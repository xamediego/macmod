FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8181

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["macmod/macmod.csproj", "macmod/"]
RUN dotnet restore "macmod/macmod.csproj"
COPY . .
WORKDIR "/src/macmod"
RUN dotnet build "macmod.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "macmod.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY macmod/appsettings.json .

ENV DATABASE_URL=""

ENTRYPOINT ["dotnet", "macmod.dll"]
