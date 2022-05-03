using FundaClear.Business;
using FundaClearApp.Models;
using FundaClearApp.Utilities;
using Loyalty.DTO;
using Loyalty.ServiceLibrary;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace FundaClearApp.Controllers
{
    public class HomeController : Controller
    {
        public string connectionString;

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            connectionString = ConnectionHelper.GetConnectionString();
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session = null;
            return RedirectToAction("Index");
        }


        public IActionResult Dashboard()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(TenantDTO model)
        {
            if (model != null && !string.IsNullOrEmpty(model.LoginId) && !string.IsNullOrEmpty(model.PassKey))
            {
                TenantManager objTenantManager = new TenantManager(connectionString);
                
                ResponseDTO objResponseDTO = objTenantManager.CheckLogin(model.LoginId, model.PassKey);

                if (objResponseDTO.Status)
                {
                    TenantDTO objTenantDTO = objResponseDTO.Data as TenantDTO;

                    HttpContext.Session.SetString("TenantId", objTenantDTO.TenantId);
                    HttpContext.Session.SetString("TenantName", objTenantDTO.TenantName);

                    return Redirect("Dashboard");
                }
                else
                {
                    TempData[Constants.KeyErrorMessage] = Constants.ErrorInvalidLoginCredentials;
                }
            }
            else
            {
                TempData[Constants.KeyErrorMessage] = Constants.ErrorEnterUsernamePassword;
            }

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Signup()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Signup(TenantDTO model)
        {
            string errorMessage = ValidationHelper.ValidateSignup(model);

            if (string.IsNullOrEmpty(errorMessage))
            {
                TenantManager objTenantManager = new TenantManager(connectionString);

                model.CreatedBy = model.TenantName;
                model.CreatedDate = DateTime.UtcNow;
                model.ActiveStatus = true;

                ResponseDTO responseDTO = objTenantManager.Save(model);

                if (responseDTO != null)
                {
                    if(responseDTO.Status)
                    {
                        TempData[Constants.KeySuccessMessage] = Constants.SuccessSignup;
                        model = new TenantDTO();
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        TempData[Constants.KeySuccessMessage] = responseDTO.Message;
                    }
                }
                else
                {
                    TempData[Constants.KeySuccessMessage] = Constants.ErrorOpps; 
                }
            }
            else
            {
                TempData[Constants.KeyErrorMessage] = errorMessage;
            }

            return View(model);
        }
    }
}
