# BUILD: docker build -t com.io/k8.demo.cronjobs:v1 -f "./src/K8.LongProcess/Dockerfile" --build-arg PAT --build-arg FEED .

FROM com.io/k8.demo.base:v1 AS build
WORKDIR /app

RUN echo $PAT \
    && echo $FEED

WORKDIR /app/src/K8.LongProcess
RUN dotnet publish -c Release -r linux-musl-x64 -o out --self-contained true /p:PublishTrimmed=true

FROM mcr.microsoft.com/dotnet/core/runtime-deps:3.0-alpine AS runtime
WORKDIR /app

# uses the default port
ENV MSI_ENDPOINT=http://host.docker.internal:5050/oauth2/token
ENV MSI_SECRET=19603b25-f198-4156-a82a-b68d40681455

COPY --from=build /app/src/K8.LongProcess/out ./
ENTRYPOINT ["./K8.LongProcess"]
