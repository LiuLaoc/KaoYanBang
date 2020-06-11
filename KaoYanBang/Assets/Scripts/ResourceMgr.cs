using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceMgr : TMonoSingleton<ResourceMgr>,IInitializable
{
    public Dictionary<string, Sprite> SubjectSprites;
    public void Init()
    {
        //加载Sprite
        var allSpriets = Resources.LoadAll<Sprite>("Sprites");
        foreach (var sprite in allSpriets)
        {
            SubjectSprites.Add(sprite.name,sprite);
        }
    }

}
