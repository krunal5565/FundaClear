using System;
using Loyalty.DataLayer;
using Loyalty.DTO;
using Loyalty.ServiceLibrary;
using Loyalty.Shared;
using System.Data.SqlClient;
using FundaClearApp.Utilities;

namespace FundaClear.Business
{
    public static class ValidationHelper
    {
        public static string ValidateSignup(TenantDTO model)
        {
            string errorMessage = string.Empty;

            if (model == null)
            {
                errorMessage = string.Format(Constants.ErrorEmptyField, "details");
            }
            if (string.IsNullOrEmpty(model.LoginId))
            {
                errorMessage = string.Format(Constants.ErrorEmptyField, "Username");
            }
            else if (string.IsNullOrEmpty(model.PassKey))
            {
                errorMessage = string.Format(Constants.ErrorEmptyField, "Password");
            }
            else if (string.IsNullOrEmpty(model.MobileNumber))
            {
                errorMessage = string.Format(Constants.ErrorEmptyField, "Mobile Number");
            }
            else if (string.IsNullOrEmpty(model.EmailId))
            {
                errorMessage = string.Format(Constants.ErrorEmptyField, "Email ID");
            }
            else if (string.IsNullOrEmpty(model.Address))
            {
                errorMessage = string.Format(Constants.ErrorEmptyField, "Address");
            }
            else if (string.IsNullOrEmpty(model.Locality))
            {
                errorMessage = string.Format(Constants.ErrorEmptyField, "Locality");
            }
            else if (string.IsNullOrEmpty(model.ContactPerson))
            {
                errorMessage = string.Format(Constants.ErrorEmptyField, "Contact Person");
            }
            else if (string.IsNullOrEmpty(model.TenantName))
            {
                errorMessage = string.Format(Constants.ErrorEmptyField, "Tenant Name");
            }

            return errorMessage;
        }

        public static string ValidateCustomer(CustomerDTO model)
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
    }
}
