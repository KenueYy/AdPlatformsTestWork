FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 777
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore "AdPlatformsTestWork/AdPlatformsTestWork.csproj"
COPY .. /src/AdPlatformsTestWork

RUN dotnet build "AdPlatformsTestWork/AdPlatformsTestWork.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AdPlatformsTestWork/AdPlatformsTestWork.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AdPlatformsTestWork.dll", "--host=0.0.0.0", "--port=777"]
