# Kubernetes DotNetCore Workshop

This workshop will provide with ability to learn the basics of the Kubernetes development from DotNetCore developer perspective.

The solution includes the following projects:

- `K8.AspNetCore.HealthChecks.csproj` - a simple project that demonstrates how to add custom HealthChecks.
- `K8.Extensions.Configuration.csproj` - a project that creates a custom Azure Key Vault Secrets retrieval.
- `K8.Extensions.Hosting.csproj` - a project that demonstrates how to extend hosting creation for the common K8s projects.
- `K8.FrontEnd.csproj` - the `AspNetCore` Web Api project that demonstrates usage of health and liveliness checks.
- `K8.LongProcess.csproj` - the DotNetCore 3.0 Worker Hosted Console App that runs custom K8s CronJobs.

## Technologies

- Docker Containers
- Kubernetes Cluster
- Helm deployment

## Pre-requisites

1. Docker and Docker Kubernetes Local Cluster
![Windows 10 Kubernetes local cluster](./docs/img/win10-docker-k8s-local-cluster.jpg)

2. Visual Studio Code

3. Install alternative K8 dashboard

```bash
    kubectl apply -f https://raw.githubusercontent.com/kubernetes/dashboard/v1.10.1/src/deploy/alternative/kubernetes-dashboard.yaml

    # launch the dashboard
    kubectl proxy
```

[Open K8 Cluster Local Dashboard](http://localhost:8001/api/v1/namespaces/kube-system/services/http:kubernetes-dashboard:/proxy/#!/overview?namespace=default)

4. Update Windows 10 Hosts file update to include `C:\Windows\System32\drivers\etc\hosts`

```txt
    127.0.0.1 kubernetes.docker.internal k8-frontend-app.local
```

[https://k8-frontend-app.local/weatherforecast](https://k8-frontend-app.local/weatherforecast)

5. [Install `Ngnix` Ingress Controller](https://kubernetes.github.io/ingress-nginx/deploy/#docker-for-mac)

```bash
    # installation
    kubectl apply -f https://raw.githubusercontent.com/kubernetes/ingress-nginx/master/deploy/static/mandatory.yaml
    kubectl apply -f https://raw.githubusercontent.com/kubernetes/ingress-nginx/master/deploy/static/provider/cloud-generic.yaml

    # validate installation
    kubectl get pods --all-namespaces -l app.kubernetes.io/name=ingress-nginx --watch
```

6. [Install `Local MSI Azure Local Authenticator`](./docs/azure-vault.md)

## Labs

1. [Lab: Build Docker Images](./docs/docker.md)
2. [Lab: `Kubectl` commands](./docs/kubectl.md)
3. [Lab:  Helm simple release](./docs/helm-simple.md)
4. [Lab:  Helm templated release](./docs/helm-templated.md)

## References

- [LIVENESS PROBES ARE DANGEROUS](https://srcco.de/posts/kubernetes-liveness-probes-are-dangerous.html)
- [Getting external traffic into Kubernetes – ClusterIp, NodePort, LoadBalancer, and Ingress](https://www.ovh.com/blog/getting-external-traffic-into-kubernetes-clusterip-nodeport-loadbalancer-and-ingress/)
- [Kubernetes Ingress scenario](https://codeburst.io/replicate-kubernetes-ingress-locally-with-docker-compose-2872e650af6b)