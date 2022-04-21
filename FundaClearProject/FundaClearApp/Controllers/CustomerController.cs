using FundaClear.Business;
using FundaClearApp.Models;
using FundaClearApp.Utilities;
using Loyalty.DTO;
using Loyalty.ServiceLibrary;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;


namespace FundaClearApp.Controllers
{
    public class CustomerController : Controller
    {
        public string connectionString;
        public CustomerController()
        {
            connectionString = Helper.GetConnectionString();
        }

        private string GetSessionTanantID()
        {
            return HttpContext != null && HttpContext.Session != null ? HttpContext.Session.GetString("TenantId") : "";
        }

        private string GetSessionTanantName()
        {
          return  HttpContext != null && HttpContext.Session != null ? HttpContext.Session.GetString("TenantName") : "";
        }

        public ActionResult AddCustomerTransaction(CustomerTransactionDTO model)
        {
            CustomerManager objCustomerManager = new CustomerManager(connectionString);
            model.TenantId = GetSessionTanantID();
            model.CustId = model.CustId;
            model.CreatedBy = GetSessionTanantName() ;
            model.BillDate = DateTime.UtcNow;
            model.ActiveStatus = true;
            ResponseDTO customerResponse = objCustomerManager.SaveCustomerTransaction(model);

            return RedirectToAction("Edit", new { id = model.CustId });
        }

        public ActionResult Index()
        {
            List<CustomerDTO> lstCustomerDTO = GetAllCustomers();
            return View(lstCustomerDTO);
        }

        [HttpGet]
        public ActionResult Add()
        {
            CustomerModel objCustomerModel = new CustomerModel();
            objCustomerModel.Customer = new CustomerDTO();
            return View("Add", objCustomerModel);
        }

        [HttpPost]
        public bool Delete(string id)
        {
            bool status = false;
           
            CustomerDTO objCustomerDTO = GetCustomer(GetSessionTanantID(), id);
            objCustomerDTO.ActiveStatus = false;

            ResponseDTO objCustResponseDTO = UpdateCustomer(objCustomerDTO);
            if(objCustResponseDTO != null)
            {
                status = objCustResponseDTO.Status;
            }

            return status;
        }

        [HttpGet]
        public ActionResult Edit(string id)
        {
            CustomerModel objCustomerModel = new CustomerModel();

            objCustomerModel.Customer = GetCustomer(GetSessionTanantID(), id);
            objCustomerModel.CustomerTransactions = GetCustomerTransactions(GetSessionTanantID(), id);
            objCustomerModel.CustomerTransaction = new CustomerTransactionDTO { CustId = id };
            return View("Edit", objCustomerModel);
        }

        public CustomerDTO GetCustomer(string tenantID, string customerID)
        {
            CustomerDTO objCustomerDTO = new CustomerDTO();

            CustomerManager objCustomerManager = new CustomerManager(connectionString);
            ResponseDTO objResponseDTO = objCustomerManager.GetCustomerById(new CustomerDTO { CustomerId = customerID, CurrentTenantId = tenantID });

            if (objResponseDTO != null && objResponseDTO.Data != null)
            {
                objCustomerDTO = objResponseDTO.Data as CustomerDTO;
            }

            return objCustomerDTO;
        }

        public ResponseDTO UpdateCustomer(CustomerDTO objCustomerDTO)
        {
            CustomerManager objCustomerManager = new CustomerManager(connectionString);

            ResponseDTO customerResponse = objCustomerManager.Update(objCustomerDTO);

            return customerResponse;

        }

        public List<CustomerDTO> GetAllCustomers()
        {
            List<CustomerDTO> lstCustomerDTO = new List<CustomerDTO>();
            CustomerManager objCustomerManager = new CustomerManager(connectionString);
            ResponseDTO customerResponse = objCustomerManager.GetAllCustomers(GetSessionTanantID());

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

        public List<CustomerTransactionDTO> GetCustomerTransactions(string tenantID, string customerID)
        {
            List<CustomerTransactionDTO> lstCustomerTransactions = new List<CustomerTransactionDTO>();
            CustomerManager objCustomerManager = new CustomerManager(connectionString);
            ResponseDTO objResponseCustomerTransactions = objCustomerManager.GetCustomerTransactions(tenantID, customerID);

            if(objResponseCustomerTransactions != null && objResponseCustomerTransactions.DataList != null)
            {
                lstCustomerTransactions = objResponseCustomerTransactions.DataList as List<CustomerTransactionDTO>;
            }

            return lstCustomerTransactions;
        }

        [HttpPost]
        public ActionResult Save(CustomerDTO model)
        {
            CustomerManager objCustomerManager = new CustomerManager(connectionString);

            string errorMessage = validateCustomer(model);

            if (string.IsNullOrEmpty(errorMessage))
            {
                ResponseDTO responseDTO = new ResponseDTO();
                model.ActiveStatus = true;
                model.CurrentTenantId = GetSessionTanantID();

                if (String.IsNullOrEmpty(model.CustomerId))
                {
                    model.CreatedDate = DateTime.UtcNow;
                    model.CreatedBy = GetSessionTanantName();
                    responseDTO = objCustomerManager.Save(model);
                }
                else
                {
                    model.ModifiedBy = GetSessionTanantName();
                    responseDTO = objCustomerManager.Update(model);
                }
               
                if (responseDTO != null)
                {
                    if (responseDTO.Status)
                    {
                        return Redirect("Index");
                    }
                    else
                    {
                        TempData[Constants.KeyErrorMessage] = responseDTO.Message ;
                    }
                }
                else
                {
                    TempData[Constants.KeyErrorMessage] = Constants.ErrorOpps;
                }              
            }
            else
            {
                TempData[Constants.KeyErrorMessage] = errorMessage;
            
            }
            return View("Add", model);
        }

        #region PrivateMethods

        private string validateCustomer(CustomerDTO model)
        {
            string errorMessage = string.Empty;

            if (model == null)
            {
                errorMessage = string.Format(Constants.ErrorEmptyField, "details");
            }
            if (string.IsNullOrEmpty(model.LoginId) && string.IsNullOrEmpty(model.CustomerId))
            {
                errorMessage = string.Format(Constants.ErrorEmptyField, "Username");
            }
            else if (string.IsNullOrEmpty(model.PassKey) && string.IsNullOrEmpty(model.CustomerId))
            {
                errorMessage = string.Format(Constants.ErrorEmptyField, "Password");
            }
            else if (string.IsNullOrEmpty(model.CustomerName))
            {
                errorMessage = string.Format(Constants.ErrorEmptyField, "Customer Name");
            }
            else if (string.IsNullOrEmpty(model.MobileNumber) && string.IsNullOrEmpty(model.CustomerId))
            {
                errorMessage = string.Format(Constants.ErrorEmptyField, "Mobile Number");
            }
            else if (string.IsNullOrEmpty(model.EmailId) && string.IsNullOrEmpty(model.CustomerId))
            {
                errorMessage = string.Format(Constants.ErrorEmptyField, "Email Id");
            }
            else if (string.IsNullOrEmpty(model.Locality))
            {
                errorMessage = string.Format(Constants.ErrorEmptyField, "Locality");
            }
            else if (string.IsNullOrEmpty(model.Address))
            {
                errorMessage = string.Format(Constants.ErrorEmptyField, "Address");
            }

            return errorMessage;
        }
        #endregion
    }


}