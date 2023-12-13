FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["PictureApi.csproj", "./"]
RUN dotnet restore "PictureApi.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "PictureApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PictureApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Copy the fonts folder
COPY Fonts/ ./fonts/

ENTRYPOINT ["dotnet", "PictureApi.dll"]
