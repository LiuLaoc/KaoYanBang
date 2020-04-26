using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StringExtension
{
    /// <summary>
    /// 任意类型字符串转换
    /// </summary>
    /// <typeparam name="T">类型</typeparam>
    /// <param name="input">输入字符串</param>
    /// <returns></returns>
    public static T Convert<T>(string input)
    {
        try
        {
            var converter = System.ComponentModel.TypeDescriptor.GetConverter(typeof(T));
            if (converter != null)
            {
                // Cast ConvertFromString(string text) : object to (T)
                return (T)converter.ConvertFromString(input);
            }
            return default(T);
        }
        catch (System.NotSupportedException)
        {
            return default(T);
        }
    }
}
