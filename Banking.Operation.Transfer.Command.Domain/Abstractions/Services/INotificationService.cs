using Banking.Operation.Transfer.Command.Domain.Transfer.Dtos;
using System.Threading.Tasks;

namespace Banking.Operation.Transfer.Command.Domain.Abstractions.Services
{
    public interface INotificationService
    {
        void PublishMessage(MessageDto message);
    }
}
