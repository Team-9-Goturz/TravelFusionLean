# 🔧 Base image til at køre appen (.NET 9 runtime)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80

# 🛠 Image til at bygge appen (.NET 9 SDK)
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# 📁 Kopiér alle filer ind i containerens build mappe
COPY . .

# ⚙️ Byg dit TravelFusionLean-projekt
RUN dotnet publish "TravelFusionLean/TravelFusionLean.csproj" -c Release -o /app/publish

# 📦 Final runtime image (lean!)
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .

# ▶️ Start appen
ENTRYPOINT ["dotnet", "TravelFusionLean.dll"]
