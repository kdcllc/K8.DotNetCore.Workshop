#------------------------------------------------------------------------------------------------------------------------
# BUILD:        docker build -t com.io/k8.demo.cronjobs:v1 -f "./src/K8.LongProcess/Dockerfile" --build-arg PAT --build-arg FEED .
#
# RUN:          docker run --rm -d com.io/k8.demo.cronjobs:v1 -t Convert -d 10 -c 5
#
#               docker run --rm -d com.io/k8.demo.cronjobs:v1 -t Import -d 20 -c 10
#
# USAGE:        This is base image for the K8s CronJobs for DotNetCore Worker Console app.
#
# REQ:
#              - base image must be build first
#              - Enviroment variables $Env:PAT and $Env:FEED must be present
#------------------------------------------------------------------------------------------------------------------------

FROM com.io/k8.demo.base:v1 AS build
WORKDIR /app

WORKDIR /app/src/K8.LongProcess
RUN dotnet publish -c Release -r linux-musl-x64 -o out --self-contained true /p:PublishTrimmed=true

FROM mcr.microsoft.com/dotnet/core/runtime-deps:3.0-alpine AS runtime
WORKDIR /app

# Azure Key Vault Local Development Authentication
ENV MSI_ENDPOINT=http://host.docker.internal:5050/oauth2/token
ENV MSI_SECRET=19603b25-f198-4156-a82a-b68d40681455
# Azure Key Vault

COPY --from=build /app/src/K8.LongProcess/out ./
ENTRYPOINT ["./K8.LongProcess"]
