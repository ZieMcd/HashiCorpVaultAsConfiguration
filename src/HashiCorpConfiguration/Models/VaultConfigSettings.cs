namespace HashiCorpConfiguration.Models;

public class VaultConfigSettings
{
    public string Token { get; set; }
    public string ServerUriWithPort { get; set; }
    public string?  MountPoint { get; set; }
    public string Path { get; set; }
}