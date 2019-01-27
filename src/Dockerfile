FROM microsoft/dotnet:2.1-sdk-stretch-arm32v7 AS build
WORKDIR /app
EXPOSE 7890

FROM arm32v7/debian:stretch-slim 
COPY qemu-arm-static /usr/bin

# copy csproj and restore as distinct layers
COPY Websocket.Relay.Console/*.csproj ./Websocket.Relay.Console/
COPY Websocket.Relay/*.csproj ./Websocket.Relay/
WORKDIR /app/Websocket.Relay.Console
RUN dotnet restore

# copy and build everything else
WORKDIR /app/
COPY Websocket.Relay.Console/. ./Websocket.Relay.Console/
COPY Websocket.Relay/. ./Websocket.Relay/
WORKDIR /app/Websocket.Relay.Console/
# add IL Linker package
RUN dotnet add package ILLink.Tasks -v 0.1.5-preview-1841731 -s https://dotnet.myget.org/F/dotnet-core/api/v3/index.json
RUN dotnet publish -c Release -r linux-arm -o out /p:ShowLinkerSizeComparison=true

FROM microsoft/dotnet:2.2-runtime-deps-stretch-slim-arm32v7 AS runtime
WORKDIR /app
COPY --from=build /app/Websocket.Relay.Console/out ./
ENTRYPOINT ["dotnet", "Websocket.Relay.Console.dll"]
