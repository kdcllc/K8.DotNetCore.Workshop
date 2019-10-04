# Helm commands reference

## Helm Templates

1. Static

```bash
    helm template
    helm template -x templates/service.yaml
    #workks without tiller and static release-name
```

2. Dynamic

```bash
    helm install --dry-run --debug
    #real helm install but without commit generated release-name
```

## Helm Template Values

### Values are supplied thru the following
1. values.yaml file
2. helm install -f other-values-file.yaml
3. helm install --set foo=bar

1. Chart data: `{{.Chart.Name}}` -> chart.yaml (name: mychart)
2. Release data: `{{.Release.Name}}` -> Release (Release.Name)
3. Kubernetes data: `{{.Capabilities.KubeVersion}} -> Kubernetes & Tiller
4. File data: `{{.Files.Get conf.ini}}` -> conf.ini must be located in the root of the directory
5. Template data: `{{.Template.Name}}` -> Template.Name or Template.BasePath
