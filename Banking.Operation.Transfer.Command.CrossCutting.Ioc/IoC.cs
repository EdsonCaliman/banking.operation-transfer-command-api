using Banking.Operation.Transfer.Command.CrossCutting.Ioc.Modules;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace Banking.Operation.Transfer.Command.CrossCutting.Ioc
{
    [ExcludeFromCodeCoverage]
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
