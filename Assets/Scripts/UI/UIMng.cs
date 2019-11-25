using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMng : Singleton<UIMng>
{
    //保存所有实例化面板的游戏物体身上的BasePanel组件
    private Dictionary<UIPanelType, BasePanel> panelDict = new Dictionary<UIPanelType, BasePanel>();
    private Stack<BasePanel> panelStack;

    //找到画布的transform
    private Transform canvasTransform;

    /// <summary>
    /// load所有UIPanel
    /// </summary>
    public void Start()
    {
        canvasTransform = GameObject.Find("Canvas").transform;
    }

    /// <summary>
    /// 把某个页面入栈，  把某个页面显示在界面上
    /// </summary>
    public void PushPanel(UIPanelType panelType, string itemName)
    {
        if (panelStack == null)
            panelStack = new Stack<BasePanel>();

        //判断一下栈里面是否有页面
        if (panelStack.Count > 0)
        {
            BasePanel toppanel = panelStack.Peek();
            toppanel.OnPause();
        }
        BasePanel panel = GetPanel(panelType, itemName);
        panel.OnEnter();
        panelStack.Push(panel);
    }
    /// <summary>
    /// 出栈 ，把页面从界面上移除
    /// </summary>
    public void PopPanel()
    {
        if (panelStack == null)
            panelStack = new Stack<BasePanel>();

        if (panelStack.Count <= 0) return;

        //关闭栈顶页面的显示
        BasePanel topPanel = panelStack.Pop();
        topPanel.OnExit();

        if (panelStack.Count <= 0) return;
        BasePanel topPanel2 = panelStack.Peek();
        topPanel2.OnResume();

    }
    /// <summary>
    /// 根据面板类型 得到实例化的面板
    /// </summary>
    /// <returns></returns>
    public BasePanel GetPanel(UIPanelType panelTpe, string itemName)
    {
        if(panelDict == null)
        {
            panelDict = new Dictionary<UIPanelType, BasePanel>();
        }

        BasePanel panel;
        panelDict.TryGetValue(panelTpe, out panel); //TODO

        GameObject newPanel;
        if (panel == null)
        {
            if(canvasTransform == null) canvasTransform = GameObject.Find("Canvas").transform;

            GameObject panelPrefab = Resources.Load<GameObject>(CsvMng.Instance.pathData[panelTpe.ToString()]["Path"]);
            newPanel = GameObject.Instantiate(panelPrefab) as GameObject;
            newPanel.transform.SetParent(canvasTransform);  //TODO

            panelDict.Add(panelTpe, newPanel.GetComponent<BasePanel>());
            panel = newPanel.GetComponent<BasePanel>();
        }

        if(panelTpe == UIPanelType.ConfirmPanel)
        {
            ConfirmPanel g = panel.GetComponent<ConfirmPanel>();
            g.itemName = itemName;
        }
        else if(panelTpe == UIPanelType.InfoPanel)
        {
            InfoPanel g = panel.GetComponent<InfoPanel>();
            g.itemName = itemName;
        }
        else if (panelTpe == UIPanelType.CollegePanel)
        {
            CollegePanel g = panel.GetComponent<CollegePanel>();
            g.itemName = itemName;
        }

        return panel;
    }
}