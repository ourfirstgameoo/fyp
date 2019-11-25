using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolObjectInfo
{
    public string name;

    //缓存时间
    public float cacheTime = 0.0f;

    //缓存物体类型
    public PoolObjectType type;

    //可以复用
    public bool canUse = true;

    //重置时间
    public float resetTime = 0.0f;
}
