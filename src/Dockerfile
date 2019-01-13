FROM microsoft/dotnet:2.1-sdk
WORKDIR /app
EXPOSE 7890

# copy csproj and restore as distinct layers
COPY /. .
RUN dotnet restore

# copy and build everything else
COPY . ./
RUN dotnet publish -c Release -o out
ENTRYPOINT ["dotnet", "Websocket.Relay.Console/out/Websocket.Relay.Console.dll"]