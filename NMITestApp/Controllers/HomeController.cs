using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NMITestApp.Models;
using System.Xml.Serialization;
using System.Xml;
using System.IO;
using System.Net;

namespace NMITestApp.Controllers
{
    public class HomeController : Controller
    {     
        public ActionResult Step1()
        {
            if (Request["token-id"] != null)
            {
                //MessageBox.Show(Request["token-id"]);
                XmlDocument xmlRequest = new XmlDocument();

                XmlDeclaration xmlDecl = xmlRequest.CreateXmlDeclaration("1.0", "UTF-8", "yes");

                XmlElement root = xmlRequest.DocumentElement;
                xmlRequest.InsertBefore(xmlDecl, root);


                XmlElement xmlCompleteTransaction = xmlRequest.CreateElement("complete-action");

                XmlElement xmlApiKey = xmlRequest.CreateElement("api-key");

                xmlApiKey.InnerText = "2F822Rw39fx762MaV7Yy86jXGTC7sCDy";

                xmlCompleteTransaction.AppendChild(xmlApiKey);


                XmlElement xmlTokenId = xmlRequest.CreateElement("token-id");
                xmlTokenId.InnerText = Request["token-id"];
                xmlCompleteTransaction.AppendChild(xmlTokenId);


                xmlRequest.AppendChild(xmlCompleteTransaction);


                string responseFromServer = this.SendXMLRequest(xmlRequest);
                XmlReader responseReader = XmlReader.Create(new StringReader(responseFromServer));


                XmlDocument xDoc = new XmlDocument();
                xDoc.Load(responseReader);
                XmlNodeList response = xDoc.GetElementsByTagName("result");
                XmlNodeList responseText = xDoc.GetElementsByTagName("result-text");

                Session["data"] = responseFromServer;
                Session["result"] = response[0].InnerText;
                Session["result-text"] = responseText[0].InnerText;

                responseReader.Close();
                return View("Step3");

            }
            return View();
        }      

