# Kubernetes Demos for AspNetCore 3.0

The purpose of this repo is to demonstrate AspNetCore 3.0 functionality within Kubernetes.

- ApsNetCore 3.0 Azure Key Vault
- DotNetCore 3.0 Worker Hosted in the Cluster and ran as CronJob

- Docker Containers
- Kubernetes Cluster
- Helm deployment

## Running locally

1. Build Docker images

```bash
    docker-compose -f "docker-compose.yml" up -d --force-recreate

    docker-compose -f "docker-compose.yml" down
```

## References

- [LIVENESS PROBES ARE DANGEROUS](https://srcco.de/posts/kubernetes-liveness-probes-are-dangerous.html)
- [Getting external traffic into Kubernetes – ClusterIp, NodePort, LoadBalancer, and Ingress](https://www.ovh.com/blog/getting-external-traffic-into-kubernetes-clusterip-nodeport-loadbalancer-and-ingress/)