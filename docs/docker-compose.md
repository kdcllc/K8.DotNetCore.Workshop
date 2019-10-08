# Docker-compose commands

## Building Docker images locally

### Building using `Docker` Cli

All of the images can be build manually by running the `docker` command from the root of the solution.

1. `docker build -t com.io/k8.demo.base:v1 -f "Dockerfile.base" . `

2. `docker build -t com.io/k8.demo.cronjobs:v1 -f "./src/K8.LongProcess/Dockerfile" --build-arg PAT --build-arg FEED .`

3. ``

- Build images only

```bash

    docker-compose -f "docker-compose.yml" up --build --no-start

    # cleans local containers that are not being used
    docker container prune -f
```

- Builds and creates instances of the Containers on the local machine

```bash

    # builds but doesn't recreates the containers
    docker-compose -f "docker-compose.yml" up -d  --no-recreate

    # builds and creates a containers
    docker-compose -f "docker-compose.yml" up -d --force-recreate

    docker-compose -f "docker-compose.yml" down
```
