using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace liulaoc.UI.Base
{
    public enum SliderType
    {
        HpListener,

    }
    public class UIMsgCenter : TSingleton<UIMsgCenter>
    {
        private UIMsgCenter()
        {
        }
        public Action<int> SelectSchool;
        public Action<int> SelectSubject;
    }
}