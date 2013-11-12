using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using demoGrid.Common;
using System.Threading;

namespace demoGrid.Controllers
{
    public class PopController : Controller
    {
        //
        // GET: /Pop/

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet, FileDownload]
        public FilePathResult DownloadReport(int id)
        {
            return GetReport(id);
        }

        private FilePathResult GetReport(int id)
        {
            //simulate generating the report
            Thread.Sleep(3000);

            return File("~/Report.pdf", "application/pdf", string.Format("Report{0}.pdf", id));
        }

        public ActionResult OpenLogin()
        {
            return PartialView("LoginDialog");
        }

       
        // perform login
        public ActionResult Login(string username, string password)
        {
            return Json(new { Success = true });
        }
        
    }
}
