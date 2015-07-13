using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCLUtility
{
    /// <summary>
    /// 文件、文件夹帮助类
    /// </summary>
    public class FileHelper
    {
        #region 文件

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="fileFullPath">文件全路径</param>
        /// <returns>bool 是否删除成功</returns>
        public static bool DeleteFile(string fileFullPath)
        {
            if (File.Exists(fileFullPath))
            {
                if (File.GetAttributes(fileFullPath) == FileAttributes.Normal)
                {
                    File.Delete(fileFullPath);
                }
                else
                {
                    File.SetAttributes(fileFullPath, FileAttributes.Normal);
                    File.Delete(fileFullPath);
                }
                return true;
            }
            return false;
        }

        /// <summary>
        /// 根据传来的文件全路径，获取文件名称部分默认包括扩展名
        /// </summary>
        /// <param name="fileFullPath">文件全路径</param>
        /// <returns>string 文件名称</returns>
        public static string GetFileName(string fileFullPath)
        {
            if (File.Exists(fileFullPath))
            {
                var f = new FileInfo(fileFullPath);
                return f.Name;
            }
            return null;
        }

        /// <summary>
        /// 根据传来的文件全路径，获取文件名称部分
        /// </summary>
        /// <param name="fileFullPath">文件全路径</param>
        /// <param name="includeExtension">是否包括文件扩展名</param>
        /// <returns>string 文件名称</returns>
        public static string GetFileName(string fileFullPath, bool includeExtension)
        {
            if (File.Exists(fileFullPath))
            {
                var f = new FileInfo(fileFullPath);
                if (includeExtension)
                {
                    return f.Name;
                }
                return f.Name.Replace(f.Extension, "");
            }
            return null;
        }

        /// <summary>
        /// 根据传来的文件全路径，获取新的文件名称全路径,一般用作临时保存用
        /// </summary>
        /// <param name="fileFullPath">文件全路径</param>
        /// <returns>string 新的文件全路径名称</returns>
        public static string GetNewFileFullName(string fileFullPath)
        {
            if (File.Exists(fileFullPath))
            {
                var f = new FileInfo(fileFullPath);
                string tempFileName = fileFullPath.Replace(f.Extension, "");
                for (int i = 0; i < 1000; i++)
                {
                    fileFullPath = tempFileName + i.ToString() + f.Extension;
                    if (File.Exists(fileFullPath) == false)
                    {
                        break;
                    }
                }
            }
            return fileFullPath;
        }

        /// <summary>
        /// 根据传来的文件全路径，获取文件扩展名不包括“.”，如“doc”
        /// </summary>
        /// <param name="fileFullPath">文件全路径</param>
        /// <returns>string 文件扩展名</returns>
        public static string GetFileExtension(string fileFullPath)
        {
            if (File.Exists(fileFullPath))
            {
                var f = new FileInfo(fileFullPath);
                return f.Extension;
            }
            return null;
        }

        /// <summary>
        /// 根据传来的文件全路径，外部打开文件，默认用系统注册类型关联软件打开
        /// </summary>
        /// <param name="fileFullPath">文件全路径</param>
        /// <returns>bool 文件名称</returns>
        public static bool OpenFile(string fileFullPath)
        {
            if (File.Exists(fileFullPath))
            {
                Process.Start(fileFullPath);
                return true;
            }
            return false;
        }

        /// <summary>
        /// 根据传来的文件全路径，得到文件大小，规范文件大小称呼，如1ＧＢ以上，单位用ＧＢ，１ＭＢ以上，单位用ＭＢ，１ＭＢ以下，单位用ＫＢ
        /// </summary>
        /// <param name="fileFullPath">文件全路径</param>
        /// <returns>bool 文件大小</returns>
        public static string GetFileSize(string fileFullPath)
        {
            if (File.Exists(fileFullPath))
            {
                var f = new FileInfo(fileFullPath);
                long fl = f.Length;
                if (fl > 1024 * 1024 * 1024)
                {
                    return Convert.ToString(Math.Round((fl + 0.00) / (1024 * 1024 * 1024), 2)) + " GB";
                }
                if (fl > 1024 * 1024)
                {
                    return Convert.ToString(Math.Round((fl + 0.00) / (1024 * 1024), 2)) + " MB";
                }
                return Convert.ToString(Math.Round((fl + 0.00) / 1024, 2)) + " KB";
            }
            return null;
        }

        /// <summary>
        /// 文件转换成二进制，返回二进制数组Byte[]
        /// </summary>
        /// <param name="fileFullPath">文件全路径</param>
        /// <returns>byte[] 包含文件流的二进制数组</returns>
        public static byte[] FileToStreamByte(string fileFullPath)
        {
            byte[] fileData = null;
            if (File.Exists(fileFullPath))
            {
                var fs = new FileStream(fileFullPath, FileMode.Open);
                fileData = new byte[fs.Length];
                fs.Read(fileData, 0, fileData.Length);
                fs.Close();
            }
            return fileData;
        }

        /// <summary>
        /// 二进制数组Byte[]生成文件
        /// </summary>
        /// <param name="createFileFullPath">要生成的文件全路径</param>
        /// <param name="streamByte">要生成文件的二进制 Byte 数组</param>
        /// <returns>bool 是否生成成功</returns>
        public static bool ByteStreamToFile(string createFileFullPath, byte[] streamByte)
        {
            if (File.Exists(createFileFullPath) == false)
            {
                FileStream fs = File.Create(createFileFullPath);
                fs.Write(streamByte, 0, streamByte.Length);
                fs.Close();
                return true;
            }
            return false;
        }

        /// <summary>
        /// 二进制数组Byte[]生成文件，并验证文件是否存在，存在则先删除
        /// </summary>
        /// <param name="createFileFullPath">要生成的文件全路径</param>
        /// <param name="streamByte">要生成文件的二进制 Byte 数组</param>
        /// <param name="fileExistsDelete">同路径文件存在是否先删除</param>
        /// <returns>bool 是否生成成功</returns>
        public static bool ByteStreamToFile(string createFileFullPath, byte[] streamByte, bool fileExistsDelete)
        {
            if (File.Exists(createFileFullPath))
            {
                if (fileExistsDelete && DeleteFile(createFileFullPath) == false)
                {
                    return false;
                }
            }
            FileStream fs = File.Create(createFileFullPath);
            fs.Write(streamByte, 0, streamByte.Length);
            fs.Close();
            return true;
        }

        /// <summary>
        /// 读写文件，并进行匹配文字替换
        /// </summary>
        /// <param name="pathRead">读取路径</param>
        /// <param name="pathWrite">写入路径</param>
        /// <param name="replaceStrings">替换字典</param>
        public static void ReadAndWriteFile(string pathRead, string pathWrite, Dictionary<string, string> replaceStrings)
        {
            var objReader = new StreamReader(pathRead);
            if (File.Exists(pathWrite))
            {
                File.Delete(pathWrite);
            }
            var streamw = new StreamWriter(pathWrite, false, Encoding.GetEncoding("utf-8"));
            var readLine = objReader.ReadToEnd();
            if (replaceStrings != null && replaceStrings.Count > 0)
            {
                foreach (var dicPair in replaceStrings)
                {
                    readLine = readLine.Replace(dicPair.Key, dicPair.Value);
                }
            }
            streamw.WriteLine(readLine);
            objReader.Close();
            streamw.Close();
        }

        /// <summary>
        /// 读取文件
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns>返回值</returns>
        public static string ReadFile(string filePath)
        {
            var objReader = new StreamReader(filePath);
            string readLine = null;
            if (File.Exists(filePath))
            {
                readLine = objReader.ReadToEnd();
            }
            objReader.Close();
            return readLine;
        }

        /// <summary>
        /// 写入文件
        /// </summary>
        /// <param name="pathWrite">写入路径</param>
        /// <param name="content">内容</param>
        public static void WriteFile(string pathWrite, string content)
        {
            if (File.Exists(pathWrite))
            {
                File.Delete(pathWrite);
            }
            var streamw = new StreamWriter(pathWrite, false, Encoding.GetEncoding("utf-8"));
            streamw.WriteLine(content);
            streamw.Close();
        }

        /// <summary>
        /// 读取并附加文本
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="content">内容</param>
        public static void ReadAndAppendFile(string filePath, string content)
        {
            File.AppendAllText(filePath, content, Encoding.GetEncoding("utf-8"));
        }

        /// <summary>
        /// 复制文件
        /// </summary>
        /// <param name="sources">源文件</param>
        /// <param name="dest">目标文件</param>
        public static void CopyFile(string sources, string dest)
        {
            var dinfo = new DirectoryInfo(sources);
            foreach (FileSystemInfo f in dinfo.GetFileSystemInfos())
            {
                var destName = Path.Combine(dest, f.Name);
                if (f is FileInfo)
                {
                    File.Copy(f.FullName, destName, true);
                }
                else
                {
                    Directory.CreateDirectory(destName);
                    CopyFile(f.FullName, destName);
                }
            }
        }

        /// <summary>
        /// 复制文件
        /// </summary>
        /// <param name="sources">源文件</param>
        /// <param name="dest">目标文件</param>
        public static void MoveFile(string sources, string dest)
        {
            var dinfo = new DirectoryInfo(sources);
            foreach (FileSystemInfo f in dinfo.GetFileSystemInfos())
            {
                var destName = Path.Combine(dest, f.Name);
                if (f is FileInfo)
                {
                    File.Move(f.FullName, destName);
                }
                else
                {
                    Directory.CreateDirectory(destName);
                    MoveFile(f.FullName, destName);
                }
            }
        }

        /// <summary>
        /// 检测指定文件是否存在,如果存在则返回true。
        /// </summary>
        /// <param name="filePath">文件的绝对路径</param>
        /// <returns>bool 是否存在文件</returns>
        public static bool IsExistFile(string filePath)
        {
            return File.Exists(filePath);
        }


        #region 创建一个文件
        /// <summary>
        /// 创建一个文件。
        /// </summary>
        /// <param name="filePath">文件的绝对路径</param>
        public static bool CreateFile(string filePath)
        {
            bool isTrue = true;
            try
            {
               
                //如果文件不存在则创建该文件
                if (!IsExistFile(filePath))
                {
                    //创建一个FileInfo对象
                    FileInfo file = new FileInfo(filePath);

                    //创建文件
                    FileStream fs = file.Create();

                    //关闭文件流
                    fs.Close();
                }
                else { isTrue = false; }
            }
            catch (Exception ex)
            {
                //LogHelper.WriteTraceLog(TraceLogLevel.Error, ex.Message);
                isTrue = false;
                throw ex;
            }
            return isTrue;
        }

        /// <summary>
        /// 创建一个文件,并将字节流写入文件。
        /// </summary>
        /// <param name="filePath">文件的绝对路径</param>
        /// <param name="buffer">二进制流数据</param>
        public static void CreateFile(string filePath, byte[] buffer)
        {
            try
            {
                //如果文件不存在则创建该文件
                if (!IsExistFile(filePath))
                {
                    //创建一个FileInfo对象
                    FileInfo file = new FileInfo(filePath);

                    //创建文件
                    FileStream fs = file.Create();

                    //写入二进制流
                    fs.Write(buffer, 0, buffer.Length);

                    //关闭文件流
                    fs.Close();
                }
            }
            catch (Exception ex)
            {
                //LogHelper.WriteTraceLog(TraceLogLevel.Error, ex.Message);
                throw ex;
            }
        }
        #endregion
        #endregion

        #region 文件夹
        
        /// <summary>
        /// 获取指定目录中所有子目录列表,若要搜索嵌套的子目录列表,请使用重载方法.
        /// </summary>
        /// <param name="directoryPath">指定目录的绝对路径</param>
        /// <returns>返回值</returns>
        public static string[] GetDirs(string directoryPath)
        {
            return Directory.GetDirectories(directoryPath);
        }

        /// <summary>
        /// 创建文件夹
        /// </summary>
        /// <param name="destDirectory">指定目录的绝对路径</param>
        public static void CreateDir(string destDirectory)
        {
            if (!string.IsNullOrEmpty(destDirectory) && !Directory.Exists(destDirectory))
            {
                Directory.CreateDirectory(destDirectory);
            }
        }

        /// <summary>
        /// 复制文件夹
        /// </summary>
        /// <param name="strFromDirectory">要复制的文件夹</param>
        /// <param name="strToDirectory">复制到的文件夹</param>
        /// <returns>是否复制成功</returns>
        public static bool CopyDir(string strFromDirectory, string strToDirectory)
        {
            Directory.CreateDirectory(strToDirectory);

            if (!Directory.Exists(strFromDirectory)) return false;

            string[] directories = Directory.GetDirectories(strFromDirectory);

            if (directories.Length > 0)
            {
                foreach (string d in directories)
                {
                    CopyDir(d, strToDirectory + d.Substring(d.LastIndexOf("\\")));
                }
            }
            string[] files = Directory.GetFiles(strFromDirectory);

            if (files.Length > 0)
            {
                foreach (string s in files)
                {
                    File.Copy(s, strToDirectory + s.Substring(s.LastIndexOf("\\")));
                }
            }
            return true;
        }

        /// <summary>
        /// 删除文件夹
        /// </summary>
        /// <param name="dirFullPath">要删除文件夹的全路径</param>
        /// <returns>是否删除成功</returns>
        public static bool DeleteDir(string dirFullPath)
        {
            if (Directory.Exists(dirFullPath))
            {
                Directory.Delete(dirFullPath, true);
            }
            else //文件夹不存在
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 得到当前文件夹中所有文件列表string[]
        /// </summary>
        /// <param name="dirFullPath">要遍历的文件夹全路径</param>
        /// <returns>string[] 文件列表</returns>
        public static string[] GetDirFiles(string dirFullPath)
        {
            string[] fileList;
            if (Directory.Exists(dirFullPath))
            {
                fileList = Directory.GetFiles(dirFullPath, "*.*", SearchOption.TopDirectoryOnly);
            }
            else //文件夹不存在
            {
                return null;
            }
            return fileList;
        }

        /// <summary>
        /// 得到当前文件夹及下级文件夹中所有文件列表string[]
        /// </summary>
        /// <param name="dirFullPath">要遍历的文件夹全路径</param>
        /// <param name="so">查找文件的选项，是否包含子级文件夹</param>
        /// <returns>string[] 文件列表</returns>
        public static string[] GetDirFiles(string dirFullPath, SearchOption so)
        {
            string[] fileList;
            if (Directory.Exists(dirFullPath))
            {
                fileList = Directory.GetFiles(dirFullPath, "*.*", so);
            }
            else //文件夹不存在
            {
                return null;
            }
            return fileList;
        }

        /// <summary>
        /// 得到当前文件夹中指定文件类型［扩展名］文件列表string[]
        /// </summary>
        /// <param name="dirFullPath">要遍历的文件夹全路径</param>
        /// <param name="searchPattern">查找文件的扩展名如“*.*代表所有文件；*.doc代表所有doc文件”</param>
        /// <returns>string[] 文件列表</returns>
        public static string[] GetDirFiles(string dirFullPath, string searchPattern)
        {
            string[] fileList;
            if (Directory.Exists(dirFullPath))
            {
                fileList = Directory.GetFiles(dirFullPath, searchPattern);
            }
            else //文件夹不存在
            {
                return null;
            }
            return fileList;
        }

        /// <summary>
        /// 得到当前文件夹及下级文件夹中指定文件类型［扩展名］文件列表string[]
        /// </summary>
        /// <param name="dirFullPath">要遍历的文件夹全路径</param>
        /// <param name="searchPattern">查找文件的扩展名如“*.*代表所有文件；*.doc代表所有doc文件”</param>
        /// <param name="so">查找文件的选项，是否包含子级文件夹</param>
        /// <returns>string[] 文件列表</returns>
        public static string[] GetDirFiles(string dirFullPath, string searchPattern, SearchOption so)
        {
            string[] fileList;
            if (Directory.Exists(dirFullPath))
            {
                fileList = Directory.GetFiles(dirFullPath, searchPattern, so);
            }
            else //文件夹不存在
            {
                return null;
            }
            return fileList;
        }

        /// <summary>
        /// 确保文件夹被创建
        /// </summary>
        /// <param name="filePath">文件夹全名（含路径）</param>
        public static void AssertDirExist(string filePath)
        {
            var dir = new DirectoryInfo(filePath);
            if (!dir.Exists)
            {
                dir.Create();
            }
        }

        /// <summary>
        /// 检测指定目录是否存在
        /// </summary>
        /// <param name="directoryPath">目录的绝对路径</param>
        /// <returns>bool 是否存在</returns>
        public static bool IsExistDirectory(string directoryPath)
        {
            return Directory.Exists(directoryPath);
        }

        /// <summary>
        /// 检测指定目录是否为空
        /// </summary>
        /// <param name="directoryPath">指定目录的绝对路径</param>
        /// <returns>bool 是否为空</returns>
        public static bool IsEmptyDirectory(string directoryPath)
        {
            //判断是否存在文件
            string[] fileNames = GetFileNames(directoryPath);
            if (fileNames.Length > 0)
            {
                return false;
            }

            //判断是否存在文件夹
            string[] directoryNames = GetDirs(directoryPath);
            if (directoryNames.Length > 0)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// 检测指定目录中是否存在指定的文件,若要搜索子目录请使用重载方法.
        /// </summary>
        /// <param name="directoryPath">指定目录的绝对路径</param>
        /// <param name="searchPattern">模式字符串，"*"代表0或N个字符，"?"代表1个字符。
        /// 范例："Log*.xml"表示搜索所有以Log开头的Xml文件。</param>
        /// <returns>bool 是否包含文件</returns>
        public static bool ContainFile(string directoryPath, string searchPattern)
        {
            //获取指定的文件列表
            string[] fileNames = GetFileNames(directoryPath, searchPattern, false);

            //判断指定文件是否存在
            if (fileNames.Length == 0)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 检测指定目录中是否存在指定的文件
        /// </summary>
        /// <param name="directoryPath">指定目录的绝对路径</param>
        /// <param name="searchPattern">模式字符串，"*"代表0或N个字符，"?"代表1个字符。
        /// 范例："Log*.xml"表示搜索所有以Log开头的Xml文件。</param>
        /// <param name="isSearchChild">是否搜索子目录</param>
        /// <returns>bool 是否包含文件</returns>
        public static bool ContainFile(string directoryPath, string searchPattern, bool isSearchChild)
        {
            //获取指定的文件列表
            string[] fileNames = GetFileNames(directoryPath, searchPattern, true);

            //判断指定文件是否存在
            if (fileNames.Length == 0)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 取当前目录
        /// </summary>
        /// <returns>当前目录名</returns>
        public static string GetCurrentDirectory()
        {
            return Directory.GetCurrentDirectory();
        }

        /// <summary>
        /// 设当前目录
        /// </summary>
        /// <param name="path">目录绝对路径</param>
        public static void SetCurrentDirectory(string path)
        {
            Directory.SetCurrentDirectory(path);
        }

        /// <summary>
        /// 取路径中不充许存在的字符
        /// </summary>
        /// <returns>不充许存在的字符</returns>
        public static char[] GetInvalidPathChars()
        {
            return Path.GetInvalidPathChars();
        }

        /// <summary>
        /// 取系统所有的逻辑驱动器
        /// </summary>
        /// <returns>所有的逻辑驱动器</returns>
        public static DriveInfo[] GetAllDrives()
        {
            return DriveInfo.GetDrives();
        }

        /// <summary>
        /// 获取指定目录中所有文件列表
        /// </summary>
        /// <param name="directoryPath">指定目录的绝对路径</param>
        /// <returns>所有文件列表</returns>
        public static string[] GetFileNames(string directoryPath)
        {
            //如果目录不存在，则抛出异常
            if (!IsExistDirectory(directoryPath))
            {
                throw new FileNotFoundException();
            }

            //获取文件列表
            return Directory.GetFiles(directoryPath);
        }

        /// <summary>
        /// 获取指定目录及子目录中所有文件列表
        /// </summary>
        /// <param name="directoryPath">指定目录的绝对路径</param>
        /// <param name="searchPattern">模式字符串，"*"代表0或N个字符，"?"代表1个字符。
        /// 范例："Log*.xml"表示搜索所有以Log开头的Xml文件。</param>
        /// <param name="isSearchChild">是否搜索子目录</param>
        /// <returns>指定目录及子目录中所有文件列表</returns>
        public static string[] GetFileNames(string directoryPath, string searchPattern, bool isSearchChild)
        {
            //如果目录不存在，则抛出异常
            if (!IsExistDirectory(directoryPath))
            {
                throw new FileNotFoundException();
            }

            if (isSearchChild)
            {
                return Directory.GetFiles(directoryPath, searchPattern, SearchOption.AllDirectories);
            }
            return Directory.GetFiles(directoryPath, searchPattern, SearchOption.TopDirectoryOnly);
        }

        #endregion
    }
}
