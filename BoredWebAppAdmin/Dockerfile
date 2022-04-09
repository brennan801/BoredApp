#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443



FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["BoredWebAppAdmin/BoredWebAppAdmin.csproj", "BoredWebAppAdmin/"]
COPY ["BoredWebApp/BoredWebApp.csproj", "BoredWebApp/"]
RUN dotnet restore "BoredWebAppAdmin/BoredWebAppAdmin.csproj"
COPY . .
WORKDIR "/src/BoredWebAppAdmin"
RUN dotnet build "BoredWebAppAdmin.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BoredWebAppAdmin.csproj" -c Release -o /app/publish

# Configure web servers to bind to port 80 when present
ENV ASPNETCORE_URLS=http://+:80 \
    # Enable detection of running in a container
    DOTNET_RUNNING_IN_CONTAINER=true \
    # Set the invariant mode since icu_libs isn't included (see https://github.com/dotnet/announcements/issues/20)
    DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=true

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BoredWebAppAdmin.dll"]