using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollegePanel : BasePanel
{
    private CanvasGroup canvasGroup;
    public Image image;
    public Text text;
    public string itemName;

    void Start()
    {
        if (canvasGroup == null) canvasGroup = GetComponent<CanvasGroup>();
    }

    public override void OnEnter()
    {
        if (canvasGroup == null) canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;

        image.overrideSprite = Resources.Load(CsvMng.Instance.pathData[itemName]["Path"], typeof(Sprite)) as Sprite;
        text.text = CsvMng.Instance.pathData[itemName]["Note"];

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
