# Helm Templated Lab

1. Install Version 2 of Helm templated package

```bash
    helm install k8s/k8netcorev2 -n dev-k8demo
```

2. Delete Helm Package

```bash
    # delete the install package
    helm delete dev-k8demo --purge

    # remove all of the jobs history
    kubectl delete jobs --all --cascade=false

    # delete pods
    kubectl delete pods --all --cascade=false
```

[Helm Templating Engine Reference](./docs/helm-reference.md)