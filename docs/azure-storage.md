# Azure Storage

```bash

    # 1. logout
    az logout

    # 2. login
    az login

    # 3. lists all of the subscriptions for the login
    az account list --output table

    # 4. set the subscription the azure storage is to be created
    az account set -s [name or id]

    # 5. lists all of the storage accounts
    az storage account list --output table

    # 6. create azure storage
    export storageName=k8demostorage

    az storage account create --location "Central US" --name $storageName --resource-group k8-demo --sku Standard_LRS

    # 7. lists all of the storage accounts again.
    az storage account list --output table

    # 8. retrieve account keys
    az storage account keys list --account-name $storageName --output table

    # 9. retrieve account connection string (this connection string can be used for K8.FrontEnd appsettings.json)
    az storage account show-connection-string --name $storageName

    # 10. Add connection string to azure vault
    export connectionString="$(az storage account show-connection-string --name $storageName --output tsv)"
    export azureVaultName=k8demovault
    az keyvault secret set --vault-name $azureVaultName --name "AzureStorageConnection" --value $connectionString

    az keyvault secret set --vault-name $azureVaultName --name "AzureStorageName" --value $storageName
```

Access Control (IAM)

Storage Blob Data Contributor
Key Vault Contributor
