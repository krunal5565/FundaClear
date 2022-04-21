using Loyalty.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FundaClearApp.Models
{
    public class CustomerModel
    {
        public List<CustomerTransactionDTO> CustomerTransactions { get; set; }
        public CustomerDTO Customer { get; set; }
        public CustomerTransactionDTO CustomerTransaction { get; set; }
    }
}