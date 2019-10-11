# Helm Simple template Lab

1. Add `helm.ex` executable from the VSCode exention location
[Environment]::SetEnvironmentVariable("PATH", [Environment]::GetEnvironmentVariable("PATH", "User") + ";" + $HOME +"/.vs-kubernetes/tools/helm/windows-amd64", "User")

2. Reload Visual Studio Code for the `Helm.exe` to be loaded in the powershell

3. Initialize Helm
This will create instance of latest tiller on local K8 Cluster

```bash
    helm init
```

4. Install Helm Package

```bash
    helm install k8s/k8netcorev1 -n dev-k8demo
```

5. Delete Helm Package

```bash
    # delete the install package
    helm delete dev-k8demo --purge

    # remove all of the jobs history
    kubectl delete jobs --all --cascade=false

    # delete pods
    kubectl delete pods --all --cascade=false
```

## Helm commands
```bash

    helm install [name of the chart]

    helm list --short

    helm status [release name]
```

[Helm Templating Engine Reference](./docs/helm-reference.md)