using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System;

public static class JsonHelper
{
    public static T DeserializeObject<T>(string json) where T:class,new()
    {
        T t = null;
        try
        {
            t = JsonConvert.DeserializeObject<T>(json);
        }
        catch(Exception ex)
        {
            t = new T();
        }
        return t;   
    }
    public static string SerializeObject<T>(T t)
    {
        string result = "";
        try 
        {
            result = JsonConvert.SerializeObject(t);
        }
        catch(Exception ex)
        {
            result = null;
        }
        return result;
    }
}
