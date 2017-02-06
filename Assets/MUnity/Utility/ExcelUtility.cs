// ========================================================
// 描 述：ExcelUtility.cs 
// 作 者：郑贤春 
// 时 间：2017/01/07 09:13:19 
// 版 本：5.4.1f1 
// ========================================================
using UnityEngine;
using System.Collections;
using System.IO;
using Excel;
using System.Data;
using System.Collections.Generic;

namespace MUnity.Utility
{
    public class ExcelUtility
    {
        public static ExcelData GetData(string path)
        {
            FileStream stream = File.Open(path, FileMode.Open, FileAccess.Read);
            IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
            DataSet dataSet = excelReader.AsDataSet();
            ExcelData result = new ExcelData(dataSet);
            return result;
        }
    }

    public class ExcelData
    {
        Dictionary<string, Dictionary<string, string>> data = new Dictionary<string, Dictionary<string, string>>();

        public ExcelData(DataSet dataSet)
        {
            int row = dataSet.Tables[0].Rows.Count;
            int column = dataSet.Tables[0].Columns.Count;
            List<string> keys = new List<string>();
            for (int i = 2; i < row; i++)
            {
                string id = null;
                for (int j = 0; j < column; j++)
                {
                    if (i == 3) continue;
                    string value = dataSet.Tables[0].Rows[i][j].ToString();
                    if (i == 2)
                    {
                        keys.Add(value);
                    }
                    if (i > 2 && j == 0)
                    {
                        id = value;
                        data.Add(id, new Dictionary<string, string>());
                    }
                    if (i > 2)
                    {
                        data[id].Add(keys[j], value);
                    }
                }
            }
        }

        public List<string> GetKeys()
        {
            return new List<string>(data.Keys);
        }

        public int GetCount()
        {
            return data.Count;
        }

        public int GetInt(string id, string field)
        {
            return int.Parse(data[id][field]);
        }

        public string GetString(string id, string field)
        {
            return data[id][field];
        }

        public float GetFloat(string id, string field)
        {
            return float.Parse(data[id][field]);
        }

        public Vector3 GetVector3(string id, string field)
        {
            string[] values = data[id][field].Split(',');
            Vector3 vector = new Vector3();
            for (int i = 0; i < 3; i++)
            {
                if (values != null && i < values.Length && !string.IsNullOrEmpty(values[i]))
                {
                    vector[i] = float.Parse(values[i]);
                }
                else
                {
                    vector[i] = 0;
                }
            }
            return vector;
        }
    }
}
