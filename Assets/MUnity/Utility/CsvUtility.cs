using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.IO;
using System.Text.RegularExpressions;
using System.Text;

namespace MUnity.Utility
{
    public class CsvUtility
    {
        /// <summary>
        /// //获取 csv
        /// </summary>
        public static Csv GetCsv(string filename)
        {
            try
            {
                string context = null;
                List<List<string>> dataList = null;
                if (!filename.Contains(":"))
                {
                    TextAsset asset = Resources.Load<TextAsset>(filename);
                    if (asset == null) throw new ArgumentException("Filepath error!");
                    context = asset.ToString();
                    string[] data = Regex.Split(context, "\r\n");
                    if (data.Length < 2) throw new ArgumentException("Not a valid csv file!");
                    dataList = new List<List<string>>();
                    foreach (string str in data)
                    {
                        dataList.Add(new List<string>(str.Split(',')));
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(filename)) throw new ArgumentException("Filename can't be null or empty!");
                    if (!File.Exists(filename)) throw new ArgumentException("File not exist!");
                    using (StreamReader sr = new StreamReader(new FileStream(filename, FileMode.Open), Encoding.UTF8))
                    {
                        string line = "";
                        dataList = new List<List<string>>();
                        while ((line = sr.ReadLine()) != null)
                        {
                            List<string> list = new List<string>(line.Split(','));
                            dataList.Add(list);
                        }
                    }
                }
                Csv myCsv = new Csv(dataList);
                return myCsv;
            }
            catch (Exception e)
            {
                Debug.LogError(filename + "Unknown Exception！" + e.ToString());
                return null;
            }
        }

        public static List<T> GetList<T>(Csv csv)
        {
            List<string> keys = new List<string>(csv.keys.ToArray());
            List<T> list = new List<T>();
            for (int i = 0; i < keys.Count; i++)
            {
                string id = keys[i];
                T t = GetObject<T>(csv, id);
                list.Add(t);
            }
            return list;
        }

        public static T GetObject<T>(Csv csv, string id)
        {
            T t = System.Activator.CreateInstance<T>();
            Type type = typeof(T);
            FieldInfo[] fields = type.GetFields();
            foreach (FieldInfo field in fields)
            {
                if (field.FieldType.Equals(typeof(string)))
                {
                    field.SetValue(t, csv.Get<string>(id, field.Name));
                }
                else if (field.FieldType.Equals(typeof(int)))
                {
                    field.SetValue(t, csv.Get<int>(id, field.Name));
                }
                else if (field.FieldType.Equals(typeof(float)))
                {
                    field.SetValue(t, csv.Get<float>(id, field.Name));
                }
                else if (field.FieldType.Equals(typeof(double)))
                {
                    field.SetValue(t, csv.Get<double>(id, field.Name));
                }
                else if (field.FieldType.Equals(typeof(bool)))
                {
                    field.SetValue(t, csv.Get<bool>(id, field.Name));
                }
            }
            return t;
        }
    }

    public class Csv
    {
        public List<string> keys;

        private List<List<string>> m_data;

        public Dictionary<string, string> typeDict = new Dictionary<string, string>();

        public Csv(List<List<string>> data)
        {
            typeDict.Add("bool", "Boolean");
            typeDict.Add("double", "Double");
            typeDict.Add("int", "Int32");
            typeDict.Add("string", "String");
            typeDict.Add("float", "Single");
            this.m_data = data;
            if (!this.m_data[1][0].Trim().Equals("string")) throw new ArgumentException("ids isn't string");
            this.keys = new List<string>();
            for(int i=0;i<this.m_data.Count;i++)
            {
                this.keys.Add(this.m_data[i][0]);
            }
        }

        public T Get<T>(string id,string name)
        {
            if (!this.keys.Contains(id)) throw new ArgumentException(string.Format("id:{0} is not exist!",id));
            if (!this.m_data[0].Contains(name)) throw new ArgumentException(string.Format("name:{0} is not exist!", name));
            int row = this.keys.FindIndex(x => x == id);
            int col = this.m_data[0].FindIndex(x => x == name);
            string typeName = this.m_data[1][col];
            if(!typeDict.ContainsKey(typeName)) throw new ArgumentException(string.Format("typename:{0} typename is not exist!", typeName));
            if (!typeof(T).Name.Equals(typeDict[typeName])) throw new ArgumentException(string.Format("name:{0} type is not correct!", name));
            List<string> values = this.m_data[row];
            string result = values[col];
            if (typeof(T).Equals(typeof(string)))
            {
                object obj = (object)result;
                return (T)obj;
            }
            else if (typeof(T).Equals(typeof(int)))
            {
                object obj = int.Parse(result);
                return (T)obj;
            }
            else if (typeof(T).Equals(typeof(float)))
            {
                object obj = float.Parse(result);
                return (T)obj;
            }
            else if (typeof(T).Equals(typeof(double)))
            {
                object obj = double.Parse(result);
                return (T)obj;
            }
            else if (typeof(T).Equals(typeof(bool)))
            {
                if (!result.Trim().Equals("0") && !result.Trim().Equals("1")) throw new ArgumentException(string.Format("row:{0},col{1} 不是有效值,应为0或1！",row,col));
                object obj = result.Trim().Equals("0")? false : true;
                return (T)obj;
            }
            else
            {
                throw new ArgumentException(string.Format("Unknown Exception！"));
            }
        }
    }
}
