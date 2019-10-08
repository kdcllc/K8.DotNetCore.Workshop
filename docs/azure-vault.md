# Azure Vault Local Development in the Containers

1. Install Authentication tool

```bash
    dotnet tool install --global appauthentication
```

2. Add the following to the `docker-compse.yml`

```yaml
    environment:
      - MSI_ENDPOINT=${MSI_ENDPOINT}
      - MSI_SECRET=${MSI_SECRET}
```

make sure the existing docker images are removed before rebuilding it.

3. Run the tool

```bash
    appauthentication run --verbose:debug
```
