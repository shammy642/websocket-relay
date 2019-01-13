FROM microsoft/dotnet:2.1-sdk-stretch-arm32v7
WORKDIR /app
EXPOSE 7890

# copy csproj and restore as distinct layers
COPY /. .
RUN dotnet restore

# copy and build everything else
COPY . ./
RUN dotnet publish -c Release -o out

FROM microsoft/dotnet:2.1-runtime-stretch-slim-arm32v7 AS runtime
WORKDIR /app
ENTRYPOINT ["dotnet", "Websocket.Relay.Console/out/Websocket.Relay.Console.dll"]