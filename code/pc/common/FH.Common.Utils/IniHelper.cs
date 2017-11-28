using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;

namespace FH.Common.Utils
{

    public class IniHelper
    {
        private string strFileName = ""; //INI文件名
        private string strFilePath = "";//获取INI文件路径

        public IniHelper()
        {
            strFileName = "app.ini"; //INI文件名
            //方法1：获取INI文件路径
            strFilePath = Directory.GetCurrentDirectory() + "\\config\\" + strFileName;
            //方法2：获取INI文件路径
            //strFilePath = Path.GetFullPath(".\\") + strFileName;
        }

        public IniHelper(string FileName)
        {
            strFileName = FileName; //INI文件名
            //获取INI文件路径
            strFilePath = Directory.GetCurrentDirectory() + "\\config\\" + strFileName;
        }

        public IniHelper(string FullPath, string FileName)
        {
            strFileName = FileName; //INI文件名
            strFilePath = FullPath + "\\" + strFileName;//获取INI文件路径
        }

        /// <summary>
        /// 写入INI文件
        /// </summary>
        /// <param name="section">节点名称[如[TypeName]]</param>
        /// <param name="key">键</param>
        /// <param name="val">值</param>
        /// <param name="filepath">文件路径</param>
        /// <returns></returns>
        [DllImport("kernel32")]
        public static extern long WritePrivateProfileString(string section, string key, string val, string filepath);

        /// <summary>
        /// 写入
        /// </summary>
        /// <param name="sectionName">section 节点名称</param>
        /// <param name="key">key 值</param>
        /// <param name="value">value 值</param>
        public void Write(string sectionName, string key, string value)
        {
            try
            {
                //根据INI文件名设置要写入INI文件的节点名称
                //此处的节点名称完全可以根据实际需要进行配置
                strFileName = Path.GetFileNameWithoutExtension(strFilePath);
                IniHelper.WritePrivateProfileString(sectionName, key, value, strFilePath);
            }
            catch
            {
                throw new Exception("配置文件不存在或权限不足导致无法写入");
            }
        }

        /// <summary>
        /// 写入默认节点"FileConfig"下的相关数据
        /// </summary>
        /// <param name="key">key 值</param>
        /// <param name="value">value 值</param>
        public void Write(string key, string value)
        {
            // section 节点名称使用默认值："SysConfig"
            Write("SysConfig", key, value);
        }

        /// <summary>
        /// 读取INI文件
        /// </summary>
        /// <param name="section">节点名称</param>
        /// <param name="key">键</param>
        /// <param name="def">值</param>
        /// <param name="retval">stringbulider对象</param>
        /// <param name="size">字节大小</param>
        /// <param name="filePath">文件路径</param>
        /// <returns></returns>
        [DllImport("kernel32")]
        public static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retval, int size, string filePath);

        /// <summary>
        /// 读取
        /// </summary>
        /// <param name="sectionName">section 节点名称</param>
        /// <param name="key">key 值</param>
        /// <returns>value 值</returns>
        public string Read(string sectionName, string key)
        {
            if (File.Exists(strFilePath)) //读取时先要判读INI文件是否存在
            {
                strFileName = Path.GetFileNameWithoutExtension(strFilePath);
                //return ContentValue(strFileName, key);
                StringBuilder outValue = new StringBuilder(1024);
                IniHelper.GetPrivateProfileString(sectionName, key, "", outValue, 1024, strFilePath);
                return outValue.ToString();
            }
            else
            {
                throw new Exception("配置文件不存在");
            }
        }

        /// <summary>
        /// 读取默认节点"FileConfig"下的相关数据
        /// </summary>
        /// <param name="key">key 值</param>
        /// <returns>value 值</returns>
        public string Read(string key)
        {
            // section 节点名称使用默认值："SysConfig"
            return Read("SysConfig", key);
        }

    }
}