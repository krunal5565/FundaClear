using System;
using Loyalty.DataLayer;
using Loyalty.DTO;
using Loyalty.ServiceLibrary;
using Loyalty.Shared;
using System.Data.SqlClient;
using FundaClearApp.Utilities;
using System.Collections.Generic;
using System.Linq;

namespace FundaClear.Business
{
    public static class APIHelper
    {

        public static CustomerDTO GetCustomer(string tenantID, string customerID)
        {
            string connectionString = ConnectionHelper.GetConnectionString();

            CustomerDTO objCustomerDTO = new CustomerDTO();

            CustomerManager objCustomerManager = new CustomerManager(connectionString);
            ResponseDTO objResponseDTO = objCustomerManager.GetCustomerById(new CustomerDTO { CustomerId = customerID, CurrentTenantId = tenantID });

            if (objResponseDTO != null && objResponseDTO.Data != null)
            {
                objCustomerDTO = objResponseDTO.Data as CustomerDTO;
            }

            return objCustomerDTO;
        }

        public static ResponseDTO UpdateCustomer(CustomerDTO objCustomerDTO)
        {
            string connectionString = ConnectionHelper.GetConnectionString();

            CustomerManager objCustomerManager = new CustomerManager(connectionString);

            ResponseDTO customerResponse = objCustomerManager.Update(objCustomerDTO);

            return customerResponse;
        }


        public static List<CustomerTransactionDTO> GetCustomerTransactions(string tenantID, string customerID)
        {
            string connectionString = ConnectionHelper.GetConnectionString();

            List<CustomerTransactionDTO> lstCustomerTransactions = new List<CustomerTransactionDTO>();
            CustomerManager objCustomerManager = new CustomerManager(connectionString);
            ResponseDTO objResponseCustomerTransactions = objCustomerManager.GetCustomerTransactions(tenantID, customerID);

            if (objResponseCustomerTransactions != null && objResponseCustomerTransactions.DataList != null)
            {
                lstCustomerTransactions = objResponseCustomerTransactions.DataList as List<CustomerTransactionDTO>;
            }

            return lstCustomerTransactions;
        }

        public static List<CustomerDTO> GetAllCustomers(string tenantID)
        {
            string connectionString = ConnectionHelper.GetConnectionString();

            List<CustomerDTO> lstCustomerDTO = new List<CustomerDTO>();
            CustomerManager objCustomerManager = new CustomerManager(connectionString);
            ResponseDTO customerResponse = objCustomerManager.GetAllCustomers(tenantID);

            if (customerResponse != null && customerResponse.DataList != null)
            {
                lstCustomerDTO = customerResponse.DataList as List<CustomerDTO>;

                if (lstCustomerDTO != null)
                {
                    lstCustomerDTO = lstCustomerDTO.Where(x => x.ActiveStatus).ToList();
                }
            }
            return lstCustomerDTO;
        }
    }
}
