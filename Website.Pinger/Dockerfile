#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM alpine AS base
RUN apk add aspnetcore8-runtime
RUN apk add tzdata --no-cache
WORKDIR /app
EXPOSE 4000

FROM alpine AS build
RUN apk add dotnet8-sdk
WORKDIR /src
COPY ["Website.Pinger/Website.Pinger.csproj", "Website.Pinger/"]
RUN dotnet restore "Website.Pinger/Website.Pinger.csproj"
COPY . .
WORKDIR "/src/Website.Pinger"
RUN dotnet build "Website.Pinger.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Website.Pinger.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Website.Pinger.dll"]