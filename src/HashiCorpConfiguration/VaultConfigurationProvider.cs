using HashiCorpConfiguration.Models;
using Microsoft.Extensions.Configuration;
using VaultSharp;
using VaultSharp.V1.AuthMethods;
using VaultSharp.V1.AuthMethods.Token;
using VaultSharp.V1.Commons;

namespace HashiCorpConfiguration;

public class VaultConfigurationProvider : ConfigurationProvider
{
    private readonly VaultConfigSettings _vaultConfigSettings;

    public VaultConfigurationProvider(VaultConfigSettings vaultConfigSettings)
    {
        _vaultConfigSettings = vaultConfigSettings;
    }

    public override void Load()
    {
        Data =  GetDataFromVault();
    }

    private  IDictionary<string, string> GetDataFromVault()
    {
        try
        {
            Secret<SecretData>? secrets;
            if (_vaultConfigSettings.MountPoint != null)
            {
                IAuthMethodInfo authMethod = new TokenAuthMethodInfo(_vaultConfigSettings.Token);
                var vaultClientSettings = new VaultClientSettings(_vaultConfigSettings.ServerUriWithPort, authMethod);
                var vaultClient = new VaultClient(vaultClientSettings);
                secrets  = vaultClient.V1.Secrets.KeyValue.V2.ReadSecretAsync(path: _vaultConfigSettings.Path,
                    mountPoint: _vaultConfigSettings.MountPoint).Result;
            }
            else
            {
                IAuthMethodInfo authMethod = new TokenAuthMethodInfo(_vaultConfigSettings.Token);
                var vaultClientSettings = new VaultClientSettings(_vaultConfigSettings.ServerUriWithPort, authMethod);
                var vaultClient = new VaultClient(vaultClientSettings);
                secrets = vaultClient.V1.Secrets.KeyValue.V2.ReadSecretAsync(path: _vaultConfigSettings.Path).Result;
            }
            

            return secrets.Data.Data.ToDictionary(k => k.Key, v => v.Value.ToString());
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}