using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Banking.Operation.Transfer.Command.CrossCutting.Ioc.Modules;

namespace Banking.Operation.Transfer.Command.CrossCutting.Ioc
{
    public static class IoC
    {
        public static IServiceCollection ConfigureContainer(this IServiceCollection services, IConfiguration configuration)
        {
            DataModule.Register(services, configuration);
            services.Register();
            return services;
        }
    }
}
