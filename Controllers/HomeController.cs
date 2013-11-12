using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using demoGrid.GridHelper;
using demoGrid.Models;

namespace demoGrid.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Welcome to ASP.NET MVC!";

            return View();
        }

        public ActionResult EditPopUp()
        {
            return PartialView();
        }


        [HttpGet]
        public ActionResult GetAccountList(GridSettings gridSettings)
        {
            // create json data
            int totalRecords = 1000;
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;

            List<Account> list = new List<Account>();
            for (int iX = startRow; iX < endRow; iX++)
            {
                Account account = new Account();
                account.AccountNumber = string.Format("Account# {0}", iX);
                account.AccountText = "Line# " + iX.ToString();
                account.AccountDropdown = ((Convert.ToSingle(iX) % 2 == 0) ? "USA" : "England");
                account.AccountDate = DateTime.Now.ToShortDateString();
                account.AccountType = ((Convert.ToSingle(iX) % 2 == 0) ? "Yes" : "No");
                account.AccountBalance = "12345678.90";
                list.Add(account);
            }

            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (
                    from Account item in list
                    select new
                    {
                        id = item.Id,
                        cell = new object[]
                        {
                            item.AccountNumber,
                            item.AccountText,
                            item.AccountDropdown,
                            item.AccountDate,
                            item.AccountType,
                            item.AccountBalance
                        }
                    }
                ).ToArray()
            };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public class Account
        {
            public int Id { get; set; }
            public string AccountNumber { get; set; }
            public string AccountText { get; set; }
            public string AccountDropdown { get; set; }
            public string AccountDate { get; set; }
            public string AccountType { get; set; }
            public string AccountBalance { get; set; }
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
