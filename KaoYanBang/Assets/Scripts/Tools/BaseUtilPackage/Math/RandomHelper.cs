using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;

/// <summary>
/// 随机数生成静态类
/// </summary>
public static class RandomHelper
{
    /// <summary>
    /// 返回0-n的随机数(n等概率)
    /// </summary>
    /// <param name="n"></param>
    public static int Random(int n)
    {
        int index = n;
        using (RNGCryptoServiceProvider rngServiceProvider = new RNGCryptoServiceProvider())
        {
            byte[] randomBytes = new byte[4];
            rngServiceProvider.GetBytes(randomBytes);
            int result = BitConverter.ToInt32(randomBytes, 0);
            int[] judgeArray = new int[n];
            int max = int.MaxValue;
            int min = int.MinValue;
            int clip = max / n * 2;
            for (int i = 0; i < n; i++)
            {
                judgeArray[i] = min + (i + 1) * clip;
                if (result < judgeArray[i])
                {
                    index = i;
                    break;
                }
            }
        }
        return index;
    }
    /// <summary>
    /// 返回0-n的随机数(n非等概率)
    /// </summary>
    /// <param name="n"></param>
    /// <param name="pList"></param>
    public static int Random(List<float> pList)
    {
        int n = pList.Count;
        int index = n;
        using (RNGCryptoServiceProvider rngServiceProvider = new RNGCryptoServiceProvider())
        {
            byte[] randomBytes = new byte[4];
            rngServiceProvider.GetBytes(randomBytes);
            int result = BitConverter.ToInt32(randomBytes, 0);
            int[] judgeArray = new int[n];
            int max = int.MaxValue;
            int min = int.MinValue;
            int clip = max / n * 2;
            for (int i = 0; i < n; i++)
            {
                int newValue = (int)(max * 2 * pList[i]);//根据概率算出来新区间
                judgeArray[i] = min + newValue;
                min += newValue;
                if (result < judgeArray[i])
                {
                    index = i;
                    break;
                }
            }
        }
        return index;
    }

    /// <summary>
    ///  用来判断是否随机成功
    /// </summary>
    /// <param name="pro">概率</param>
    /// <returns>是否随机成功</returns>
    public static bool RandomSucc(float pro)
    {
        int result = GetRandom();
        float left = 1f;
        float right = int.MaxValue * pro * 2f;
        return result >= left && result <= right;
    }
    private static int GetRandom()
    {
        using (RNGCryptoServiceProvider rngServiceProvider = new RNGCryptoServiceProvider())
        {
            byte[] randomBytes = new byte[4];
            rngServiceProvider.GetBytes(randomBytes);
            int result = BitConverter.ToInt32(randomBytes, 0);
            return result;
        }
    }
}