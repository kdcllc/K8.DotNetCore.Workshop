#------------------------------------------------------------------------------------------------------------------------
# BUILD:        docker build -t com.io/k8.demo.frontend:v1 -f "./src/K8.FrontEnd/Dockerfile" --build-arg PAT --build-arg FEED .
#
# RUN:          docker run -p 5000:5000  --rm -d com.io/k8.demo.frontend:v1 --name k8.frontend
#
# CHECK:        wget -qO- http://localhost:5000/WeatherForecast
#------------------------------------------------------------------------------------------------------------------------

FROM com.io/k8.demo.base:v1 AS build
WORKDIR /app

RUN echo $PAT \
    && echo $FEED

WORKDIR /app/src/K8.FrontEnd
RUN dotnet publish -c Release -r linux-musl-x64 -o out --self-contained true /p:PublishTrimmed=true

FROM mcr.microsoft.com/dotnet/core/runtime-deps:3.0-alpine AS runtime

ENV ASPNETCORE_URLS=http://+:5000
ENV ASPNETCORE_ENVIRONMENT=Development

# uses the default port
ENV MSI_ENDPOINT=http://host.docker.internal:5050/oauth2/token
ENV MSI_SECRET=19603b25-f198-4156-a82a-b68d40681455

EXPOSE 5000

WORKDIR /app
COPY --from=build /app/src/K8.FrontEnd/out ./
ENTRYPOINT ["./K8.FrontEnd"]
