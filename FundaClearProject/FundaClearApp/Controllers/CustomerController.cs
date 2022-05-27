using FundaClear.Business;
using FundaClearApp.Models;
using FundaClearApp.Utilities;
using Loyalty.DTO;
using Loyalty.ServiceLibrary;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;


namespace FundaClearApp.Controllers
{
    
    public class CustomerController : Controller
    {
        public string connectionString;
        private readonly IConfiguration configuration;
        public CustomerController(IConfiguration iConfiguration)
        {
          //  connectionString = ConnectionHelper.GetConnectionString();
            connectionString = iConfiguration.GetConnectionString("DefaultConnection");
            configuration = iConfiguration;
        }

        public string GetSessionTanantName()
        {
            return HttpContext != null && HttpContext.Session != null ? HttpContext.Session.GetString("TenantName") : "";
        }

        public string GetSessionTanantID()
        {
            return HttpContext != null && HttpContext.Session != null ? HttpContext.Session.GetString("TenantId") : "";
        }

        public ActionResult AddCustomerTransaction(CustomerTransactionDTO model)
        {
            CustomerManager objCustomerManager = new CustomerManager(connectionString);
            model.TenantId = GetSessionTanantID();
            model.CustId = model.CustId;
            model.CreatedBy = GetSessionTanantName();
            model.BillDate = model.BillDate;
            model.ActiveStatus = true;
           
            ResponseDTO customerResponse = objCustomerManager.SaveCustomerTransaction(model);

            string actionName = string.Empty ;

            if (model.IsAddPoint)
            {
                actionName = "CustomerTransactions";
            }
            else
            {
                actionName = "RedemptionTransactions";
            }
            return RedirectToAction(actionName, new { id = model.CustId });
        }

        public ActionResult Index()
        {
            List<CustomerDTO> lstCustomerDTO = APIHelper.GetAllCustomers(GetSessionTanantID(), connectionString);
            return View(lstCustomerDTO);
        }

        [HttpGet]
        public ActionResult Add()
        {
            CustomerModel objCustomerModel = new CustomerModel();
            objCustomerModel.Customer = new CustomerDTO();
            return View("Save", objCustomerModel);
        }

        [HttpPost]
        public bool Delete(string id)
        {
            bool status = false;

            CustomerDTO objCustomerDTO = APIHelper.GetCustomer(GetSessionTanantID(), id, connectionString);
            objCustomerDTO.ActiveStatus = false;

            ResponseDTO objCustResponseDTO = APIHelper.UpdateCustomer(objCustomerDTO, connectionString);
            if (objCustResponseDTO != null)
            {
                status = objCustResponseDTO.Status;
            }

            return status;
        }

        [HttpGet]
        public ActionResult Edit(string id)
        {
            CustomerModel objCustomerModel = new CustomerModel();

            objCustomerModel.Customer = APIHelper.GetCustomer(GetSessionTanantID(), id, connectionString);
            return View("Save", objCustomerModel);
        }

        [HttpGet]
        public ActionResult CustomerTransactions(string id)
        {
            CustomerModel objCustomerModel = GetCustomerTransactions(id, true);

            return View("CustomerTransactions", objCustomerModel);
        }

        [HttpGet]
        public ActionResult RedemptionTransactions(string id)
        {
            CustomerModel objCustomerModel = GetCustomerTransactions(id, false);

            return View("CustomerTransactions", objCustomerModel);
        }

        [HttpPost]
        public ActionResult Save(CustomerDTO model)
        {
            CustomerManager objCustomerManager = new CustomerManager(connectionString);

            string errorMessage = ValidationHelper.ValidateCustomer(model);

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
                        TempData[Constants.KeyErrorMessage] = responseDTO.Message;
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

            CustomerModel objCustomerModel = new CustomerModel();

            if(!string.IsNullOrEmpty(model.CustomerId))
            {
                objCustomerModel.Customer = APIHelper.GetCustomer(GetSessionTanantID(), model.CustomerId, connectionString);
                objCustomerModel.Customer.Locality = model.Locality;
                objCustomerModel.Customer.CustomerName = model.CustomerName;
                objCustomerModel.Customer.Address = model.Address;
            }
            else
            {
                objCustomerModel.Customer = model;
            }

            string viewName = "Save";

            return View(viewName, objCustomerModel);
        }

        public CustomerModel GetCustomerTransactions(string id, bool isAddTransactions)
        {
            CustomerModel objCustomerModel = new CustomerModel();
            objCustomerModel.Customer = APIHelper.GetCustomer(GetSessionTanantID(), id, connectionString);
            objCustomerModel.CustomerTransactions = APIHelper.GetCustomerTransactions(GetSessionTanantID(), id, connectionString);
            objCustomerModel.IsAddPoints = isAddTransactions;

            if (objCustomerModel.CustomerTransactions != null)
            {
                objCustomerModel.CustomerTransactions= objCustomerModel.CustomerTransactions.Where(x => x.IsAddPoint = isAddTransactions).ToList();
            }

            objCustomerModel.CustomerTransaction = new CustomerTransactionDTO { CustId = id, BillDate = DateTime.Now.Date , IsAddPoint  = isAddTransactions};

            return objCustomerModel;
        }

        //[HttpPost]
        //public ActionResult Edit(CustomerDTO model)
        //{
        //    CustomerManager objCustomerManager = new CustomerManager(connectionString);

        //    string errorMessage = ValidationHelper.ValidateCustomer(model);

        //    if (string.IsNullOrEmpty(errorMessage))
        //    {
        //        model.ActiveStatus = true;
        //        model.CurrentTenantId = GetSessionTanantID();

        //        model.ModifiedBy = GetSessionTanantName();
        //        ResponseDTO responseDTO = objCustomerManager.Update(model);

        //        if (responseDTO != null)
        //        {
        //            if (responseDTO.Status)
        //            {
        //                return Redirect("Index");
        //            }
        //            else
        //            {
        //                TempData[Constants.KeyErrorMessage] = responseDTO.Message;
        //            }
        //        }
        //        else
        //        {
        //            TempData[Constants.KeyErrorMessage] = Constants.ErrorOpps;
        //        }
        //    }
        //    else
        //    {
        //        TempData[Constants.KeyErrorMessage] = errorMessage;
        //    }

        //    CustomerModel objCustomerModel = new CustomerModel();
        //    objCustomerModel.Customer = model;
        //    objCustomerModel.CustomerTransactions = APIHelper.GetCustomerTransactions(GetSessionTanantID(), model.CustomerId);
        //    objCustomerModel.CustomerTransaction = new CustomerTransactionDTO { CustId = model.CustomerId, BillDate = DateTime.Now };

        //    return View("Edit", objCustomerModel);
        //}


    }


}