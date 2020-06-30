FROM mcr.microsoft.com/dotnet/core/runtime:3.1 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
RUN ls -la
COPY ["Public Bot/Public Bot.csproj", "Public Bot/"]
RUN dotnet restore "Public Bot/Public Bot.csproj"
COPY . .
WORKDIR "/src/Public Bot"
RUN dotnet build "Public Bot.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Public Bot.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
RUN ls -la
ENTRYPOINT ["dotnet", "PublicBot.dll"]
