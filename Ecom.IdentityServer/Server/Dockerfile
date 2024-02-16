#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
ENV ASPNETCORE_ENVIRONMENT=Development
ENV ASPNETCORE_URLS=http://+:80
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Server.csproj", "."]
RUN dotnet restore "./Server.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "Server.csproj" -c Release -o /app/build
RUN ls -al
FROM build AS publish
RUN dotnet publish "Server.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Server.dll"]


##See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.
#
#FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
#WORKDIR /app
#EXPOSE 5000:5000
#EXPOSE 5001:5001
#
#FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
#WORKDIR /src
#COPY ["./Ecom.ProductService/Ecom.ProductService.csproj", "."]
#COPY ["./Ecom.ProductService/Ecom.ProductService.Contracts/Ecom.ProductService.Contracts.csproj", "Ecom.ProductService.Contracts/"]
#COPY ["./Ecom.ProductService/NuGet.Config", "./nuget.config"]
#COPY ["./packages", "./packages"]
#RUN dotnet restore "./Ecom.ProductService.csproj"
#COPY . .
#WORKDIR "/src/."
#RUN dotnet build "Ecom.ProductService.csproj" -c Release -o /app/build
#
#FROM build AS publish
#RUN dotnet publish "Ecom.ProductService.csproj" -c Release -o /app/publish /p:UseAppHost=false
#
#FROM base AS final
#WORKDIR /app
#COPY --from=publish /app/publish .
#ENTRYPOINT ["dotnet", "Ecom.ProductService.dll"]