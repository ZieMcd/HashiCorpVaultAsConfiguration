using Microsoft.AspNetCore.Mvc;

namespace HashiCorpConfigurationExampleApp.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class ConfigurationDemoController : ControllerBase
{
    private readonly IConfiguration _config;
    
    public ConfigurationDemoController(IConfiguration config)
    {
        _config = config;
    }

    //Gets a list of the configuration provider your configuration is using
    [HttpGet]
    public List<string?> ConfigProvider()
    {
        var  configRoot = (IConfigurationRoot)_config;
        return configRoot.Providers.Select(x => x.ToString()).ToList();

    }
    
    [HttpGet]
    public string GetSecret(string secret)
    {
        return _config["secret-from-vault"];
    }
}