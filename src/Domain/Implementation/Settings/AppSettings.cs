using Microsoft.Extensions.Configuration;
using SmartCharging.Domain.Contract.Settings;

namespace SmartCharging.Domain.Implementation.Settings;

public class AppSettings : IAppSettings
{
    private readonly IConfiguration _configuration;
    
    public AppSettings(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public int MaxConnectorCountOfChargeStation => int.TryParse(_configuration["MaxConnectorCountOfChargeStation"], out var intValue) ? intValue : 5;
}
