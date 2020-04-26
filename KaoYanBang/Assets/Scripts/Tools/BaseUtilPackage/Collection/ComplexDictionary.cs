using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// (int,int),T 类型字典，第一个int为dbid，第二个int为本地表格id
/// </summary>
/// <typeparam name="T"></typeparam>
public class ComplexDictionary<T> where T : class
{
    private Dictionary<(int, int), T> dataDic = new Dictionary<(int, int), T>();
    public List<int> GetItem1()
    {
        List<int> resList = new List<int>();
        foreach (KeyValuePair<(int, int), T> data in dataDic)
        {
            resList.Add(data.Key.Item1);
        }
        return resList;
    }
    public List<int> GetItem2()
    {
        List<int> resList = new List<int>();
        foreach (KeyValuePair<(int, int), T> data in dataDic)
        {
            resList.Add(data.Key.Item2);
        }
        return resList;
    }
    public bool Add((int, int) key, T t)
    {
        if (dataDic.ContainsKey(key))
        {
            return false;
        }
        dataDic.Add(key, t);
        return true;
    }
    public bool ContainsItem2(int item2)
    {
        var list = GetItem2();
        if (list.Contains(item2))
        {
            return true;
        }
        return false;
    }
    public List<int> GetRestItem2List(List<int> localDb)
    {
        var res = new List<int>();
        var item2List = GetItem2();
        foreach (var localId in localDb)
        {
            if (!item2List.Contains(localId))
            {
                res.Add(localId);
            }
        }
        return res;
    }
    public string Print()
    {
        var list = GetItem2();
        return JsonHelper.SerializeObject(list);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="index">输入0则第二个参数为dbid，1则为本地id</param>
    /// <param name="id"></param>
    public void Remove(int index,int id)
    {
        var shouldRemove = new List<(int, int)>();
        if(index == 0)
        {
            foreach(KeyValuePair<(int,int),T> kvp in dataDic)
            {
                if(kvp.Key.Item1 == id)
                {
                    shouldRemove.Add(kvp.Key);
                }
            }
        }
        else if(index == 1)
        {
            foreach (KeyValuePair<(int, int), T> kvp in dataDic)
            {
                if (kvp.Key.Item2 == id)
                {
                    shouldRemove.Add(kvp.Key);
                }
            }
        }
        if(shouldRemove.Count!=0)
        {
            foreach(var key in shouldRemove)
            {
                if(dataDic.ContainsKey(key))
                {
                    dataDic.Remove(key);
                }
            }
        }
    }
    public void Update((int,int) key,T t)
    {
        dataDic[key] = t;
    }

    /// <summary>
    /// 用于获取本地数据id对应的数据
    /// </summary>
    /// <param name="index">输入0则第二个参数为dbid，1则为本地id</param>
    /// <param name="id"></param>
    /// <returns></returns>
    public T this[int index, int id]
    {
        get
        {
            if (index == 0)
            {
                foreach (KeyValuePair<(int, int), T> kvp in dataDic)
                {
                    if (kvp.Key.Item1 == id)
                    {
                        return dataDic[kvp.Key];
                    }
                }
            }
            else if (index == 1)
            {
                foreach (KeyValuePair<(int, int), T> kvp in dataDic)
                {
                    if (kvp.Key.Item2 == id)
                    {
                        return dataDic[kvp.Key];
                    }
                }
            }
            return null;
        }
    }
    //所有值的集合
    public Dictionary<(int, int), T>.ValueCollection Vales { get { return dataDic.Values; } }

}
