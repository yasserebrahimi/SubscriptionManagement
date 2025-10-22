terraform {
  required_version = ">= 1.5.0"
  required_providers {
    kubernetes = {
      source  = "hashicorp/kubernetes"
      version = "~> 2.27"
    }
  }
}

variable "kubeconfig_path" { type = string }
variable "kube_context"     { type = string }
variable "namespace"        { type = string  default = "subscription" }

provider "kubernetes" {
  config_path    = var.kubeconfig_path
  config_context = var.kube_context
}

resource "kubernetes_namespace" "ns" {
  metadata { name = var.namespace }
}
