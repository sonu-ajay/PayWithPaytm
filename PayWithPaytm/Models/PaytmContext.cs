using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PayWithPaytm.Models
{
    public class PaytmContext : DbContext
    {
        public PaytmContext() : base("PaytmDBConnection")
        {
            Database.SetInitializer<PaytmContext>(new CreateDatabaseIfNotExists<PaytmContext>());
        }

        public DbSet<PaytmPayment> PaytmPayments { get; set; }
    }
}