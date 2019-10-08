# Kubectl Commands Lab

## [Kubectl Context and Configuration](https://kubernetes.io/docs/reference/kubectl/cheatsheet/#kubectl-context-and-configuration)

- To setup K8 configuration Steps 1-6

```bash
    # Step 1: displays all available contexts and configurations
    kubectl config get-contexts

    # Step 2: display the name of the current context
    kubectl config current-context

    # Step 3: switch to docker k8 local engine
    kubectl config use-context docker-for-desktop

    # Step 4: display all of the existing namespaces
    kubectl get namespaces

    # Step 5: if dev namespace doesn't exist create it
    kubectl create namespace dev

    # Step 6: set default namespace to dev
    kubectl config set-context --current --namespace=dev
```

- Step 7 - Create FrontEnd

```bash
    kubectl create -f k8s/k8netcorev1/charts/frontend/templates/frontend-service.yaml -n dev
    kubectl create -f k8s/k8netcorev1/charts/frontend/templates/frontend.yaml -n dev
```
- Step 8 Create CronJobs

```bash
    kubectl create -f k8s/k8netcorev1/charts/cronjobs/templates/cronjob-convert.yaml -n dev
    kubectl create -f k8s/k8netcorev1/charts/cronjobs/templates/cronjob-import.yaml -n dev
```

- Step 9 Clean up

```bash
    # delete frontend
    kubectl delete -f k8s/k8netcorev1/charts/frontend/templates/frontend-service.yaml -n dev
    kubectl delete -f k8s/k8netcorev1/charts/frontend/templates/frontend.yaml -n dev

    # delete cronjobs
    kubectl delete -f k8s/k8netcorev1/charts/cronjobs/templates/cronjob-convert.yaml -n dev
    kubectl delete -f k8s/k8netcorev1/charts/cronjobs/templates/cronjob-import.yaml -n dev

    # delete jobs history
    kubectl delete jobs --all --cascade=false
```

