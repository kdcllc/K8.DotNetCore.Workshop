# Docker-compse commands

## Building Docker images locally

- Build images only

```bash

    docker-compose -f "docker-compose.yml" up --build --no-start

    # cleans local containers that are not being used
    docker container prune -f
```

- Builds and creates instance of the Containers

```bash

    # builds but doesn't recreates the containers
    docker-compose -f "docker-compose.yml" up -d  --no-recreate

    # builds and creates a containers
    docker-compose -f "docker-compose.yml" up -d --force-recreate

    docker-compose -f "docker-compose.yml" down
```
