FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["AdPlatformsTestWork/AdPlatformsTestWork.csproj", "AdPlatformsTestWork/"]
RUN dotnet restore "AdPlatformsTestWork/AdPlatformsTestWork.csproj"
COPY . .
WORKDIR "/src/AdPlatformsTestWork"
RUN dotnet build "AdPlatformsTestWork.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AdPlatformsTestWork.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AdPlatformsTestWork.dll"]
