# Kubectl Commands Lab

## [Kubectl Context and Configuration](https://kubernetes.io/docs/reference/kubectl/cheatsheet/#kubectl-context-and-configuration)

- To setup K8 configuration Steps 1-6

```bash
    # Step 1: displays all available contexts and configurations
    kubectl config get-contexts

    # Step 2: display the name of the current context
    kubectl config current-context
`
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
    # create front end service
    kubectl create -f k8s/k8netcorev1/charts/frontend/templates/frontend-service.yaml -n dev

    # create front end ingress
    kubectl create -f k8s/k8netcorev1/charts/frontend/templates/ingress.yaml -n dev

    # create front end deployment
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
    kubectl delete -f k8s/k8netcorev1/charts/frontend/templates/ingress.yaml -n dev
    kubectl delete -f k8s/k8netcorev1/charts/frontend/templates/frontend.yaml -n dev

    # delete cronjobs
    kubectl delete -f k8s/k8netcorev1/charts/cronjobs/templates/cronjob-convert.yaml -n dev
    kubectl delete -f k8s/k8netcorev1/charts/cronjobs/templates/cronjob-import.yaml -n dev

    # delete jobs history
    kubectl delete jobs --all --cascade=false
    # delete pods
    kubectl delete pods --all --cascade=false
```

## Other helpful commands

- Kubernetes Clsuter commands:

```bash

    # version
    kubectl version

    # display all of the nodes
    kubectl get nodes

    # locally only one node available
    kubectl describe node docker-desktop

    # cluster information
    kubectl cluster-info

    # get all infor about pods, deployments and services
    kubectl get all

    # switch namespaces
    kubectl config set-context docker-for-desktop --namespace dev

```

- Pods specific commands:

```bash

    # simple way to run a pod
    kubectl run [podname] --image=[image-name]

    # get pods
    kubectl get pods

    # get info on the pod
    kubectl describe pod [podname]

    # executes pod with bash
    kubectl exec [podname] -it sh

    # forward a port to allow external access
    kubectl port-forward [podname] [ports]

    # delete pod by deployement re-creates it
    kubectl delete pod [podname]
```

- Deployments specific commands:

```bash
    # create namespace
    kubectl create namespace [name]

    # create deployment/ validate is default value
    kubectl create -f file.yaml --dry-run --validate=true

    # delete the deployment by name
    kubectl delete deployment [deployname]

```


## Create Azure Container Repository (ACR)

```bash
    # https://kubernetes.io/docs/concepts/containers/images/#using-azure-container-registry-acr
    $acrServer={name}.azurecr.io
    $acrUserName=
    $acrPassword=
    $acrEmail=someemail@gmail.com

    kubectl create secret docker-registry <name> --docker-server=$acrServer --docker-username=$acrUserName --docker-password=$acrPassword --docker-email=$acrEmail
```