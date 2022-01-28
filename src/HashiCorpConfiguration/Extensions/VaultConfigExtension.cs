using HashiCorpConfiguration.Models;
using Microsoft.Extensions.Configuration;

namespace HashiCorpConfiguration.Extensions;

public static class VaultConfigExtension
{
    public static IConfigurationBuilder AddVaultConfiguration(this IConfigurationBuilder builder, VaultConfigSettings settings)
    {
        return builder.Add(new VaultConfigurationSource(settings));
    }
}