FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

#FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
# below is temporary FROM to mitigate .NET images problem with certificates (https://github.com/NuGet/Announcements/issues/49)
FROM mcr.microsoft.com/dotnet/sdk:5.0.102-ca-patch-buster-slim AS build 
WORKDIR /src
RUN dir
COPY ./RecruitmentTaskApi.Api/RecruitmentTaskApi.Api.csproj RecruitmentTaskApi.Api/
RUN dir
RUN dotnet restore RecruitmentTaskApi.Api/RecruitmentTaskApi.Api.csproj
COPY . .
WORKDIR /src/RecruitmentTaskApi.Api
RUN dotnet build RecruitmentTaskApi.Api.csproj -c Debug -o /app

FROM build AS publish
RUN dotnet publish RecruitmentTaskApi.Api.csproj -c Debug -o /app


FROM base AS final
WORKDIR /app
COPY --from=publish /app .

ENTRYPOINT ["dotnet", "RecruitmentTaskApi.Api.dll"]
