using MessagePack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace MsgPackPractice.Converter
{
    /// <summary>
    /// object <-> JSON <-> XML の相互変換用の拡張メソッド
    /// </summary>
    public static class ExtensionsClass
    {
        /// <summary>
        /// object -> JSON(string)
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string Object2Json(this object obj)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(obj);
        }

        /// <summary>
        /// JSON(string) -> object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="str"></param>
        /// <returns></returns>
        public static object Json2Object<T>(this string str)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(str);
        }

        /// <summary>
        /// JSON(string) -> XmlDocument
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static XmlDocument Json2XmlDocument(this string str)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeXmlNode(str);
        }

        /// <summary>
        /// JSON(string) -> XDocument
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static XDocument Json2XDocument(this string str)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeXNode(str);
        }

        /// <summary>
        /// XmlDocument -> XDocument
        /// </summary>
        /// <param name="xmldoc"></param>
        /// <returns></returns>
        public static XDocument XmlDocument2XDocument(this XmlDocument xmldoc)
        {
            return XDocument.Load(new XmlNodeReader(xmldoc));
        }

        /// <summary>
        /// object -> XDocument
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static XDocument Object2XDocument(this object obj)
        {
            return Json2XDocument(Object2Json(obj));
        }

        /// <summary>
        /// XDocument -> JSON(string)
        /// </summary>
        /// <param name="xdoc"></param>
        /// <returns></returns>
        public static string XDocumnet2Json(this XDocument xdoc)
        {
            return Newtonsoft.Json.JsonConvert.SerializeXNode(xdoc);
        }

        /// <summary>
        /// XDocument -> object
        /// </summary>
        /// <param name="xdoc"></param>
        /// <returns></returns>
        public static object XDocument2Object<T>(this XDocument xdoc)
        {
            return Json2Object<T>(XDocumnet2Json(xdoc));
        }

        /// <summary>
        /// object -> MessagePack(byte[])
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static byte[] Object2MessagePack<T>(this object obj)
        {
            return MessagePackSerializer.Serialize(obj);
        }

        /// <summary>
        /// MessagePack(byte[]) -> object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static T MessagePack2Object<T>(this byte[] bytes)
        {
            return MessagePackSerializer.Deserialize<T>(bytes);
        }
    }
}
