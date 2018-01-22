using PayWithPaytm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using paytm;
using PayWithPaytm.Services;

namespace PayWithPaytm.Controllers
{
    public class HomeController : Controller
    {
        private readonly PaytmContext context;
        private readonly PaymentService payment;
        public HomeController()
        {
            context = new PaytmContext();
            payment = new PaymentService();
        }

        public ActionResult Index()
        {
            var model = new PaytmPayment();
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(PaytmPayment model)
        {
            if(ModelState.IsValid)
            {
                model.ClientIP = Request.UserHostAddress;
                context.PaytmPayments.Add(model);
                context.SaveChanges();
                string paymentPage = payment.CreateRequest(model);
                return Content(paymentPage);
            }
            return View(model);
        }

        public ActionResult CallBack()
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            foreach (string key in Request.Form.Keys)
            {
                parameters.Add(key.Trim(), Request.Form[key].Trim());
            }
            bool isPaymentSuccess = payment.IsPaymentSuccess(parameters);

            if (isPaymentSuccess)
            {
                int orderId = Convert.ToInt32(Request.Form["ORDERID"].Trim());
                var model = context.PaytmPayments.Find(orderId);
                model.Paid = true;
                context.SaveChanges();
                return Content("payment successfull");
            }

            return Content("payment failed");
        }
    }
}