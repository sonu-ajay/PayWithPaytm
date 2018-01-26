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
            model.Name = "SomeName";
            model.Email = "email@example.com";
            model.Phone = "7777777777";
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(PaytmPayment model)
        {
            if(ModelState.IsValid)
            {
                model.ClientIP = Request.UserHostAddress;
                model.TimeStampInitiated = DateTime.Now;
                model.OrderId = DateTime.Now.ToString("MMyyyyMMddhhmmss");
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
            string paytmChecksum = "";
            bool isPaymentSuccess = payment.IsPaymentSuccess(parameters,out paytmChecksum);

            if (isPaymentSuccess)
            {
                string orderId = Request.Form["ORDERID"].Trim();
                var model = context.PaytmPayments.Where(m=>m.OrderId==orderId).FirstOrDefault();
                model.Paid = true;
                model.CheckSum = paytmChecksum;
                model.TimeStampSuccess = DateTime.Now;
                context.SaveChanges();
                return Redirect("Reciept?Id=" + model.OrderId);
            }

            return RedirectToAction("Failed");
        }

        public ActionResult Reciept(string Id)
        {
            var model = context.PaytmPayments.Where(m => m.OrderId == Id).FirstOrDefault();
            return View(model);
        }

        public ActionResult Failed()
        {            
            return View();
        }
    }
}