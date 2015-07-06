using SQLite.Demo.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
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


            return View();
        }
        [HttpPost]
        public ActionResult Index(FormCollection form)
        {
            string name = form["name"];
            Random rd = new Random();
            DateTime dt =DateTime.Now;//DateTime.Parse(form["createdate"].ToString());
            string strSQL =string.Format("INSERT INTO demo VALUES({0},'{1}','{2}')","null", name, dt);
            int result=SQLiteHelper2.ExecuteQuery(strSQL,CommandType.Text);
            string message = "失败";
            if (result > 0)
            {
                message = "成功";
            }
            ViewBag.Message = message;
            return View();
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
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
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
    }
}
