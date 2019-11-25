using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemPanel : BasePanel
{
    private CanvasGroup canvasGroup;



    void Start()
    {
        if (canvasGroup == null) canvasGroup = GetComponent<CanvasGroup>();

        //string path;
        //GameObject obj;
        //foreach(var item in PlayerInfo.Instance.save.itemInfo)
        //{
        //    path = CsvMng.Instance.pathData[item.Key]["Path"];
        //    obj = Resources.Load<GameObject>(path);
        //    Instantiate(obj, content.transform);
        //}


    }

    public override void OnEnter()
    {
        if (canvasGroup == null) canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;

        //transform.localScale = Vector3.zero;
        //transform.DOScale(1, .5f);
    }

    public override void OnExit()
    {
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;

        //transform.DOScale(0, .5f).OnComplete(() => canvasGroup.alpha = 0);
    }

    public void OnClosePanel()
    {
        UIMng.Instance.PopPanel();
    }
}
