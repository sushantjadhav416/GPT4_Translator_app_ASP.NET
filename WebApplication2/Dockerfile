FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY "WebApplication2.csproj" .
RUN dotnet restore "WebApplication2.csproj"
COPY . .
RUN dotnet build . -c Release -o /app/build

RUN apt-get install curl

FROM build AS publish
RUN dotnet publish "WebApplication2.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .


ENV ASPNETCORE_ENVIRONMENT Development

ENTRYPOINT ["dotnet", "WebApplication2.dll"]
