#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
ENV TZ="America/Sao_Paulo"
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["Banking.Operation.Transfer.Command.Api/Banking.Operation.Transfer.Command.Api.csproj", "Banking.Operation.Transfer.Command.Api/"]
COPY ["Banking.Operation.Transfer.Command.CrossCutting.Ioc/Banking.Operation.Transfer.Command.CrossCutting.Ioc.csproj", "Banking.Operation.Transfer.Command.CrossCutting.Ioc/"]
COPY ["Banking.Operation.Transfer.Command.Infra.Data/Banking.Operation.Transfer.Command.Infra.Data.csproj", "Banking.Operation.Transfer.Command.Infra.Data/"]
COPY ["Banking.Operation.Transfer.Command.Domain/Banking.Operation.Transfer.Command.Domain.csproj", "Banking.Operation.Transfer.Command.Domain/"]
RUN dotnet restore "Banking.Operation.Transfer.Command.Api/Banking.Operation.Transfer.Command.Api.csproj"
COPY . .
WORKDIR "/src/Banking.Operation.Transfer.Command.Api"
RUN dotnet build "Banking.Operation.Transfer.Command.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Banking.Operation.Transfer.Command.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Banking.Operation.Transfer.Command.Api.dll"]