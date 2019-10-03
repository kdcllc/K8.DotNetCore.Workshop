#------------------------------------------------------------------------------------------------------------------------
# BUILD:    docker build -t k8.demo.frontend:v1 -f "./src/K8.FrontEnd/Dockerfile" --build-arg PAT --build-arg FEED .
# RUN:      docker run -p 5000:5000  --rm -d k8.demo.frontend:v1 --name k8.frontend
# CHECK:    wget -qO- http://localhost:5000/WeatherForecast
#------------------------------------------------------------------------------------------------------------------------

FROM k8.demo.base:v1 AS build
WORKDIR /app

RUN echo $PAT \
    && echo $FEED

WORKDIR /app/src/K8.FrontEnd
RUN dotnet publish -c Release -r linux-musl-x64 -o out --self-contained true /p:PublishTrimmed=true

FROM mcr.microsoft.com/dotnet/core/runtime-deps:3.0-alpine AS runtime

ENV ASPNETCORE_URLS=http://+:5000
ENV ASPNETCORE_ENVIRONMENT=Development

EXPOSE 5000

WORKDIR /app
COPY --from=build /app/src/K8.FrontEnd/out ./
ENTRYPOINT ["./K8.FrontEnd"]
