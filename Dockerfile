#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
LABEL io.k8s.display-name="app name" \
      io.k8s.description="container description..." \
      io.openshift.expose-services="8080:http,8088:https"

EXPOSE 8080
EXPOSE 8088
ENV ASPNETCORE_URLS=http://*:8080;https://*:8088

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["WebApplication1/WebApplication1.csproj", "WebApplication1/"]
RUN dotnet nuget add source https://aresccb.pkgs.visualstudio.com/_packaging/NuggetFeedCecoban/nuget/v2 -n NuggetFeedCecoban --username NotUsed --password oivuwoxvxrkrjuau2uhdqgtbu7oh7dg422moudslrxyqnj575mba --store-password-in-clear-text
RUN dotnet restore "WebApplication1/WebApplication1.csproj"
COPY . .
WORKDIR "/src/WebApplication1"
RUN dotnet build "WebApplication1.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebApplication1.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebApplication1.dll"]