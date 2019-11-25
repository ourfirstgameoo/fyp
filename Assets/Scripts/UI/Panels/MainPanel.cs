using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MainPanel : BasePanel
{
    private CanvasGroup canvasGroup;
    public GameObject ARCamera;
    public Image image;

    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        //image.overrideSprite = Resources.Load(CsvMng.Instance.pathData[PlayerInfo.Instance.save.college]["Path"], typeof(Sprite)) as Sprite;
    }

    public override void OnPause()
    {
        canvasGroup.blocksRaycasts = false;//当弹出新的面板的时候，让主菜单面板 不再和鼠标交互
    }
    public override void OnResume()
    {
        canvasGroup.blocksRaycasts = true;
    }

    public void OnPushPanel(string panelTypeString)
    {
        UIPanelType panelType = (UIPanelType)System.Enum.Parse(typeof(UIPanelType), panelTypeString);
        UIMng.Instance.PushPanel(panelType, null);
    }

    public void OnChangeCamera()
    {
        //GameMng.Instance.ShowMap(false);
        //GameObject.Find("Map").gameObject.SetActive(false);
        SceneManager.LoadScene("ARgathering");
    }

    public void ShowCollegeInfo()
    {
        UIMng.Instance.PushPanel(UIPanelType.CollegePanel, PlayerInfo.Instance.save.college);
    }

    public void SwitchMode(int n)
    {
        Camera.main.GetComponent<cameraCtl>().switchMode(n);
    }



}
