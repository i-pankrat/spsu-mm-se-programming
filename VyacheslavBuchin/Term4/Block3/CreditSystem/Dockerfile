﻿FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["CreditSystem.WebApi/CreditSystem.WebApi.fsproj", "CreditSystem.WebApi/"]
RUN dotnet restore "CreditSystem.WebApi/CreditSystem.WebApi.fsproj"
COPY . .
WORKDIR "/src/CreditSystem.WebApi"
RUN dotnet build "CreditSystem.WebApi.fsproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CreditSystem.WebApi.fsproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CreditSystem.WebApi.dll"]
