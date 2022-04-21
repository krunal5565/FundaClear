using Loyalty.DTO;
using Loyalty.ServiceLibrary;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Loyalty.Sample.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        private readonly string custId;
        private readonly string tenantId;

        private CustomerManager customerManager;
        private TenantManager tenantManager;
        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
            tenantId = "23";
            custId = "15149f0e-62f2-4d22-bd9c-3896f41fbc6d";
            customerManager = new CustomerManager(@"Data Source=164.52.195.234;Initial Catalog=Loyalty_dev;user id=Loyalty_dev;password=Dell@2022");
            tenantManager = new TenantManager(@"Data Source=DESKTOP-QL7LPL9\SQLEXPRESS;Initial Catalog=Loyalty;user id=sa;password=P@ssw0rd");

        }

        public void OnGet()
        {
            GetAllCustomers();
        }

        private void Savecustomer()
        {
            CustomerDTO dto = new CustomerDTO()
            {
                ActiveStatus = true,
                Address = "BTM REsidency",
                CreatedBy = "Prasant",
                EmailId = "test@test.com",
                Locality = "Bangalore",
                LoginId = "123",
                PassKey = "passkey",
                MobileNumber = "9937056946",
                CustomerName = "Prasant samal",
                CurrentTenantId = "23",
                CreatedDate = DateTime.UtcNow,
                BalancePoint =3,
                ModifiedBy ="krunal",
                ModifiedDate = DateTime.UtcNow,



            };
            var x = customerManager.Save(dto);
        }
        private void GetCustomer()
        {

            var x = customerManager.GetCustomerById(new CustomerDTO { CustomerId = custId, CurrentTenantId = tenantId });
        }
        private void GetAllCustomers()
        {

            var x = customerManager.GetAllCustomers( tenantId );
        }
        private void GetCustomerByEmailMobile()
        {

            var x = customerManager.GetCustomerByMobileEmail(new CustomerDTO { MobileNumber = "7878787878",EmailId="krunal@gmail.com", CurrentTenantId = tenantId });
        }
        private void SaveTenant()
        {
            TenantDTO tenantDTO = new TenantDTO()
            {
                ActiveStatus = true,
                Address = "BTM REsidency",
                ContactPerson = " Person",
                CreatedBy = "Prasant",
                EmailId = "test@test.com1",
                Locality = "Bangalore",
                LoginId = "99370569461",
                PassKey = "test",
                MobileNumber = "99370569461",
                TenantName = "Prasant Shop",
                PointPercentage = 10
            };
            var x = tenantManager.Save(tenantDTO);
        }

        private void SaveTransaction()
        {
            var trans = new CustomerTransactionDTO
            {
                ActiveStatus = true,
                BillAmount = 2500,
                BillDate = DateTime.Now,
                BillNumber = "bill/1",
                CreatedBy = "prasant",
                CustId = custId,
                TenantId = tenantId
            };
            var obj = customerManager.SaveCustomerTransaction(trans);
        }
    }
}
