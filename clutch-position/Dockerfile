#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 81
EXPOSE 444

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["clutch-position.csproj", "."]
RUN dotnet restore "./clutch-position.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "clutch-position.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "clutch-position.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "clutch-position.dll"]