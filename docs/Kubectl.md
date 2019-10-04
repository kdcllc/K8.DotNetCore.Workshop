# Kubectl Commands

## [Kubectl Context and Configuration](https://kubernetes.io/docs/reference/kubectl/cheatsheet/#kubectl-context-and-configuration)

To setup configuration

```bash
    # displays all available contexts and configurations
    kubectl config get-contexts

    # display the name of the current context
    kubectl config current-context

    # switch to docker k8 local engine
    kubectl config use-context docker-for-desktop

    # display all of the existing namespaces
    kubectl get namespaces

    # if dev namespace doesn't exist create it
    kubectl create namespace dev

    # set default namespace to dev
    kubectl config set-context --current --namespace=dev
```


- Delete history of CronJobs

```bash
    kubectl delete jobs $(kubectl get jobs -o custom-columns=:.metadata.name)

    # or
    kubectl delete jobs --all --cascade=false
```