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
        /// <summary>
        /// 监听滑动条变化，第一个为监听对象id，第二个为修改后的值，第三个为角色总的血量
        /// </summary>
        public Action<int, int, int> OnHpChange;

        /// <summary>
        /// 监听能量槽变化，第一个修改后的能量值，第二个为总的需要的能量值
        /// </summary>
        public Action<int, int> OnEnegyChange;

        /// <summary>
        /// 摇杆变化，摇杆移动时，调用角色移动代码
        /// </summary>
        public Action<float, float> OnBarValueChange;

        /// <summary>
        /// 点击技能键，第一个参数角色id，第二个参数技能id（攻击就是技能0
        /// </summary>
        public Action<int, int> OnClickSkill;

    }
}