        [HttpPost]
        public ActionResult SubmitCustomerDetails(CustomerDetails customerDetails)
        {
            XmlDocument xmlRequest = new XmlDocument();

            XmlDeclaration xmlDecl = xmlRequest.CreateXmlDeclaration("1.0", "UTF-8", "yes");

            XmlElement root = xmlRequest.DocumentElement;
            xmlRequest.InsertBefore(xmlDecl, root);


            XmlElement xmlSale = xmlRequest.CreateElement("sale");

            XmlElement xmlApiKey = xmlRequest.CreateElement("api-key");

            xmlApiKey.InnerText = "2F822Rw39fx762MaV7Yy86jXGTC7sCDy";

            xmlSale.AppendChild(xmlApiKey);

            XmlElement xmlRedirectUrl = xmlRequest.CreateElement("redirect-url");
            xmlRedirectUrl.InnerText = Request.ServerVariables["HTTP_REFERER"];      //////////////////////////////////change redirect url
            xmlSale.AppendChild(xmlRedirectUrl);

            XmlElement xmlAmount = xmlRequest.CreateElement("amount");
            xmlAmount.InnerText = "12.00";
            xmlSale.AppendChild(xmlAmount);

            XmlElement xmlRemoteAddr = xmlRequest.CreateElement("ip-address");
            xmlRemoteAddr.InnerText = Request.ServerVariables["REMOTE_ADDR"];      
            xmlSale.AppendChild(xmlRemoteAddr);

            XmlElement xmlCurrency = xmlRequest.CreateElement("currency");
            xmlCurrency.InnerText = "USD";
            xmlSale.AppendChild(xmlCurrency);

            XmlElement xmlOrderId = xmlRequest.CreateElement("order-id");
            xmlOrderId.InnerText = "1234";
            xmlSale.AppendChild(xmlOrderId);

            XmlElement xmlOrderDescription = xmlRequest.CreateElement("order-description");
            xmlOrderDescription.InnerText = "Small Order";
            xmlSale.AppendChild(xmlOrderDescription);

            XmlElement xmlMDF1 = xmlRequest.CreateElement("merchant-defined-field-1");
            xmlMDF1.InnerText = "Red";
            xmlSale.AppendChild(xmlMDF1);

            XmlElement xmlMDF2 = xmlRequest.CreateElement("merchant-defined-field-2");
            xmlMDF2.InnerText = "Medium";
            xmlSale.AppendChild(xmlMDF2);

            XmlElement xmlTax = xmlRequest.CreateElement("tax-amount");
            xmlTax.InnerText = "0.00";
            xmlSale.AppendChild(xmlTax);

            XmlElement xmlShipping = xmlRequest.CreateElement("shipping-amount");
            xmlShipping.InnerText = "0.00";
            xmlSale.AppendChild(xmlShipping);

            if (!(customerDetails.CustomerVaultId.Equals(0)))
            {
                XmlElement xmlCustomerVaultId = xmlRequest.CreateElement("customer-vault-id");
                xmlCustomerVaultId.InnerText = customerDetails.CustomerVaultId.ToString();
                xmlSale.AppendChild(xmlCustomerVaultId);

            }
            //To Add a customer
             else
             {
                 XmlElement xmlAddCustomer = xmlRequest.CreateElement("add-customer");

                 XmlElement xmlCustomerVaultId = xmlRequest.CreateElement("customer-vault-id");
                 xmlCustomerVaultId.InnerText = "411";
                 xmlAddCustomer.AppendChild(xmlCustomerVaultId);

                 xmlSale.AppendChild(xmlAddCustomer);
             }
            
            XmlElement xmlBillingAddress = xmlRequest.CreateElement("billing");

            XmlElement xmlFirstName = xmlRequest.CreateElement("first-name");
            xmlFirstName.InnerText = customerDetails.BillingAddressFirstName;
            xmlBillingAddress.AppendChild(xmlFirstName);

            XmlElement xmlLastName = xmlRequest.CreateElement("last-name");
            xmlLastName.InnerText = customerDetails.BillingAddressLastName;
            xmlBillingAddress.AppendChild(xmlLastName);

            XmlElement xmlAddress1 = xmlRequest.CreateElement("address1");
            xmlAddress1.InnerText = customerDetails.BillingAddressAddress1;
            xmlBillingAddress.AppendChild(xmlAddress1);

            XmlElement xmlCity = xmlRequest.CreateElement("city");
            xmlCity.InnerText = customerDetails.BillingAddressCity;
            xmlBillingAddress.AppendChild(xmlCity);

            XmlElement xmlState = xmlRequest.CreateElement("state");
            xmlState.InnerText = customerDetails.BillingAddressState;
            xmlBillingAddress.AppendChild(xmlState);

            XmlElement xmlZip = xmlRequest.CreateElement("postal");
            xmlZip.InnerText = customerDetails.BillingAddressZip;
            xmlBillingAddress.AppendChild(xmlZip);

            XmlElement xmlCountry = xmlRequest.CreateElement("country");
            xmlCountry.InnerText = customerDetails.BillingAddressCountry;
            xmlBillingAddress.AppendChild(xmlCountry);

            XmlElement xmlPhone = xmlRequest.CreateElement("phone");
            xmlPhone.InnerText = customerDetails.BillingAddressPhone;
            xmlBillingAddress.AppendChild(xmlPhone);

            XmlElement xmlCompany = xmlRequest.CreateElement("company");
            xmlCompany.InnerText = customerDetails.BillingAddressCompany;
            xmlBillingAddress.AppendChild(xmlCompany);

            XmlElement xmlAddress2 = xmlRequest.CreateElement("address2");
            xmlAddress2.InnerText = "";
            xmlBillingAddress.AppendChild(xmlAddress2);

            XmlElement xmlFax = xmlRequest.CreateElement("fax");
            xmlFax.InnerText = "";
            xmlBillingAddress.AppendChild(xmlFax);


            xmlSale.AppendChild(xmlBillingAddress);

            //////////

            XmlElement xmlShippingAddress = xmlRequest.CreateElement("shipping");

            XmlElement xmlSFirstName = xmlRequest.CreateElement("first-name");
            xmlSFirstName.InnerText = customerDetails.ShippingAddressFirstName;
            xmlShippingAddress.AppendChild(xmlSFirstName);

            XmlElement xmlSLastName = xmlRequest.CreateElement("last-name");
            xmlSLastName.InnerText = customerDetails.ShippingAddressLastName;
            xmlShippingAddress.AppendChild(xmlSLastName);

            XmlElement xmlSAddress1 = xmlRequest.CreateElement("address1");
            xmlSAddress1.InnerText = customerDetails.ShippingAddressAddress1;
            xmlShippingAddress.AppendChild(xmlSAddress1);

            XmlElement xmlSCity = xmlRequest.CreateElement("city");
            xmlSCity.InnerText = customerDetails.ShippingAddressCity;
            xmlShippingAddress.AppendChild(xmlSCity);

            XmlElement xmlSState = xmlRequest.CreateElement("state");
            xmlSState.InnerText = customerDetails.ShippingAddressState;
            xmlShippingAddress.AppendChild(xmlSState);

            XmlElement xmlSZip = xmlRequest.CreateElement("postal");
            xmlSZip.InnerText = customerDetails.ShippingAddressZip;
            xmlShippingAddress.AppendChild(xmlSZip);

            XmlElement xmlSCountry = xmlRequest.CreateElement("country");
            xmlSCountry.InnerText = customerDetails.ShippingAddressCountry;
            xmlShippingAddress.AppendChild(xmlSCountry);

            XmlElement xmlSPhone = xmlRequest.CreateElement("phone");
            xmlSPhone.InnerText = customerDetails.ShippingAddressPhone;
            xmlShippingAddress.AppendChild(xmlSPhone);

            XmlElement xmlSCompany = xmlRequest.CreateElement("company");
            xmlSCompany.InnerText = "";
            xmlShippingAddress.AppendChild(xmlSCompany);

            XmlElement xmlSAddress2 = xmlRequest.CreateElement("address2");
            xmlSAddress2.InnerText = customerDetails.ShippingAddressAddress2;
            xmlShippingAddress.AppendChild(xmlSAddress2);

            XmlElement xmlSFax = xmlRequest.CreateElement("fax");
            xmlFax.InnerText = "";
            xmlShippingAddress.AppendChild(xmlSFax);


            xmlSale.AppendChild(xmlShippingAddress);

            ////////////////

            XmlElement xmlProduct = xmlRequest.CreateElement("product");

            XmlElement xmlSku = xmlRequest.CreateElement("product-code");
            xmlSku.InnerText = "SKU-123456";
            xmlProduct.AppendChild(xmlSku);

            XmlElement xmlDescription = xmlRequest.CreateElement("description");
            xmlDescription.InnerText = "Books";
            xmlProduct.AppendChild(xmlDescription);

            XmlElement xmlQuantity = xmlRequest.CreateElement("quantity");
            xmlQuantity.InnerText = "1";
            xmlProduct.AppendChild(xmlQuantity);

            XmlElement xmlUnit = xmlRequest.CreateElement("unit-of-measure");
            xmlUnit.InnerText = "1";
            xmlProduct.AppendChild(xmlUnit);


            XmlElement xmlUnitAmount = xmlRequest.CreateElement("total-amount");
            xmlUnitAmount.InnerText = "1";
            xmlProduct.AppendChild(xmlUnitAmount);

            XmlElement xmlUnitDiscount = xmlRequest.CreateElement("discount-amount");
            xmlUnitDiscount.InnerText = "0.00";
            xmlProduct.AppendChild(xmlUnitDiscount);


            XmlElement xmlUnitTax = xmlRequest.CreateElement("tax-amount");
            xmlUnitTax.InnerText = "0.00";
            xmlProduct.AppendChild(xmlUnitTax);


            XmlElement xmlTaxRate = xmlRequest.CreateElement("tax-rate");
            xmlTaxRate.InnerText = "0.01";
            xmlProduct.AppendChild(xmlTaxRate);

            xmlSale.AppendChild(xmlProduct);

            ///////////////

            XmlElement xmlProduct2 = xmlRequest.CreateElement("product");

            XmlElement xmlSku2 = xmlRequest.CreateElement("product-code");
            xmlSku2.InnerText = "SKU-654321";
            xmlProduct2.AppendChild(xmlSku2);

            XmlElement xmlDescription2 = xmlRequest.CreateElement("description");
            xmlDescription2.InnerText = "Videos";
            xmlProduct2.AppendChild(xmlDescription2);

            XmlElement xmlQuantity2 = xmlRequest.CreateElement("quantity");
            xmlQuantity2.InnerText = "1";
            xmlProduct2.AppendChild(xmlQuantity2);

            XmlElement xmlUnit2 = xmlRequest.CreateElement("unit-of-measure");
            xmlUnit2.InnerText = "";
            xmlProduct2.AppendChild(xmlUnit2);

            XmlElement xmlUnitAmount2 = xmlRequest.CreateElement("total-amount");
            xmlUnitAmount2.InnerText = "2";
            xmlProduct2.AppendChild(xmlUnitAmount2);

            XmlElement xmlUnitDiscount2 = xmlRequest.CreateElement("discount-amount");
            xmlUnitDiscount2.InnerText = "0.00";
            xmlProduct2.AppendChild(xmlUnitDiscount2);

            XmlElement xmlUnitTax2 = xmlRequest.CreateElement("tax-amount");
            xmlUnitTax2.InnerText = "0.00";
            xmlProduct2.AppendChild(xmlUnitTax2);


            XmlElement xmlTaxRate2 = xmlRequest.CreateElement("tax-rate");
            xmlTaxRate2.InnerText = "0.01";
            xmlProduct2.AppendChild(xmlTaxRate2);

            xmlSale.AppendChild(xmlProduct2);

            ///////////////////

            xmlRequest.AppendChild(xmlSale);


            string responseFromServer = this.SendXMLRequest(xmlRequest);


            XmlReader responseReader = XmlReader.Create(new StringReader(responseFromServer));


            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(responseReader);
            XmlNodeList response = xDoc.GetElementsByTagName("result");
            XmlNodeList resultText = xDoc.GetElementsByTagName("result-text");
            if (response[0].InnerText.Equals("1"))
            {
                XmlNodeList formUrl = xDoc.GetElementsByTagName("form-url");
                Session["formURL"] = "";
                Session["formURL"] = formUrl[0].InnerText;
                responseReader.Close();
                return View("Step2");
            }
            if(response[0].InnerText.Equals("2"))
            {
                ViewBag.error = "Transaction declined. Error: " + resultText[0].InnerText;
                return View("Step1", customerDetails);
            }
            if (response[0].InnerText.Equals("3"))
            {
                ViewBag.error = "Error in transaction date or system error. Error: " + resultText[0].InnerText;
                return View("Step1", customerDetails);
            }

            ViewBag.error = "Error in communicating with NMI. Try again!";
            return View("Step1", customerDetails);
        }

        protected string SendXMLRequest(XmlDocument xmlRequest)
        {
          
            string uri = "https://secure.networkmerchants.com/api/v2/three-step";

            WebRequest req = WebRequest.Create(uri);
            //req.Proxy = WebProxy.GetDefaultProxy(); // Enable if using proxy
            req.Method = "POST";        // Post method
            req.ContentType = "text/xml";     // content type

            // Wrap the request stream with a text-based writer
            StreamWriter writer = new StreamWriter(req.GetRequestStream());

            // Write the XML text into the stream
            xmlRequest.Save(writer);
            writer.Close();

            // Send the data to the webserver
            WebResponse rsp = req.GetResponse();
            Stream dataStream = rsp.GetResponseStream();

            // Open the stream using a StreamReader 
            StreamReader reader = new StreamReader(dataStream);

            // Read the content.
            string responseFromServer = reader.ReadToEnd();

            // int index = responseFromServer.IndexOf("<?");
            //string substr = responseFromServer.Substring(index);
            // Display the content.
            //MessageBox.Show(responseFromServer);
            // Clean up the streams.

            reader.Close();
            dataStream.Close();
            rsp.Close();

            return responseFromServer;
        }
    }
}