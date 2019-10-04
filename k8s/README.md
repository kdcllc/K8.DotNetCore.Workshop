# K8 notes

- Creating a new Cluster namespace:

```bash
    kubectl create namespace dev
```


- Create FrontEnd

```bash
kubectl create -f frontend-service.yaml -n dev
kubectl create -f frontend.yaml -n dev
```

- Delete FrontEnd

```bash
kubectl delete -f frontend-service.yaml -n dev
kubectl delete -f frontend.yaml -n dev
```

- Create CronJob

```bash
    kubectl create -f cronjob-convert.yaml -n dev
```

## Helm commands

```bash

helm install [name of the chart]

helm list --short

helm status [release name] | less
```

### Install

```bash
    helm install  k8netcorev1 -n dev-k8demo

    helm install  k8netcorev2 -n dev-k8demo

```

### Delete helm chart deployment

```bash
    # delete specific deployment
    helm delete dev-k8demo --purge

```

## Local Docker and Kubernates Development on Windows 10

Install alternative K8 dashboard

```bash
kubectl apply -f https://raw.githubusercontent.com/kubernetes/dashboard/v1.10.1/src/deploy/alternative/kubernetes-dashboard.yaml
```

1. Launch

```bash
kubectl proxy
```

2. Open Dashboard

[Local Dashboard](http://localhost:8001/api/v1/namespaces/kube-system/services/http:kubernetes-dashboard:/proxy/#!/overview?namespace=default)

## Ngnix Ingress Controller

Enables nginx controller to be used as ingress.yaml within the services

```bash
kubectl apply -f https://raw.githubusercontent.com/kubernetes/ingress-nginx/nginx-0.24.1/deploy/mandatory.yaml
kubectl apply -f https://raw.githubusercontent.com/kubernetes/ingress-nginx/nginx-0.24.1/deploy/provider/cloud-generic.yaml
kubectl get svc --namespace=ingress-nginx
```

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

[http://k8-frontend-app.local/weatherforecast](http://k8-frontend-app.local/weatherforecast)

## Hosts file update to include

C:\Windows\System32\drivers\etc\hosts

```txt
    127.0.0.1 kubernetes.docker.internal k8-frontend-app.local
```
