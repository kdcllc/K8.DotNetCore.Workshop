# Kubernetes Installations


## [Configuring probes for Kubernetes using health checks](https://github.com/dotnet-architecture/eShopOnContainers/wiki/Using-HealthChecks#configuring-probes-for-kubernetes-using-health-checks)
Helm charts already configure the needed probes in Kubernetes using the healthchecks, but you can override the configuration provided by editing the file `/k8s/<chart-folder>/values.yaml`. You'll see a code like that:

```yaml
probes:
  liveness:
    path: /liveness
    initialDelaySeconds: 10
    periodSeconds: 15
    port: 80
  readiness:
    path: /hc
    timeoutSeconds: 5
    initialDelaySeconds: 90
    periodSeconds: 60
    port: 80

```

You can remove a probe if you want or update its configuration. Default configuration is the same for all charts:

- 10 seconds before k8s starts to test the liveness probe
- 1 sec of timeout for liveness probe (not configurable)
- 15 sec between liveness probes calls
- 90 seconds before k8s starts to test the readiness probe
- 5 sec of timeout for readiness probe
- 60 sec between readiness probes calls