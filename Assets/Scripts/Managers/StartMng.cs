using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMng : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        CsvMng.Instance.Start();
        CsvMng.Instance.Awake();
        // 判断玩家是否已选择过书院
        if (PlayerInfo.Instance.LoadByJson())
        {
            SceneManager.LoadScene("MainGame");
        }
        else
        {
            UIMng.Instance.PushPanel(UIPanelType.ChoosePanel, "");
            //SceneManager.LoadScene("Choose");
        }
        //var college = PlayerPrefs.GetString("College");
        //if(college == "")
        //{
        //    SceneManager.LoadScene("Choose");
        //}
        //else
        //{
        //    SceneManager.LoadScene("Choose");
        //    //SceneManager.LoadScene("MainGame");
        //}
    }
}
