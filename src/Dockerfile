FROM arm32v7/debian:stretch-slim 
COPY qemu-arm-static /usr/bin

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /app
EXPOSE 7890

# copy csproj and restore as distinct layers
COPY Websocket.Relay.Console/*.csproj ./Websocket.Relay.Console/
COPY Websocket.Relay/*.csproj ./Websocket.Relay/
WORKDIR /app/Websocket.Relay.Console/
RUN dotnet restore

# copy and build everything else
WORKDIR /app/
COPY Websocket.Relay.Console/. ./Websocket.Relay.Console/
COPY Websocket.Relay/. ./Websocket.Relay/
WORKDIR /app/Websocket.Relay.Console/
RUN dotnet publish -c Release -o out

FROM microsoft/dotnet:2.2-runtime AS runtime
WORKDIR /app
COPY --from=build /app/Websocket.Relay.Console/out ./
ENTRYPOINT ["dotnet", "Websocket.Relay.Console.dll"]