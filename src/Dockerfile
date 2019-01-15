FROM microsoft/dotnet:2.1-sdk
WORKDIR /app
EXPOSE 7890

# copy csproj and restore as distinct layers
COPY /. .
RUN dotnet restore

# copy and build everything else
COPY . ./
RUN dotnet publish -c Release -o out

FROM microsoft/dotnet:2.1 AS runtime
WORKDIR /app
ENTRYPOINT ["dotnet", "websocket.relay.console/out/Websocket.Relay.Console.dll"]