using Microsoft.AspNetCore.Mvc;
using HidraPay.Api.Models;
using HidraPay.Application.Services;
using HidraPay.Domain.ValueObjects;
using HidraPay.Domain.Enums;

namespace HidraPay.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentsController : ControllerBase
    {
        private readonly PaymentService _service;

        public PaymentsController(PaymentService service)
        {
            _service = service;
        }

        [HttpPost("authorize")]
        public async Task<ActionResult<PaymentResponseModel>> Authorize([FromBody] AuthorizeRequestModel model)
        {
            var request = new PaymentRequest(
                OrderId: model.OrderId,
                Amount: model.Amount,
                Currency: model.Currency,
                Method: Enum.Parse<PaymentMethod>(model.Method, ignoreCase: true),
                ExpiresAt: model.ExpiresAt
            );

            var result = await _service.AuthorizeAsync(request);

            var response = new PaymentResponseModel
            {
                TransactionId = result.TransactionId,
                Status = result.Status,
                Amount = result.Amount
            };

            return Ok(response);
        }
    }
}
