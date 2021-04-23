# Get Base SDK Image from Microsoft, create work directory
FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build-env
WORKDIR /app

# Copy the CSPROJ file and restore any dependencies (via NUGET)
COPY ProStock.API/*.csproj ./ProStock.API/
COPY ProStock.Domain/*.csproj ./ProStock.Domain/
COPY ProStock.Repository/*.csproj ./ProStock.Repository/
WORKDIR /app/ProStock.API
RUN dotnet restore

WORKDIR /app

COPY . .

WORKDIR /app/ProStock.API

RUN dotnet publish --output /app/binaryfiles --configuration Release
RUN sed -n 's:.*<AssemblyName>\(.*\)</AssemblyName>.*:\1:p' *.csproj > /app/binaryfiles/__assemblyname
RUN if [ ! -s __assemblyname ]; then filename=$(ls *.csproj); echo ${filename%.*} > /app/binaryfiles/__assemblyname; fi

# Generate runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:2.2
WORKDIR /app
EXPOSE 80
COPY --from=build-env /app/binaryfiles .
ENTRYPOINT ["dotnet", "ProStock.API.dll"]
