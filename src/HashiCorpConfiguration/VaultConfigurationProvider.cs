using HashiCorpConfiguration.Models;
using Microsoft.Extensions.Configuration;
using VaultSharp;
using VaultSharp.V1.AuthMethods;
using VaultSharp.V1.AuthMethods.Token;

namespace HashiCorpConfiguration;

public class VaultConfigurationProvider : ConfigurationProvider
{
    private readonly VaultConfigSettings _vaultConfigSettings;

    public VaultConfigurationProvider(VaultConfigSettings vaultConfigSettings)
    {
        _vaultConfigSettings = vaultConfigSettings;
    }

    public override async void Load()
    {
        Data = await GetDataFromVault();
    }

    private async Task<IDictionary<string, string>> GetDataFromVault()
    {
        try
        {
            IAuthMethodInfo authMethod = new TokenAuthMethodInfo(_vaultConfigSettings.Token);
            var vaultClientSettings = new VaultClientSettings(_vaultConfigSettings.ServerUriWithPort, authMethod);
            var vaultClient = new VaultClient(vaultClientSettings);
            var secrets = await vaultClient.V1.Secrets.KeyValue.V2.ReadSecretAsync(path: _vaultConfigSettings.Path,
                mountPoint: _vaultConfigSettings.MountPoint);

            return secrets.Data.Data.ToDictionary(k => k.Key, v => v.Value.ToString());
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}