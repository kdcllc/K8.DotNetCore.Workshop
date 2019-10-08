# Kubernetes DotNetCore Workshop

This workshop will provide with ability to learn the basics of the Kubernetes development from DotNetCore developer perspective.

The solution includes the following projects:

- `K8.AspNetCore.HealthChecks.csproj` - a simple project that demonstrates how to add custom HealthChecks.
- `K8.Extensions.Configuration.csproj` - a project that creates a custom Azure Key Vault Secrets retrieval.
- `K8.Extensions.Hosting.csproj` - a project that demonstrates how to extend hosting creation for the common K8s projects.
- `K8.FrontEnd.csproj` - the `AspNetCore` Web Api project that demonstrates usage of health and liveliness checks.
- `K8.LongProcess.csproj` - the DotNetCore 3.0 Worker Hosted Console App that runs custom K8s CronJobs.

- Docker Containers
- Kubernetes Cluster
- Helm deployment

## Pre-requisites

1. Docker and Docker Kubernetes Local Cluster
2. Visual Studio Code


## Running locally

1. Build Docker images

```bash
    docker-compose -f "docker-compose.yml" up -d --force-recreate

    docker-compose -f "docker-compose.yml" down
```

## References

- [LIVENESS PROBES ARE DANGEROUS](https://srcco.de/posts/kubernetes-liveness-probes-are-dangerous.html)
- [Getting external traffic into Kubernetes – ClusterIp, NodePort, LoadBalancer, and Ingress](https://www.ovh.com/blog/getting-external-traffic-into-kubernetes-clusterip-nodeport-loadbalancer-and-ingress/)
- [Kubernetes Ingress scenario](https://codeburst.io/replicate-kubernetes-ingress-locally-with-docker-compose-2872e650af6b)