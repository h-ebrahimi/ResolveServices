namespace ResolveServices
{
    public interface IServiceCollectionConfiguration
    {
        void Configure(IServiceCollection services, IConfiguration configuration);
    }
}
