﻿using System;
using System.Reflection;

/// <summary>
/// 全局单例，请自己实现构造函数
/// </summary>
/// <typeparam name="T"></typeparam>
public class TSingleton<T> where T : class
{
    protected static T _instance = null;

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                var ctors = typeof(T).GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic);
                var ctor = Array.Find(ctors, c => c.GetParameters().Length == 0);
                if (ctor == null)
                {
                    throw new Exception("[Base]: Non-Public Constructor() not found! in " + typeof(T));
                }
                _instance = ctor.Invoke(null) as T;
            }
            return _instance;
        }
    }
}