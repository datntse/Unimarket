using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unimarket.Infracstruture.Data;
using Unimarket.Infracstruture.Repositories;

namespace Unimarket.Infracstruture.Services
{
    public interface IPaymentService
    {
        string CreatePaymentUrl(Guid packageId,int amount, string orderDescription, string locale);
    }
    public class PaymentService : IPaymentService
    {
        private readonly IConfiguration _configuration;
        private readonly IPackageService _packageService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PaymentService(IConfiguration configuration, IHttpContextAccessor httpContextAccessor, IPackageService packageService)
        {
            _configuration = configuration;
            _packageService = packageService;
            _httpContextAccessor = httpContextAccessor;
        }

        public string CreatePaymentUrl(Guid packageId,int amount, string orderDescription, string locale)
        {
            var package = _packageService.FindAsync(packageId);
            if (package == null)
            {
                throw new Exception("Package not found");
            }
            var vnPay = new VnPayLibrary();

            vnPay.AddRequestData("vnp_Version", _configuration["VnPay:Version"]);
            vnPay.AddRequestData("vnp_Command", _configuration["VnPay:Command"]);
            vnPay.AddRequestData("vnp_TmnCode", _configuration["VnPay:TmnCode"]);
            vnPay.AddRequestData("vnp_Amount", (amount * 100).ToString());
            vnPay.AddRequestData("vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss"));
            vnPay.AddRequestData("vnp_CurrCode", _configuration["VnPay:CurrCode"]);
            vnPay.AddRequestData("vnp_IpAddr", Utils.GetIpAddress(_httpContextAccessor.HttpContext));
            vnPay.AddRequestData("vnp_Locale", string.IsNullOrEmpty(locale) ? _configuration["VnPay:Locale"] : locale);
            vnPay.AddRequestData("vnp_OrderInfo", orderDescription);
            vnPay.AddRequestData("vnp_OrderType", "other");
            vnPay.AddRequestData("vnp_ReturnUrl", _configuration["VnPay:ReturnUrl"]);
            vnPay.AddRequestData("vnp_TxnRef", DateTime.Now.Ticks.ToString());
            //vnPay.AddRequestData("vnp_BankCode", "NCB");
    

            string paymentUrl = vnPay.CreateRequestUrl(_configuration["VnPay:BaseUrl"], _configuration["VnPay:HashSecret"]);

            return paymentUrl;
        }
    }
}
