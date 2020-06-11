using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace liulaoc.Net.Http
{
    public enum RespondsResult
    {
        Running,
        Fail,
        Succ,
    }

    public class HttpResponds
    {
        public string data;
        public RespondsResult Result;
        public string token;
    }
}