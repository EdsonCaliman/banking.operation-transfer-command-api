using Banking.Operation.Transfer.Command.Domain.Abstractions.Exceptions;
using Banking.Operation.Transfer.Command.Domain.Abstractions.Messages;
using Banking.Operation.Transfer.Command.Domain.Transfer.Dtos;
using Banking.Operation.Transfer.Command.Domain.Transfer.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Banking.Operation.Transfer.Command.Api.Controllers
{
    [ApiVersion("1.0")]
    [Route("/v{version:apiVersion}/banking-operation/{clientid}/transfers")]
    [ApiController]
    public class TransferController : Controller
    {
        private readonly ILogger<TransferController> _logger;
        private readonly ITransferService _transferService;

        public TransferController(ILogger<TransferController> logger, ITransferService transferService)
        {
            _logger = logger;
            _transferService = transferService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ResponseTransferDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BussinessMessage), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Save(Guid clientid, RequestTransferDto transfer)
        {
            _logger.LogInformation("Receive Save...");

            try
            {
                var transaction = await _transferService.Save(clientid, transfer);

                return Ok(transaction);
            }
            catch (BussinessException bex)
            {
                return BadRequest(new BussinessMessage(bex.Type, bex.Message));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Save exception: {ex}");
                return BadRequest();
            }
        }
    }
}
