using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace RevitStarter
{
    public class XmlHelper
    {
        /// <summary>
        /// 获取指定路径节点中所有子节点的值
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="xPath"></param>
        /// <returns></returns>
        public static List<string> GetListValues(string filePath, string xPath)
        {
            List<string> list = new List<string>();
            try
            {
                XmlDocument xmldoc = new XmlDocument();//实例化一个xml文件类
                xmldoc.Load(filePath);
                XmlNode xn = xmldoc.SelectSingleNode(xPath);
                XmlElement newNode = (XmlElement)xn;//要读取的节点转换为元素                      
                foreach (XmlNode tempNode in newNode)
                {
                    XmlNode Node = tempNode.ChildNodes[0];
                    string nodeName = Node.InnerText;//取子节点的值
                    string u = nodeName;
                    list.Add(u);
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return list;
        }

        /// <summary>
        /// 获取指定节点的指定属性
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="xPath"></param>
        /// <param name="attName">属性集合</param>
        /// <returns></returns>
        public static List<string> GetListAttribute(string filePath, string xPath, params string[] attName)
        {
            List<string> list = new List<string>();
            try
            {
                XmlDocument xmldoc = new XmlDocument();//实例化一个xml文件类
                xmldoc.Load(filePath);
                XmlNode xn = xmldoc.SelectSingleNode(xPath);
                XmlElement newNode = (XmlElement)xn;//要读取的节点转换为元素                   
                for (int i = 0; i < attName.Length; i++)
                {
                    string stratt = newNode.GetAttribute(attName[i]);
                    list.Add(stratt);
                }
            }
            catch (Exception ex)
            {

                return null;
            }
            return list;
        }

        /// <summary>
        /// 获取指定节点的指定属性
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="xPath"></param>
        /// <param name="attName">属性集合</param>
        /// <returns></returns>
        public static List<string> GetAllListAttribute(string filePath, string xPath, params string[] attName)
        {
            List<string> list = new List<string>();
            try
            {
                XmlDocument xmldoc = new XmlDocument();//实例化一个xml文件类
                xmldoc.Load(filePath);
                var xns = xmldoc.SelectNodes(xPath);
                foreach (var xn in xns)
                {
                    XmlElement newNode = (XmlElement)xn;//要读取的节点转换为元素                   
                    for (int i = 0; i < attName.Length; i++)
                    {
                        string stratt = newNode.GetAttribute(attName[i]);
                        list.Add(stratt);
                    }
                }

            }
            catch (Exception ex)
            {

                return null;
            }
            return list;
        }

        /// <summary>
        /// 反序列化xml
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static T Deserialize<T>(string filePath) where T : class
        {
            FileStream fs = File.Open(filePath, FileMode.Open);
            using (StreamReader sr = new StreamReader(fs, Encoding.UTF8))
            {
                XmlSerializer xz = new XmlSerializer(typeof(T));
                T result = (T)xz.Deserialize(sr);
                return result;
            }
        }

        
    }
}
