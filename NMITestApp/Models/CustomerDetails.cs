using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace NMITestApp.Models
{      
    public class CustomerDetails
    {        
        [Display(Name = "Customer Vault ID")]
        public int CustomerVaultId { get; set; }
       
        [Display(Name = "Company")]
        public string BillingAddressCompany { get; set; }

        [Display(Name = "First Name")]
        public string BillingAddressFirstName { get; set; }

        [Display(Name = "Last Name")]
        public string BillingAddressLastName { get; set; }
        
        [Display(Name ="Address")]
        public string BillingAddressAddress1 { get; set; }

        [Display(Name ="City")]
        public string BillingAddressCity { get; set; }

        [Display(Name = "State")]
        public string BillingAddressState { get; set; }

        [Display(Name = "Zip")]
        public string BillingAddressZip { get; set; }

        [Display(Name = "Country")]
        public string BillingAddressCountry { get; set; }

        [Display(Name = "Phone")]
        public string BillingAddressPhone { get; set; }

        [Display(Name = "Email")]
        public string BillingAddressEmail { get; set; }

        [Display(Name = "First Name")]
        public string ShippingAddressFirstName { get; set; }

        [Display(Name = "Last Name")]
        public string ShippingAddressLastName { get; set; }

        [Display(Name = "Address")]
        public string ShippingAddressAddress1 { get; set; }

        [Display(Name = "Suite")]
        public string ShippingAddressAddress2 { get; set; }

        [Display(Name = "City")]
        public string ShippingAddressCity { get; set; }

        [Display(Name = "State")]
        public string ShippingAddressState { get; set; }

        [Display(Name = "Zip")]
        public string ShippingAddressZip { get; set; }

        [Display(Name = "Country")]
        public string ShippingAddressCountry { get; set; }

        [Display(Name = "Phone")]
        public string ShippingAddressPhone { get; set; }
     
    }
}