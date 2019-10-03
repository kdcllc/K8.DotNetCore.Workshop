# BUILD: docker build -t k8.demo.cronjobs:v1 -f "./src/K8.LongProcess/Dockerfile" --build-arg PAT --build-arg FEED .

FROM k8.demo.base:v1 AS build
WORKDIR /app

RUN echo $PAT \
    && echo $FEED

WORKDIR /app/src/K8.LongProcess
RUN dotnet publish -c Release -r linux-musl-x64 -o out --self-contained true /p:PublishTrimmed=true

FROM mcr.microsoft.com/dotnet/core/runtime-deps:3.0-alpine AS runtime
WORKDIR /app
COPY --from=build /app/src/K8.LongProcess/out ./
ENTRYPOINT ["./K8.LongProcess"]
