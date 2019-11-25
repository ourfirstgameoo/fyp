using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PoolMng : Singleton<PoolMng>//需要写成单例
{
    //用于清理缓存的时间记录
    private float totalTime;
    
    //允许缓存的时间
    private float cacheTime = 300.0f;
    
    //缓存object map
    private Dictionary<string, PoolInfo> poolDic = new Dictionary<string, PoolInfo>();

    //删除队列
    private List<GameObject> mDestoryPoolGameObjects = new List<GameObject>();

    // Start is called before the first frame update
    public void Start()
    {
        poolDic.Clear();
        mDestoryPoolGameObjects.Clear();
    }

    //获取缓存物体
    public GameObject GetGO(string res)
    {
        //有效性检查
        if (null == res)
        {
            return null;
        }
        //查找对应pool，如果没有缓存
        PoolInfo poolInfo = null;
        KeyValuePair<GameObject, PoolObjectInfo> pair;

        bool hasObjectPool = poolDic.TryGetValue(res, out poolInfo);

        //Debug.Log("here!!");
        //Debug.Log(hasObjectPool);
        // 如果没有该物体的对象池，则直接生成
        if (!hasObjectPool)
        {
            string path = CsvMng.Instance.pathData[res]["Path"];
            GameObject obj = Resources.Load<GameObject>(path);
            return GameObject.Instantiate(obj) as GameObject;
        }
        else if(!poolDic[res].TryGetObject(poolInfo, out pair)) // 有对象池，但池内无合适对象
        {
            string path = CsvMng.Instance.pathData[res]["Path"];
            GameObject obj = Resources.Load<GameObject>(path);
            return GameObject.Instantiate(obj) as GameObject;
        }

        //出队列数据
        GameObject go = pair.Key;
        PoolObjectInfo info = pair.Value;

        poolInfo.queue.Remove(go);

        //使有效
        EnablePoolGameObject(go, info);

        //返回缓存Gameobjec
        return go;
    }
    public void EnablePoolGameObject(GameObject go, PoolObjectInfo info)
    {
        //特效Enable          
        //if (info.type == PoolObjectType.Tower)
        //{
        //    go.SetActive(true);
        //    go.transform.parent = null;
        //}
        //info.cacheTime = 0.0f;
        if (go == null)
            return;
        go.SetActive(true);
        info.cacheTime = 300.0f;
    }
    public void ReleaseGO(string res, GameObject go, PoolObjectType type)
    {
        //获取缓存节点,设置为不可见位置
        //if (objectsPool == null)
        //{
        //    objectsPool = new GameObject("ObjectPool");
        //    objectsPool.AddComponent<Canvas>();
        //    objectsPool.transform.localPosition = new Vector3(0, -5000, 0);
        //}
        if (null == res || null == go)
        {
            return;
        }
        PoolInfo poolInfo = null;
        //没有创建 
        if (!poolDic.TryGetValue(res, out poolInfo))
        {
            //Debug.Log("创建新对象池！！" + res);
            poolInfo = new PoolInfo();
            poolDic.Add(res, poolInfo);
        }

        PoolObjectInfo poolGameObjInfo = new PoolObjectInfo();
        poolGameObjInfo.type = type;
        poolGameObjInfo.name = res;

        //无效缓存物体
        //DisablePoolGameObject(go, poolGameObjInfo);
        go.SetActive(false);

        //保存缓存GameObject,会传入相同的go, 有隐患          
        poolInfo.queue[go] = poolGameObjInfo;
    }
    // Update is called once per frame
    public void Update()
    {
        //每隔0.1更新一次
        totalTime += Time.deltaTime;
        if (totalTime <= 0.1f)
        {
            return;
        }
        else
        {
            totalTime = 0;
        }

        float deltaTime = Time.deltaTime;

        //遍历数据
        foreach (PoolInfo poolInfo in poolDic.Values)
        {
            //死亡列表
            mDestoryPoolGameObjects.Clear();

            foreach (KeyValuePair<GameObject, PoolObjectInfo> pair in poolInfo.queue)
            {
                GameObject obj = pair.Key;
                PoolObjectInfo info = pair.Value;

                info.cacheTime += deltaTime;

                float mAllCachTime = cacheTime;

                //POT_UITip,缓存3600秒
                //if (info.type == PoolObjectType.Tower)
                //    mAllCachTime = 3600;

                //缓存时间到
                if (info.cacheTime >= mAllCachTime)
                {
                    mDestoryPoolGameObjects.Add(obj);
                }

                //拖尾重置计时
                if (!info.canUse)
                {
                    info.resetTime += deltaTime;

                    if (info.resetTime > 1.0f)
                    {
                        info.resetTime = .0f;
                        info.canUse = true;

                        obj.SetActive(false);
                    }
                }
            }

            //移除
            for (int k = 0; k < mDestoryPoolGameObjects.Count; k++)
            {
                GameObject obj = mDestoryPoolGameObjects[k];
                GameObject.DestroyImmediate(obj);

                poolInfo.queue.Remove(obj);
            }
        }
    }
}

