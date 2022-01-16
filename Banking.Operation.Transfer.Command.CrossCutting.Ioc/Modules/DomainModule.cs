using Banking.Operation.Transfer.Command.Domain.Transfer.Services;
using Banking.Operation.Transfer.Command.Infra.Data.MessageBroker;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace Banking.Operation.Transfer.Command.CrossCutting.Ioc.Modules
{
    [ExcludeFromCodeCoverage]
    public static class DomainModule
    {
        public static void Register(this IServiceCollection services)
        {
            services.AddScoped<ITransferService, TransferService>();
            services.AddScoped<IContactService, ContactService>();
            services.AddScoped<IClientService, ClientService>();
            services.AddScoped<IBalanceService, BalanceService>();
            services.AddScoped<ITransactionService, TransactionService>();
            services.AddScoped<IReceiptService, ReceiptService>();
        }
    }
}
