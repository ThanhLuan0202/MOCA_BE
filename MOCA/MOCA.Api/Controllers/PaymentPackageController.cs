using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MOCA_Repositories.DBContext;
using MOCA_Repositories.Enitities;
using MOCA_Repositories.Models.PackageDTO;
using MOCA_Services.Interfaces;
using System;
using System.Security.Claims;

namespace MOCA.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentPackageController : ControllerBase
    {
        private readonly IPurchasedPackageService _purchasedPackageService;

        public PaymentPackageController(IPurchasedPackageService purchasedPackageService)
        {
           _purchasedPackageService = purchasedPackageService;
        }



        [HttpGet("success")]
        public async Task<IActionResult> SuccessPayment([FromQuery(Name = "orderCode")] int orderId)
        {
            var cf = await _purchasedPackageService.ConfirmPackage(orderId);
            

            if (cf != null)
            {

            }

            return Redirect($"https://moca.mom/payment-success");

        }


        [HttpGet("cancel")]
        public async Task<IActionResult> CancelPayment([FromQuery(Name = "orderCode")] int orderId)
        {
            var ccel = await _purchasedPackageService.CancelPackage(orderId);

            if (ccel != null)
            {

            }

            return Redirect($"https://moca.mom/payment-cancel");
        }
    }
}
