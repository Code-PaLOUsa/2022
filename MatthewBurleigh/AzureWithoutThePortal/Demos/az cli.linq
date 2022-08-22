<Query Kind="Statements" />

string subscriptionId = "<your Azure subscription id>";


Util.Cmd($"az group list --subscription {subscriptionId} --query [].name -o tsv", true).Dump("all resource groups");
Util.Cmd($"az group list --subscription {subscriptionId} --query \"[?contains(name,'dev')].name\" -o tsv", true).Dump("dev resource groups");
































