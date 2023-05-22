FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Lative.Presentation/Lative.Presentation.csproj", "Lative.Presentation/"]
RUN dotnet restore "Lative.Presentation/Lative.Presentation.csproj"
COPY . .
WORKDIR "/src/Lative.Presentation"
RUN dotnet build "Lative.Presentation.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Lative.Presentation.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Lative.Presentation.dll"]
