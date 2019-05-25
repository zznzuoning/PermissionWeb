using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Script.Serialization;
using System.Data;
using System.Text.RegularExpressions;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Text;

/// <summary>
/// JSON帮助类
/// </summary>
namespace Common
{
    public class JsonHelper
    {
        /// <summary>
        /// 对象转JSON
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns>JSON格式的字符串</returns>
        public static string ObjectToJSON(object obj)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            try
            {
                return jss.Serialize(obj);
            }
            catch (Exception ex)
            {

                throw new Exception("JSONHelper.ObjectToJSON(): " + ex.Message);
            }
        }

        /// <summary>
        /// 数据表转键值对集合
        /// 把DataTable转成 List集合, 存每一行
        /// 集合中放的是键值对字典,存每一列
        /// </summary>
        /// <param name="dt">数据表</param>
        /// <returns>哈希表数组</returns>
        public static List<Dictionary<string, object>> DataTableToList(DataTable dt)
        {
            List<Dictionary<string, object>> list
                 = new List<Dictionary<string, object>>();

            foreach (DataRow dr in dt.Rows)
            {
                Dictionary<string, object> dic = new Dictionary<string, object>();
                foreach (DataColumn dc in dt.Columns)
                {
                    if (dc.DataType == Type.GetType("System.DateTime") && !string.IsNullOrEmpty(dr[dc.ColumnName].ToString()))
                    {
                        dic.Add(dc.ColumnName, ((System.DateTime)dr[dc.ColumnName]).AddHours(8));//解决反序列化日期少了8小时的问题，原因：JavaScriptSerializer类在反序列化日期时,把序列化里的日期当做北京时间看待，把日期减了8，返回为：GMT日期
                    }
                    else
                    {
                        dic.Add(dc.ColumnName, dr[dc.ColumnName]);
                    }
                }
                list.Add(dic);
            }
            return list;
        }

        /// <summary>
        /// 数据集转键值对数组字典
        /// </summary>
        /// <param name="dataSet">数据集</param>
        /// <returns>键值对数组字典</returns>
        public static Dictionary<string, List<Dictionary<string, object>>> DataSetToDic(DataSet ds)
        {
            Dictionary<string, List<Dictionary<string, object>>> result = new Dictionary<string, List<Dictionary<string, object>>>();

            foreach (DataTable dt in ds.Tables)
                result.Add(dt.TableName, DataTableToList(dt));

            return result;
        }

        /// <summary>
        /// 数据表转JSON【DateTime:类型加了8小时】
        /// </summary>
        /// <param name="dataTable">数据表</param>
        /// <returns>JSON字符串</returns>
        public static string DataTableToJSON(DataTable dt)
        {
            return ObjectToJSON(DataTableToList(dt));
        }

        /// <summary>
        /// 数据表转JSON;【DateTime:类型没有加了8小时】
        /// </summary>
        /// <param name="dataTable">数据表</param>
        /// <returns>JSON字符串</returns>
        public static string DataTableToJSONStr(DataTable dt)
        {
            List<Dictionary<string, object>> list
                 = new List<Dictionary<string, object>>();

            foreach (DataRow dr in dt.Rows)
            {
                Dictionary<string, object> dic = new Dictionary<string, object>();
                foreach (DataColumn dc in dt.Columns)
                {
                    dic.Add(dc.ColumnName, dr[dc.ColumnName]);
                }
                list.Add(dic);
            }
            return ObjectToJSON(list);
        }

        /// <summary>
        /// JSON文本转对象,泛型方法
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="jsonText">JSON文本</param>
        /// <returns>指定类型的对象</returns>
        public static T JSONToObject<T>(string jsonText)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            try
            {
                return jss.Deserialize<T>(jsonText);
                //return JsonConvert.DeserializeObject<T>(jsonText);
            }
            catch (Exception ex)
            {
                throw new Exception("JSONHelper.JSONToObject(): " + ex.Message);
            }
        }

        /// <summary>
        /// 将JSON文本转换为数据表数据
        /// </summary>
        /// <param name="jsonText">JSON文本</param>
        /// <returns>数据表字典</returns>
        public static Dictionary<string, List<Dictionary<string, object>>> TablesDataFromJSON(string jsonText)
        {
            return JSONToObject<Dictionary<string, List<Dictionary<string, object>>>>(jsonText);
        }

        /// <summary>
        /// 将JSON文本转换成数据行
        /// </summary>
        /// <param name="jsonText">JSON文本</param>
        /// <returns>数据行的字典</returns>
        public static Dictionary<string, object> DataRowFromJSON(string jsonText)
        {
            return JSONToObject<Dictionary<string, object>>(jsonText);
        }

        /// <summary>
        /// 将string对象反序列化为list对象
        /// </summary>
        /// <typeparam name="T">转化的结果实体</typeparam>
        /// <param name="strJson">源数据DataTable</param>
        /// <returns></returns>
        public static List<T> JSONStringToList<T>(string strJson)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            List<T> objList = serializer.Deserialize<List<T>>(strJson);
            return objList;
        }

        /// <summary>
        /// 将DataTable反序列化为实体集合
        /// </summary>
        /// <typeparam name="T">转化的结果实体</typeparam>
        /// <param name="table">源数据DataTable</param>
        /// <returns></returns>
        public static List<T> DataTableStringToList<T>(DataTable table)
        {
            string strJson = DataTableToJSON(table);
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            serializer.MaxJsonLength = Int32.MaxValue;
            List<T> objList = serializer.Deserialize<List<T>>(strJson);
            return objList;
        }

        /// <summary>
        /// JSON序列化
        /// </summary>
        public static string JsonSerializer<T>(T t)
        {
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
            MemoryStream ms = new MemoryStream();
            ser.WriteObject(ms, t);
            string jsonString = Encoding.UTF8.GetString(ms.ToArray());
            ms.Close();
            return jsonString;
        }

        /// <summary>
        /// 将DataTable序列化为实体
        /// </summary>
        /// <typeparam name="T">将要转为的实体目标</typeparam>
        /// <param name="dt">源TataTable数据</param>
        /// <returns></returns>
        public static T DataTableToEntity<T>(DataTable dt)
        {
            return JsonDeserialize<T>(DataTableToJSON(dt).Replace("[", "").Replace("]", ""));
        }

        /// <summary>
        /// JSON反序列化
        /// </summary>
        public static T JsonDeserialize<T>(string jsonString)
        {
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));
            T obj = (T)ser.ReadObject(ms);
            return obj;
        }
    }
}
