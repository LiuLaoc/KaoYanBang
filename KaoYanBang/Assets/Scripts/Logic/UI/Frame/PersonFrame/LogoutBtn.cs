using liulaoc.UI.Base;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogoutBtn : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            NetDataManager.Instance.user = null;
            UIMgr.Instance.RemoveFrame();
            UIMgr.Instance.RemoveFrame();
        });
    }
}
