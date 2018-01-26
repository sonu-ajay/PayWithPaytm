using PayWithPaytm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using paytm;

namespace PayWithPaytm.Services
{
    public class PaymentService
    {
        public string CreateRequest(PaytmPayment model)
        {
            String merchantKey = GetConfig.Get("Merchant_KEY");
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("MID", GetConfig.Get("MID"));
            parameters.Add("CHANNEL_ID", GetConfig.Get("CHANNEL_ID"));
            parameters.Add("INDUSTRY_TYPE_ID", GetConfig.Get("INDUSTRY_TYPE_ID"));
            parameters.Add("WEBSITE", "WEB_STAGING");
            parameters.Add("EMAIL", model.Email);
            parameters.Add("MOBILE_NO", model.Phone);
            parameters.Add("CUST_ID", model.Id.ToString());
            parameters.Add("ORDER_ID", model.OrderId.ToString());
            parameters.Add("TXN_AMOUNT", model.Amount.ToString());
            parameters.Add("CALLBACK_URL", GetConfig.Get("CALLBACK_URL")); //This parameter is not mandatory. Use this to pass the callback url dynamically.

            string checksum = CheckSum.generateCheckSum(merchantKey, parameters);

            string paytmURL = GetConfig.Get("PAYTM_URL") + model.Id.ToString();

            string outputHTML = "<html>";
            outputHTML += "<head>";
            outputHTML += "<title>Merchant Check Out Page</title>";
            outputHTML += "</head>";
            outputHTML += "<body>";
            outputHTML += "<center><h1>Please do not refresh this page...</h1></center>";
            outputHTML += "<form method='post' action='" + paytmURL + "' name='f1'>";
            outputHTML += "<table border='1'>";
            outputHTML += "<tbody>";
            foreach (string key in parameters.Keys)
            {
                outputHTML += "<input type='hidden' name='" + key + "' value='" + parameters[key] + "'>";
            }
            outputHTML += "<input type='hidden' name='CHECKSUMHASH' value='" + checksum + "'>";
            outputHTML += "</tbody>";
            outputHTML += "</table>";
            outputHTML += "<script type='text/javascript'>";
            outputHTML += "document.f1.submit();";
            outputHTML += "</script>";
            outputHTML += "</form>";
            outputHTML += "</body>";
            outputHTML += "</html>";
            return outputHTML;
        }

        public bool IsPaymentSuccess(Dictionary<string, string> parameters,out string paytmChecksum)
        {
            bool isPaymentSuccess = false;

            String merchantKey = GetConfig.Get("Merchant_KEY"); // Replace the with the Merchant Key provided by Paytm at the time of registration.

            paytmChecksum = "";

            if (parameters.ContainsKey("CHECKSUMHASH"))
            {
                paytmChecksum = parameters["CHECKSUMHASH"];                
                parameters.Remove("CHECKSUMHASH");
            }

            if (CheckSum.verifyCheckSum(merchantKey, parameters, paytmChecksum) && parameters["STATUS"]== "TXN_SUCCESS")
                isPaymentSuccess = true;

            return isPaymentSuccess;
        }
    }
}