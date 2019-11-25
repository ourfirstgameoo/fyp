using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolInfo
{
    //缓存队列
    public Dictionary<GameObject, PoolObjectInfo> queue = new Dictionary<GameObject, PoolObjectInfo>();
    
    //取得可用对象
    public bool TryGetObject(PoolInfo poolInfo, out KeyValuePair<GameObject, PoolObjectInfo> objPair)
    {
        if (poolInfo.queue.Count > 0)
        {
            foreach (KeyValuePair<GameObject, PoolObjectInfo> pair in poolInfo.queue)
            {
                GameObject go = pair.Key;
                PoolObjectInfo info = pair.Value;

                if (info.canUse)
                {
                    objPair = pair;
                    return true;
                }
            }
        }
        objPair = new KeyValuePair<GameObject, PoolObjectInfo>();
        return false;
    }

    void Update()
    {

    }
}
