using HCLUtility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SQLite.Demo.Controllers
{
    public class DemoController : Controller
    {
        //
        // GET: /Demo/
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult CookDemo()
        {
            HttpCookie cook = new HttpCookie("Cook");
            cook.Value = "Demo";
            cook.Expires = DateTime.Now.AddDays(7.0);
            ViewBag.Message = cook.Value + "|" + cook.Name ;
            Response.Cookies.Add(cook);
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CookDemo(FormCollection form)
        {
            string val = form["ddl"];
            string Message = "成功";
            if (val == "1")
                Message = CookieHelper.GetCookieValue("Cook");
            if (val == "2")
                CookieHelper.SetCookie("Coke", "Demo2", DateTime.Now.AddDays(1.0));
            if (val == "3")
                CookieHelper.ClearCookie("Coke");

            ViewBag.Message = Message;
            return View();
        }

        public ActionResult UploadDemo()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UploadDemo(FormCollection form)
        {
            string file = form["file"];
            string fse=FileUp.CreatUpFilePath(file,"/upload/", ".jpg|.png|.gif");
            string msg = "失败";
            try
            {
               // DirFile.CreateFile(Server.MapPath(fse.Split('|')[0] + fse.Split('|')[1]));
              bool isSuc= FileHelper.CreateFile(Server.MapPath(fse.Split('|')[0] + fse.Split('|')[1]));
              if (isSuc)
                  msg = "成功";
            }
            catch(Exception ex)
            {
                msg = "失败"+ex.ToString();
            }
            finally
            {
                ViewBag.Message = msg;
            }
            return View();
        }
	}
}