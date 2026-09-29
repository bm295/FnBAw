# syntax=docker/dockerfile:1
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

FROM node:24-alpine AS client-build
WORKDIR /client
COPY client/package*.json ./
RUN npm ci
COPY client/ ./
RUN npm run build

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY src/FnBManagement.Web/FnBManagement.Web.csproj src/FnBManagement.Web/
RUN dotnet restore src/FnBManagement.Web/FnBManagement.Web.csproj
COPY . .
RUN dotnet publish src/FnBManagement.Web/FnBManagement.Web.csproj -c Release -o /app/publish /p:UseAppHost=false
COPY --from=client-build /client/dist/client/browser/ /app/publish/wwwroot/

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "FnBManagement.Web.dll"]
