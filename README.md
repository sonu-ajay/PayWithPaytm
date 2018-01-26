# PayWithPaytm

To use this project I am using stagin url of Paytm. <br/>
You can add your MID(Merchant ID) and Merchant Key in the Web.Config File and you are good to go.<br/>
Below is the sample of the Configs.
```
    <add key="PAYTM_URL" value="Paytm Payment Url"/>
    <add key="MID" value="Merchant ID"/>
    <add key="Merchant_KEY" value="Merchant Key"/>
    <add key="CHANNEL_ID" value="WEB"/>
    <add key="INDUSTRY_TYPE_ID" value="Retail"/>
    <add key="CALLBACK_URL" value="Return or Response Url"/>
```
I have referenced paytm.dll 4.5 in my project.<br/>
You can find and update you dlls from below url. <br/>
<a href="https://github.com/Paytm-Payments/Paytm_Web_Sample_Kit_dotNet" target="_blank">https://github.com/Paytm-Payments/Paytm_Web_Sample_Kit_dotNet</a><br/>
Once you are done with developement and testing replace the config values to production.<br/>
