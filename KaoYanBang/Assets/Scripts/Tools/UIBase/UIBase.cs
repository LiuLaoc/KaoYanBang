using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace liulaoc.UI.Base
{
    public class UIBase : MonoBehaviour
    {
        public void Show()
        {
            gameObject.SetActive(true);
        }
        public void Hide()
        {
            gameObject.SetActive(false);
        }
        public void Destroy()
        {
            Destroy(gameObject);
        }
    }
}