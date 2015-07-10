

using Newtonsoft.Json;
using SQLite.Demo.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;


namespace SQLite.Demo.Controllers
{
    public class SQLiteDeController : Controller
    {
        //
        // GET: /SQLiteDe/
        public ActionResult Index()
        {

            HCLUtility.MyJsonResultMessageEntity jms = new HCLUtility.MyJsonResultMessageEntity();
            jms.Message = "成功";
            jms.IsSuccess = true;
            //ViewBag.Message = JsonConvert.SerializeObject(jms);
            #region MyRegion
            
           
            //string strSQL = string.Format("INSERT INTO  customers VALUES({0},'{1}','{2}','{3}','{4}','{5}',{6})", 8, "Joe", "上海", "潜在客户", DateTime.Now, "admin", 9);
            //string message = "失败";
            //try
            //{
            //    int i = SQLiteHelper2.ExecuteQuery(strSQL, CommandType.Text);
            //    if (i > 0)
            //    {
            //        message = "成功";
            //    }
            //}
            //catch (Exception ex)
            //{
            //    message = ex.ToString();
            //}
           
            //ViewBag.Message = message + "";

           //  SQLiteConnection conn = null;  
  
           //string dbPath = "Data Source ="+Server.MapPath("App_Data/test.db");  
           //conn = new SQLiteConnection(dbPath);//创建数据库实例，指定文件位置  
           //conn.Open();//打开数据库，若文件不存在会自动创建  
  
           // string sql = "CREATE TABLE IF NOT EXISTS student(id integer, name varchar(20), sex varchar(2));";//建表语句  
           // SQLiteCommand cmdCreateTable = new SQLiteCommand(sql, conn);  
           //cmdCreateTable.ExecuteNonQuery();//如果表不存在，创建数据表  
  
           //SQLiteCommand cmdInsert = new SQLiteCommand(conn);  
           // cmdInsert.CommandText = "INSERT INTO student VALUES(1, '小红', '男')";//插入几条数据  
           // cmdInsert.ExecuteNonQuery();  
           // cmdInsert.CommandText = "INSERT INTO student VALUES(2, '小李', '女')";  
           // cmdInsert.ExecuteNonQuery();  
           // cmdInsert.CommandText = "INSERT INTO student VALUES(3, '小明', '男')";  
           // cmdInsert.ExecuteNonQuery();  

            //conn.Close();  
            #endregion

            DataSet ds = SQLiteHelper2.ExecuteDataset("select id,name,createdate from demo order by id desc", CommandType.Text);
            return View(ds);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(FormCollection form)
        {
            
            string name = form["name"];
            //SQLite date函数datetime('now','localtime')  当前的本地时间
            string strSQL =string.Format("INSERT INTO demo VALUES({0},'{1}',{2})","null", name,"datetime('now','localtime')");
            int result=SQLiteHelper2.ExecuteQuery(strSQL,CommandType.Text);
            string message = "失败";
            if (result > 0)
            {
                message = "成功";
            }
            ViewBag.Message = message;
            DataSet ds = SQLiteHelper2.ExecuteDataset("select id,name,createdate from demo order by createdate desc", CommandType.Text);
            return View(ds);
        }
        //
        // GET: /SQLiteDe/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /SQLiteDe/Create
        public ActionResult Create()
        {
           
            return View();
        }

        //
        // POST: /SQLiteDe/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            string file = collection["fileup"];
            HttpPostedFileBase image =Request.Files["fileup"];
            string spath = "";
            if (image != null && image.ContentLength > 0)
            {
                spath = HCLUtility.FileUp.CreatUpFilePath(image.FileName, "/upload/", ".jpg|.png|.gif");
                //  spath = spath.Split('|')[0] + spath.Split('|')[1];
                //image.SaveAs(Server.MapPath(spath));

                //System.Web.HttpContext.Current.Request.ApplicationPath+
                //ViewBag.Message = spath + "<>" + image.FileName + "<><>" + System.IO.File.ReadAllBytes(image.FileName);
                // return View();
                try
                {
                    ViewBag.Message = HCLUtility.FileUp.ApiUpFile("http://192.168.88.148:7080", "/api/mobile/post", image.FileName, spath.Split('|')[1],"/Upload/Api/");
                    #region
                    
                    //using (var client = new HttpClient())
                    //using (var content = new MultipartFormDataContent())
                    //{
                    //    // Make sure to change API address
                    //    client.BaseAddress = new Uri("http://192.168.88.148:7080");

                    //    // Add first file content 
                    //    var fileContent = new ByteArrayContent(System.IO.File.ReadAllBytes(image.FileName));
                    //    fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                    //    {
                    //        FileName = spath.Split('|')[1]
                    //    };

                    //    //// Add Second file content
                    //    //var fileContent2 = new ByteArrayContent(System.IO.File.ReadAllBytes(@"c:\Users\aisadmin\Desktop\Sample.txt"));
                    //    //fileContent2.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                    //    //{
                    //    //    FileName = "Sample.txt"
                    //    //};

                    //    content.Add(fileContent);
                    //    // content.Add(fileContent2);

                    //    // Make a call to Web API
                    //    var result = client.PostAsync("/api/mobile/post", content).Result;

                    //    //Console.WriteLine(result.StatusCode);
                    //    //Console.ReadLine();
                    //    ViewBag.Message = image.FileName + "<*>" + result.StatusCode + " <**> ";
                    //}
                    #endregion
                    return View();
                }
                catch (Exception ex)
                {
                    ViewBag.Message = image.FileName + "<+**+>" + ex.Message + "<+*+>" + spath.Split('|')[1];
                    return View();
                }
            }
            else
            {
                ViewBag.Message ="请选择上传文件";
                return View();
            }
        }

        //
        // GET: /SQLiteDe/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /SQLiteDe/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /SQLiteDe/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /SQLiteDe/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult DesDemo()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DesDemo(FormCollection  form)
        {
            string txt = form["txt"];
            string ddl = form["ddl"];

            string message = txt + ddl;
            if (ddl == "1")
                message = HCLUtility.DESEncrypt.Encrypt(txt);
            else
                message = HCLUtility.DESEncrypt.Decrypt(txt) == null ? "解密失败" : HCLUtility.DESEncrypt.Decrypt(txt);
            ViewBag.Message = message;
            return View();
        }
         [HttpPost]
        public ActionResult JqueryPost()
        {
            string txt = Request.Form["txt"];
            string ddl = Request.Form["ddl"];
            string message ="";
            if (ddl == "1")
                message = HCLUtility.DESEncrypt.Encrypt(txt);
            else
                message = HCLUtility.DESEncrypt.Decrypt(txt) == null ? "解密失败" : HCLUtility.DESEncrypt.Decrypt(txt);
           // string json = "{txt:'"+txt+"',ddl:'" + ddl + "',des:'" + message + "}";
            Dictionary<string, string> dt = new Dictionary<string, string>();
            dt.Add("txt", txt);
            dt.Add("ddl", ddl);
            dt.Add("des", message);

            return Json(JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
        }
    }
}
