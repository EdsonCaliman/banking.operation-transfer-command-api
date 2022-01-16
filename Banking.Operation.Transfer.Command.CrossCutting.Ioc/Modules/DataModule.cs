using Banking.Operation.Transfer.Command.Domain.Transfer.Parameters;
using Banking.Operation.Transfer.Command.Domain.Transfer.Repositories;
using Banking.Operation.Transfer.Command.Infra.Data;
using Banking.Operation.Transfer.Command.Infra.Data.Transfer.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Banking.Operation.Transfer.Command.CrossCutting.Ioc.Modules
{
    [ExcludeFromCodeCoverage]
    public static class DataModule
    {
        public static void Register(IServiceCollection services, IConfiguration configuration)
        {
            var clientParameters = configuration.GetSection("ClientApi").Get<ClientApiParameters>();
            services.AddSingleton(clientParameters);
            var contactParameters = configuration.GetSection("ContactApi").Get<ContactApiParameters>();
            services.AddSingleton(contactParameters);
            var balanceParameters = configuration.GetSection("BalanceApi").Get<BalanceApiParameters>();
            services.AddSingleton(balanceParameters);
            var transactionParameters = configuration.GetSection("TransactionApi").Get<TransactionApiParameters>();
            services.AddSingleton(transactionParameters);
            var kafkaParameters = configuration.GetSection("KafkaParameters").Get<KafkaParameters>();
            services.AddSingleton(kafkaParameters);


            var connectionString = configuration.GetConnectionString("DefaultConnection");
            var serverVersion = new MySqlServerVersion(new Version(8, 0, 26));

            services.AddDbContext<AppDbContext>(options => options.UseMySql(connectionString, serverVersion));

            services.AddScoped<ITransferRepository, TransferRepository>();
        }
    }
}
