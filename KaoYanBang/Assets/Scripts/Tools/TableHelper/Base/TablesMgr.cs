using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using UnityEngine;

/// <summary>
/// 表格中数据在内存中常驻
/// </summary>
public class TablesMgr : TMonoSingleton<TablesMgr>, IInitializable
{
    public Dictionary<string, ATable> AllTablesData = new Dictionary<string, ATable>();//缓存，表格名-表格内容
    public void Init()
    {
        AllTablesData = new Dictionary<string, ATable>();
        foreach (var path in TablesInfo.Instance.tablePaths)
        {
            var str = path.Split('\\');
            var tableName = str[str.Length - 1];
            var table = LoadTableFromFile(path, tableName);
            AllTablesData.Add(tableName + "Table", table);
        }
    }
    ATable LoadTableFromFile(string path, string className)
    {
        var lines = Resources.Load<TextAsset>(path).text.Split('\n');
        // 获取参数类型
        var types = lines[1].Split(',');
        types[types.Length - 1] = Regex.Replace(types[types.Length - 1], "\r", "");
        // 反射创建表对象
        var t = Type.GetType(className + "Table");
        var tableObj = Activator.CreateInstance(t);

        foreach (var line in lines.Skip(4))
        {
            if (string.IsNullOrEmpty(line)) continue;

            var datas = line.Split(',');
            var keys = new List<string>();

            //反射获取AddRecord方法
            var method = t.GetMethod("AddRecord", BindingFlags.NonPublic | BindingFlags.Instance);
            object[] args = new object[datas.Length];

            //解析配置表参数
            for (var i = 0; i < datas.Length; i++)
            {
                args[i] = ParseParam(types[i], datas[i]);
            }
            method.Invoke(tableObj, args);
        }
        return tableObj as ATable;
    }
    object ParseParam(string type, string data)
    {
        data = Regex.Replace(data, "\r", "");
        type = type.ToUpper();
        switch (type)
        {
            case "STRING":
                data = Regex.Replace(data, "\"", "");
                return data;
            case "INT":
                return int.Parse(data);
            case "FLOAT":
                return float.Parse(data);
            case "DOUBLE":
                return double.Parse(data);
            case "INT[]":
                data = Regex.Replace(data, "\"", "");
                var x = data.Split(';');
                int[] result = new int[x.Length];
                for (var i = 0; i < x.Length; i++)
                {
                    result[i] = int.Parse(x[i]);
                }
                return result;
            case "INT[][]":
                data = Regex.Replace(data, "\"", "");
                var y1 = data.Split('-');
                int[,] results = new int[y1.Length, y1.Length];
                string[] y2;
                for (var i = 0; i < y1.Length; i++)
                {
                    y2 = y1[i].Split(';');
                    for (var j = 0; j < y1.Length; j++)
                    {
                        results[i, j] = int.Parse(y2[j]);
                    }
                }
                return results;
            default:
                break;
        }
        return null;
    }

    /// <summary>
    /// 获取表格
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public T GetTable<T>() where T : ATable
    {
        var tableName = typeof(T).ToString();
        if (!AllTablesData.ContainsKey(tableName))
        {
            return null;
        }
        return AllTablesData[tableName] as T;
    }

}