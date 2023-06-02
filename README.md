# HashiCorpAsConfiguration
HashiCorpAsConfiguration is a Custom [configuration provider](https://docs.microsoft.com/en-us/dotnet/core/extensions/configuration#configuration-providers) for asp.net projects. It adds your hashcorp vault secrets to dotnet's [Configuration](https://docs.microsoft.com/en-us/dotnet/core/extensions/configuration) and can be used in your projects. It is built on top of vault sharp.

## Intallation 
PMC:

    Install-Package Ziemcd.Configuration.HashiCorpAsConfiguration
.NET CLI:

    dotnet add package Ziemcd.Configuration.HashiCorpAsConfiguration

## Usage
Setting up Vault configuration is a pretty easy set up. 
1. All you need to do is add it in program.cs like so.

       builder.Configuration.AddVaultConfiguration(new VaultConfigSettings
       {
          //Set up your vault settings here
          Token = "Your Token",
          ServerUriWithPort = "Your port",
          //MountPoint can be null
          MountPoint = "Your mount point",
          Path = "Path to your secrets"
       });
2. Now that Vault is configured all your secrets will be part of Configuration and can be injected else where in your project.

       private readonly IConfiguration _config;

       public ConfigurationDemoController(IConfiguration config)
       {
           _config = config;
       }
3. Now that IConfiguration is injected you can get your secrets.

       public string GetSecret(string secret)
       {
           return _config["A secret you have in vault"];
       }
