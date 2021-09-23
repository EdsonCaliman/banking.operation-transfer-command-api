﻿using Banking.Operation.Transfer.Command.Domain.Transfer.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Banking.Operation.Transfer.Command.CrossCutting.Ioc.Modules
{
    public static class DomainModule
    {
        public static void Register(this IServiceCollection services)
        {
            services.AddScoped<ITransferService, TransferService>();
            services.AddScoped<IContactService, ContactService>();
            services.AddScoped<IClientService, ClientService>();
        }
    }
}
