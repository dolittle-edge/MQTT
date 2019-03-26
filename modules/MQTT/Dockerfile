FROM microsoft/dotnet:2.1-sdk AS build-env
WORKDIR /app

COPY Build/MSBuild/* ./Build/MSBuild/
COPY modules/MQTT/*.csproj ./modules/MQTT/

WORKDIR /app/modules/MQTT

RUN dotnet restore

COPY modules/MQTT/ ./
RUN dotnet publish -c Release -o out

FROM microsoft/dotnet:2.1-runtime-stretch-slim
WORKDIR /app
COPY --from=build-env /app/modules/MQTT/out ./

#RUN useradd -ms /bin/bash moduleuser
#USER moduleuser

ENTRYPOINT ["dotnet", "Dolittle.Edge.MQTT.dll"]