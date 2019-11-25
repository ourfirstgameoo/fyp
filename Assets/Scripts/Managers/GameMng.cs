using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMng : MonoSingleton<GameMng>
{
    public void Awake()
    {
        DontDestroyOnLoad(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        //加载玩家信息，地图信息
        //PlayerInfoLoad.Instance.Start();
        //MapLoad.Instance.Start();
        //Debug.Log(Application.dataPath);
        //PlayerInfoLoad.Instance.LoadByJson();
        CsvMng.Instance.Start();
        UIMng.Instance.Start();
        ItemMng.Instance.Start();
       // Shop.Instance.Start();
        //UIMng.Instance.PushPanel(UIPanelType.MainPanel, null);
    }

    private void OnDestroy()
    {
        Debug.Log("GAME OVER");
        PlayerInfo.Instance.SaveByJson();
    }
}
