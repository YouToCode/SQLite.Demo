using HCLUtility;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace SQLite.Demo.Controllers
{
    public class MobileController : ApiController
    {
        // GET api/mobile
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/mobile/5
        public HttpResponseMessage Get(int id)
        {
            MyJsonResultMessageEntity jms = new MyJsonResultMessageEntity();
            jms.Message = "成功";
            jms.IsSuccess = true;
            return JsonHelper.toJson(jms);
        }

        // POST api/mobile
        public async  Task<HttpResponseMessage> Post()
        {

            // Check whether the POST operation is MultiPart?
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }
            
            // Prepare CustomMultipartFormDataStreamProvider in which our multipart form
            // data will be loaded.
            string fileSaveLocation = HCLUtility.FileUp.ApiCreatdDirectory(HttpContext.Current.Server.MapPath("/Upload/Api/"));
            var provider = new CustomMultipartFormDataStreamProvider(fileSaveLocation);
            List<string> files = new List<string>();

            try
            {
                // Read all contents of multipart message into CustomMultipartFormDataStreamProvider.
                await Request.Content.ReadAsMultipartAsync(provider);

                foreach (MultipartFileData file in provider.FileData)
                {
                    files.Add(Path.GetFileName(file.LocalFileName));
                }

                // Send OK Response along with saved file names to the client.
                return Request.CreateResponse(HttpStatusCode.OK, files);
            }
            catch (System.Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }

            #region

            //// 检查是否是 multipart/form-data
            //if (!Request.Content.IsMimeMultipartContent("form-data"))
            //    throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            //HttpResponseMessage response = null;

            //try
            //{
            //    string sPath = HttpContext.Current.Server.MapPath("/Upload/");
            //    // 设置上传目录
            //    var provider = new MultipartFormDataStreamProvider(sPath);
            //    // 接收数据，并保存文件
            //    var bodyparts = await Request.Content.ReadAsMultipartAsync(provider);

            //    // This illustrates how to get the file names.  
            //    foreach (MultipartFileData file in provider.FileData)
            //    {//接收文件  
            //        string sjname=file.Headers.ContentDisposition.FileName;//获取上传文件实际的文件名  
            //        string spp="Server file path: " + file.LocalFileName;//获取上传文件在服务上默认的文件名  
            //    }//TODO:这样做直接就将文件存到了指定目录下，暂时不知道如何实现只接收文件数据流但并不保存至服务器的目录下，由开发自行指定如何存储，比如通过服务存到图片服务器  
               



            //    response = Request.CreateResponse(HttpStatusCode.Accepted);


            //    //foreach (var file in provider.FileData)
            //    //{
            //    //    FileInfo fileInfo = new FileInfo(file.LocalFileName);
            //    //    sb.Append(string.Format("Uploaded file: {0} ({1} bytes)\n", fileInfo.Name, fileInfo.Length));
            //    // }

            //}
            //catch
            //{
            //    throw new HttpResponseException(HttpStatusCode.BadRequest);
            //}
            //return response;
            #endregion

        }
        public class CustomMultipartFormDataStreamProvider : MultipartFormDataStreamProvider
        {
            public CustomMultipartFormDataStreamProvider(string path) : base(path) { }

            public override string GetLocalFileName(HttpContentHeaders headers)
            {
                return headers.ContentDisposition.FileName.Replace("\"", string.Empty);
            }
        }


        // PUT api/mobile/5
        public void Put(int id, [FromBody]string value)
        {
           
        }

        // DELETE api/mobile/5
        public void Delete(int id)
        {
        }
    }
}
