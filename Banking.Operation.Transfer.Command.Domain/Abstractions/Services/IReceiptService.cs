using Banking.Operation.Transfer.Command.Domain.Transfer.Dtos;
using System.Threading.Tasks;

namespace Banking.Operation.Transfer.Command.Domain.Abstractions.Services
{
    public interface IReceiptService
    {
        Task PublishReceipt(ReceiptDto receipt);
    }
}
