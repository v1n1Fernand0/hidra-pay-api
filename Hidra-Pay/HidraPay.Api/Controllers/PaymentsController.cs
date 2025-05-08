using Microsoft.AspNetCore.Mvc;
using HidraPay.Api.Models;
using HidraPay.Application.UseCases.AuthorizePayment;
using HidraPay.Application.UseCases.CapturePayment;
using HidraPay.Application.UseCases.RefundPayment;
using HidraPay.Application.UseCases.GetStatus;
using HidraPay.Domain.ValueObjects;

namespace HidraPay.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentsController : ControllerBase
    {
        private readonly IAuthorizePaymentUseCase _authorize;
        private readonly ICapturePaymentUseCase _capture;
        private readonly IRefundPaymentUseCase _refund;
        private readonly IGetStatusUseCase _status;

        public PaymentsController(
            IAuthorizePaymentUseCase authorize,
            ICapturePaymentUseCase capture,
            IRefundPaymentUseCase refund,
            IGetStatusUseCase status)
        {
            _authorize = authorize;
            _capture = capture;
            _refund = refund;
            _status = status;
        }

        [HttpPost("authorize")]
        public async Task<ActionResult<PaymentResponseModel>> Authorize([FromBody] AuthorizeRequestModel model)
        {
            var request = new PaymentRequest(
                model.OrderId,
                model.Amount,
                model.Currency,
                model.Method,
                model.ExpiresAt
            );
            var result = await _authorize.ExecuteAsync(request);
            return Ok(new PaymentResponseModel
            {
                TransactionId = result.TransactionId,
                Status = result.Status,
                Amount = result.Amount
            });
        }

        [HttpPost("capture")]
        public async Task<ActionResult<PaymentResponseModel>> Capture([FromBody] CaptureRequestModel model)
        {
            var result = await _capture.ExecuteAsync(
                model.TransactionId,
                model.Amount,
                model.Method
            );
            return Ok(new PaymentResponseModel
            {
                TransactionId = result.TransactionId,
                Status = result.Status,
                Amount = result.Amount
            });
        }

        [HttpPost("refund")]
        public async Task<ActionResult<PaymentResponseModel>> Refund([FromBody] RefundRequestModel model)
        {
            var result = await _refund.ExecuteAsync(
                model.TransactionId,
                model.Amount,
                model.Method
            );
            return Ok(new PaymentResponseModel
            {
                TransactionId = result.TransactionId,
                Status = result.Status,
                Amount = result.Amount
            });
        }

        [HttpGet("status")]
        public async Task<ActionResult<PaymentResponseModel>> Status([FromQuery] StatusRequestModel model)
        {
            var status = await _status.ExecuteAsync(
                model.TransactionId,
                model.Method
            );
            return Ok(new PaymentResponseModel
            {
                TransactionId = model.TransactionId,
                Status = status,
                Amount = 0m
            });
        }
    }
}
