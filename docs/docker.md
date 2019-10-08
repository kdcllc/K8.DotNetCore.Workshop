# Docker-compose commands Lab

## Building Docker images locally

### Build images with `Docker-compose`

```bash
    # Step 1 - build images
    docker-compose -f "docker-compose.yml" up --build --no-start

    # Step 2 - clean up
    # cleans local containers that are not being used
    docker container prune -f
```

### Build and create instances of the Containers on the local machine with `Docker-compose` (Optional)

```bash

    # builds but doesn't recreates the containers
    docker-compose -f "docker-compose.yml" up -d  --no-recreate

    # builds and creates a containers
    docker-compose -f "docker-compose.yml" up -d --force-recreate

    docker-compose -f "docker-compose.yml" down
```

### Building using `Docker` Cli (Optional)

All of the images can be build manually by running the `docker` command from the root of the solution.

1. `docker build -t com.io/k8.demo.base:v1 -f "Dockerfile.base" . `

2. `docker build -t com.io/k8.demo.cronjobs:v1 -f "./src/K8.LongProcess/Dockerfile" --build-arg PAT --build-arg FEED .`

3. `docker build -t com.io/k8.demo.frontend:v1 -f "./src/K8.FrontEnd/Dockerfile" --build-arg PAT --build-arg FEED .`


## Docker Commands

- Clean up system

```bash
    # clean up system
    docker system prune -f
    docker container prune -f

    # list all images
    docker images -f dangling=true
    # clean up
    docker rmi -f $(docker images -f "dangling=true" -q)
```