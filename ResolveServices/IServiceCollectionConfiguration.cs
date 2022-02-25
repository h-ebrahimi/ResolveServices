using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
namespace ResolveServices
{
    public interface IServiceCollectionConfiguration
    {
        void Configure(IServiceCollection services, IConfiguration configuration);
    }
}
