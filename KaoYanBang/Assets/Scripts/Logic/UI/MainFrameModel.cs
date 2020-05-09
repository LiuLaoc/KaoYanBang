using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainFrameModel : TMonoSingleton<MainFrameModel>,IInitializable
{
    public int ToggleIndex { get; set; }
    public void Init()
    {
        ToggleIndex = 4;
    }
}
