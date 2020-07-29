FROM mcr.microsoft.com/dotnet/core/runtime:3.1 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["Public Bot/Luminous.csproj", "Public Bot/"]
RUN dotnet restore "Public Bot/Luminous.csproj"
COPY . .
WORKDIR "/src/Public Bot"
RUN dotnet build "Luminous.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Luminous.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Luminous.dll"]
