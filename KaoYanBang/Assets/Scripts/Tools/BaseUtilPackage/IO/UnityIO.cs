using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class UnityIOHelper:MonoBehaviour
{
    /// <summary>
    /// 流写入
    /// </summary>
    /// <param name="stream"></param>
    /// <param name="path"></param>
    public static void SaveToFile(string stream, string path)
    {
        if (!Directory.Exists(Path.GetDirectoryName(path)))
        {
            Directory.CreateDirectory(Path.GetDirectoryName(path));
        }
        FileInfo file = new FileInfo(path);

        StreamWriter sw = file.CreateText();
        sw.Write(stream);
        sw.Close();
        sw.Dispose();
    }
    /// <summary>
    /// 流读取
    /// </summary>
    /// <param name="stream"></param>
    /// <param name="path"></param>
    /// <param name="callback"></param>
    public static string ReadFromFile(string path)
    {
        if(!Directory.Exists(Path.GetDirectoryName(path)))
        {
            Debug.LogError("不存在路径"+path);
        }
        FileInfo file = new FileInfo(path);
        StreamReader sr = file.OpenText();
        string stream = sr.ReadToEnd();
        sr.Close();
        sr.Dispose();
        return stream;
    }
}
