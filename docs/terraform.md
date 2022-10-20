# Terraform

[Terraform Download](https://www.terraform.io/downloads.html)
[tflint](https://github.com/terraform-linters/tflint/releases)

```bash
    terraform init

    terraform plan -out myplan.tfplan

    terraform plan -var my_custom_variable -out myplan.tfplan

    terraform apply "myplan.tfplan" (or terraform apply)

    terraform destroy -force

    terraform graph

    # init backend configuration thru command line (partial config)
    terraform init -backend-config="sas_token=oiubcghfgfyg"
    terraform init -backend-config=backend-config.txt

    terraform workspace new development
```

For variable that are an environment variable

export TF_VAR_my_value=test

```terraform
    resource "random_integer" "rad" {
        min = 100000
        max = 999999
    }
```

## References

- [Getting Started Terraform](https://github.com/ned1313/Getting-Started-Terraform)
- [Implementing Terraform on Microsoft Azure](https://github.com/ned1313/Implementing-Terraform-on-Microsoft-Azure)