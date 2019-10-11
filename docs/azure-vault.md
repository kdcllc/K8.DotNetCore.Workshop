# Azure Vault Local Development in the Containers

1. Creating Azure Vault

```bash
    # create Azure vault
    export azureVaultName=k8demovault
    az keyvault create --location centralus --name $azureVaultName --resource-group k8-demo

    # list all of the Azure Vaults
    az keyvault list --output table

    # adds secret
    az keyvault secret set --vault-name "$azureVaultName" --name "AzCronJobKey" --value "CronJob Value for the Vault"

    # shows secret
    az keyvault secret show --name AzCronJobKey --vault-name $azureVaultName --output table

```

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

4. Restart Visual Studio Code in order for the Environment Variables to load in the terminal window.

[Test AppAuthentication CLI tool generation of the Token for Azure Storage](http://localhost:5050/oauth2/token?resource=https://storage.azure.com)

## Library

```bash
    dotnet add package Microsoft.Azure.Services.AppAuthentication
```