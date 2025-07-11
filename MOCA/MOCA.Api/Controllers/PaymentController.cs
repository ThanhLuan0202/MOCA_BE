using AutoMapper.Internal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MOCA_Services.Constants;
using MOCA_Services.Interfaces;
using PayPalCheckoutSdk.Orders;

namespace MOCA.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IBookingPaymentService _bookingPaymentService;
        private readonly IDoctorBookingService _doctorBookingService;

        public PaymentController(IBookingPaymentService bookingPaymentService, IDoctorBookingService doctorBookingService)
        {
            _bookingPaymentService = bookingPaymentService;
            _doctorBookingService = doctorBookingService;
        }



        [HttpGet("success")]
        public async Task<IActionResult> SuccessPayment([FromQuery(Name = "orderCode")] int orderId)
        {
            var cf = await _bookingPaymentService.ConfirmPayment(orderId);
            var cfdb  = await _doctorBookingService.ConfirmDoctorBooking(orderId);

            if (cf != null)
            {

            }

            return Redirect($"https://moca.mom/payment-success");

        }


        [HttpGet("cancel")]
        public async Task<IActionResult> CancelPayment([FromQuery(Name = "orderCode")] int orderId)
        {
            var ccel = await _doctorBookingService.CancelDoctorBooking(orderId);

            if (ccel != null)
            {

            }

            return Redirect($"https://moca.mom/payment-cancel");
        }
    }
}
