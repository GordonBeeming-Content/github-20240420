FROM mcr.microsoft.com/dotnet/aspnet:9.0-preview-alpine AS base
RUN apk add icu-libs
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false
USER $APP_UID
WORKDIR /app
EXPOSE 80
ENV ASPNETCORE_URLS=http://+:80
ARG A_COMMIT_HASH
ENV COMMIT_HASH=$A_COMMIT_HASH
ARG A_BRANCH_NAME
ENV BRANCH_NAME=$A_BRANCH_NAME

FROM mcr.microsoft.com/dotnet/sdk:9.0-preview-alpine AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["global.json", "global.json"]
COPY ["Directory.Build.props", "Directory.Build.props"]
COPY ["Directory.Packages.props", "Directory.Packages.props"]
COPY ["Solution1/src/SampleApp.Web/SampleApp.Web.csproj", "src/SampleApp.Web/"]
COPY ["Solution1/src/SampleApp.Database/SampleApp.Database.csproj", "src/SampleApp.Database/"]
RUN dotnet restore "src/SampleApp.Web/SampleApp.Web.csproj"
COPY Solution1 /src
WORKDIR "/src/src/SampleApp.Web"
RUN dotnet build "SampleApp.Web.csproj" -c $BUILD_CONFIGURATION --no-restore

# FROM build AS publish
# ARG BUILD_CONFIGURATION=Release
# RUN dotnet publish "SampleApp.Web.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false --no-restore --no-build

# FROM base AS final
# WORKDIR /app
# COPY --from=publish /app/publish .
# ENTRYPOINT ["dotnet", "SampleApp.Web.dll"]
