#------------------------------------------------------------------------------------------------------------------------
# BUILD:
#               docker build -t com.io/k8.demo.frontend:v1 -f "./src/K8.FrontEnd/Dockerfile" --build-arg PAT --build-arg FEED .
#
#               docker build -t com.io/k8.demo.frontend:v1 -f "./src/K8.FrontEnd/Dockerfile" --build-arg PAT="v" --build-arg FEED="f" .
#
#               docker build -t com.io/k8.demo.frontend:v1 -f "./src/K8.FrontEnd/Dockerfile"
#
# RUN:          docker run --env-file=.env -p 5000:5000  --rm -d com.io/k8.demo.frontend:v1 --name k8.frontend
#
# CHECK:        wget -qO- http://localhost:5000/WeatherForecast
#------------------------------------------------------------------------------------------------------------------------

FROM com.io/k8.demo.base:v1 AS build
WORKDIR /app


WORKDIR /app/src/K8.FrontEnd
RUN dotnet publish -c Release -r linux-musl-x64 -o out --self-contained true /p:PublishTrimmed=true

FROM mcr.microsoft.com/dotnet/core/runtime-deps:3.0-alpine AS runtime

EXPOSE 5000

WORKDIR /app
COPY --from=build /app/src/K8.FrontEnd/out ./
ENTRYPOINT ["./K8.FrontEnd"]
