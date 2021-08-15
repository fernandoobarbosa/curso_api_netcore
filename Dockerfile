FROM mcr.microsoft.com/dotnet/sdk:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY [".", "."]
RUN dotnet restore "Api.Application/Api.Application.csproj"
COPY . .
WORKDIR "Api.Application"
RUN dotnet build "Api.Application.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Api.Application.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Api.Application.dll"]
