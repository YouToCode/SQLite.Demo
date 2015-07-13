using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;

namespace HCLUtility
{
    /// <summary>
    /// 文件上传类
    /// </summary>
    public class FileUp
    {
        public FileUp()
        { }

        /// <summary>
        /// 转换为字节数组
        /// </summary>
        /// <param name="filename">文件名</param>
        /// <returns>字节数组</returns>
        public byte[] GetBinaryFile(string filename)
        {
            if (File.Exists(filename))
            {
                FileStream Fsm = null;
                try
                {
                    Fsm = File.OpenRead(filename);
                    return this.ConvertStreamToByteBuffer(Fsm);
                }
                catch
                {
                    return new byte[0];
                }
                finally
                {
                    Fsm.Close();
                }
            }
            else
            {
                return new byte[0];
            }
        }

        /// <summary>
        /// 流转化为字节数组
        /// </summary>
        /// <param name="theStream">流</param>
        /// <returns>字节数组</returns>
        public byte[] ConvertStreamToByteBuffer(System.IO.Stream theStream)
        {
            int bi;
            MemoryStream tempStream = new System.IO.MemoryStream();
            try
            {
                while ((bi = theStream.ReadByte()) != -1)
                {
                    tempStream.WriteByte(((byte)bi));
                }
                return tempStream.ToArray();
            }
            catch
            {
                return new byte[0];
            }
            finally
            {
                tempStream.Close();
            }
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="PosPhotoUpload">控件</param>
        /// <param name="saveFileName">保存的文件名</param>
        /// <param name="imagePath">保存的文件路径</param>
        public string FileSc(FileUpload PosPhotoUpload, string saveFileName, string imagePath)
        {
            string state = "";
            if (PosPhotoUpload.HasFile)
            {
                if (PosPhotoUpload.PostedFile.ContentLength / 1024 < 10240)
                {
                    string MimeType = PosPhotoUpload.PostedFile.ContentType;
                    if (String.Equals(MimeType, "image/gif") || String.Equals(MimeType, "image/pjpeg"))
                    {
                        string extFileString = System.IO.Path.GetExtension(PosPhotoUpload.PostedFile.FileName);
                        PosPhotoUpload.PostedFile.SaveAs(HttpContext.Current.Server.MapPath(imagePath));
                    }
                    else
                    {
                        state = "上传文件类型不正确";
                    }
                }
                else
                {
                    state = "上传文件不能大于10M";
                }
            }
            else
            {
                state = "没有上传文件";
            }
            return state;
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="binData">字节数组</param>
        /// <param name="fileName">文件名</param>
        /// <param name="fileType">文件类型</param>
        //-------------------调用----------------------
        //byte[] by = GetBinaryFile("E:\\Hello.txt");
        //this.SaveFile(by,"Hello",".txt");
        //---------------------------------------------
        public void SaveFile(byte[] binData, string fileName, string fileType)
        {
            FileStream fileStream = null;
            MemoryStream m = new MemoryStream(binData);
            try
            {
                string savePath = HttpContext.Current.Server.MapPath("~/File/");
                if (!Directory.Exists(savePath))
                {
                    Directory.CreateDirectory(savePath);
                }
                string File = savePath + fileName + fileType;
                fileStream = new FileStream(File, FileMode.Create);
                m.WriteTo(fileStream);
            }
            finally
            {
                m.Close();
                fileStream.Close();
            }
        }


        /// <summary>
        /// 上传文件保存按年月日\[返回文件路径|文件名]
        /// </summary>
        /// <param name="sFileSourcePath">源文件路径</param>
        /// <param name="sFileTargetPath">保存目录</param>
        /// <param name="checkFileExt">文件格式</param>
        /// <returns>返回文件路径|文件名</returns>
        public static string CreatUpFilePath(string sFileSourcePath, string sFileTargetPath, string checkFileExt)
        {
            string sOut = null;
            //获取要保存的文件信息
            FileInfo file = new FileInfo(sFileSourcePath);
            //获得文件扩展名
            string fileNameExt = file.Extension;

            //验证合法的文件
            if (CheckFileExt(fileNameExt, checkFileExt))
            {
                //生成将要保存的随机文件名
                string fileName = GetFileName() + fileNameExt;
                //检查保存的路径 是否有/结尾
                if (sFileSourcePath.EndsWith("/") == false) sFileSourcePath = sFileSourcePath + "/";

                //按日期归类保存
                string datePath = DateTime.Now.ToString("yyyyMM") + "/" + DateTime.Now.ToString("dd") + "/";
                if (true)
                {
                    sFileTargetPath += datePath;
                }

                //获得要保存的文件路径
                string serverFileName = sFileTargetPath + fileName;
                //物理完整路径                    
                string toFileFullPath = HttpContext.Current.Server.MapPath(sFileTargetPath);

                //检查是否有该路径  没有就创建
                CreatdDirectory(toFileFullPath);


                // sOut = serverFileName + "|" + toFileFullPath + fileName;
                sOut = sFileTargetPath + "|" + fileName;
            }
            else
            {
                sOut = "文件格式非法";
            }
            return sOut;
        }
        private static void CreatdDirectory(string sPath)
        {
            //检查是否有该路径  没有就创建
            if (!Directory.Exists(sPath))
            {
                Directory.CreateDirectory(sPath);
            }
        }
        /// </summary>
        /// <param name="_fileExt"></param>
        /// <returns></returns>
        private static bool CheckFileExt(string inputFileExt, string checkFileExt)
        {
            string[] allowExt = checkFileExt.ToLower().Split('|');
            for (int i = 0; i < allowExt.Length; i++)
            {
                if (allowExt[i].ToLower() == inputFileExt.ToLower()) { return true; }
            }
            return false;

        }
        private static string GetFileName()
        {
            Random rd = new Random();
            StringBuilder serial = new StringBuilder();
            serial.Append(DateTime.Now.ToString("yyyyMMddHHmmss"));
            serial.Append(rd.Next(100000, 999999).ToString());
            return serial.ToString();

        }

        /// <summary>
        /// 用户webAPI保存文件路径生成 、路径/年月/日
        /// </summary>
        /// <param name="sFileTargetPath"></param>
        /// <returns></returns>
        public static string ApiCreatdDirectory(string sFileTargetPath)
        {
            //按日期归类保存
            string datePath = DateTime.Now.ToString("yyyyMM") + "/" + DateTime.Now.ToString("dd") + "/";
            sFileTargetPath += datePath;
            if (!Directory.Exists(sFileTargetPath))
            {
                Directory.CreateDirectory(sFileTargetPath);
            }
            return sFileTargetPath;
        }
        /// <summary>
        /// 通过api接口上传图片 multipart/form-date
        /// </summary>
        /// <param name="http">http:..com</param>
        /// <param name="uri">uri /api/mobile/up</param>
        /// <param name="image">文件路径file.FileName</param>
        /// <param name="filename">重名后的名称</param>
        /// <param name="pathurl">保存路径</param> 
        /// <returns>返回上传状态|文件路径，错误返回Error|</returns>
        public static string ApiUpFile(string http, string uri, string image, string filename,string pathurl)
        {
            string sReturn = "";
            try
            {
                using (var client = new HttpClient())
                using (var content = new MultipartFormDataContent())
                {
                    // Make sure to change API address
                    client.BaseAddress = new Uri(http);
                    // Add first file content 
                    var fileContent = new ByteArrayContent(System.IO.File.ReadAllBytes(image));
                    fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                    {
                        FileName = filename
                    };
                    content.Add(fileContent);

                    var result = client.PostAsync(uri, content).Result;
                   
                    sReturn = result.StatusCode.ToString() + "|" + pathurl + DateTime.Now.ToString("yyyyMM") + "/" + DateTime.Now.ToString("dd") + "/" + filename;
                }
            }
            catch (Exception)
            {
                sReturn = "Error|";
            }
            return sReturn;
        }
    }
}