using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PayWithPaytm.Models
{
    public class PaytmPayment
    {
        public PaytmPayment()
        {
            Amount = 1;
            Country = "India";
        }
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Phone { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        [Required]
        public double Amount { get; set; }
        public string ClientIP { get; set; }
        public bool Paid { get; set; }
    }
}