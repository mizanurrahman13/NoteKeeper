using Microsoft.Extensions.Configuration;

namespace NOTEKEEPER.Api.Tests.Helpers;
public static class ConfigurationHelper
{
    public static IConfiguration BuildConfiguration(Dictionary<string, string> inMemorySettings)
    {
        return new ConfigurationBuilder()
            .AddInMemoryCollection(inMemorySettings)
            .Build();
    }
}
