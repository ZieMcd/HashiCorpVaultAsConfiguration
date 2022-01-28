using HashiCorpConfiguration.Models;
using Microsoft.Extensions.Configuration;

namespace HashiCorpConfiguration;

public class VaultConfigurationSource : IConfigurationSource
{
    private readonly VaultConfigSettings _vaultConfigSettings;

    public VaultConfigurationSource(VaultConfigSettings vaultConfigSettings)
    {
        _vaultConfigSettings = vaultConfigSettings;
    }
    public IConfigurationProvider Build(IConfigurationBuilder builder)
    {
        return new VaultConfigurationProvider(_vaultConfigSettings);
    }
}