using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BookPanel : BasePanel
{
    private CanvasGroup canvasGroup;

    private int num = 9;
    public GameObject prefab;
    public GameObject content;

    private List<int> bookInfo = new List<int>();
    private List<GameObject> bookObject = new List<GameObject>();

    private bool hasInit = false;

    void Start()
    {
        if (canvasGroup == null) canvasGroup = GetComponent<CanvasGroup>();
    }

    public override void OnEnter()
    {
        if (canvasGroup == null) canvasGroup = GetComponent<CanvasGroup>();

        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;

        //初始化图鉴
        if (!hasInit)
        {
            InitBook();
            hasInit = true;
        }

        GameObject go;
        for (int i = 1; i <= num; i++)
        {
            if(!bookInfo.Contains(i) && PlayerInfo.Instance.save.bookInfo.Contains(CsvMng.Instance.bookData[i.ToString()]["Name"]))
            {
                bookInfo.Add(i);
                go = bookObject[i - 1];
                go.GetComponent<Image>().overrideSprite = Resources.Load(CsvMng.Instance.bookData[i.ToString()]["Path"], typeof(Sprite)) as Sprite;
                go.GetComponent<Button>().interactable = true;
                go.GetComponentInChildren<Text>().text = "";
            }
        }

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

    private void InitBook()
    {
        GameObject go;
        for (int i = 1; i <= num; i++)
        {
            go = (GameObject)Instantiate(prefab, content.transform);
            go.GetComponent<ShowInfo>().id = i;
            bookObject.Add(go);

            if (PlayerInfo.Instance.save.bookInfo.Contains(CsvMng.Instance.bookData[i.ToString()]["Name"]))
            {
                bookInfo.Add(i);
                go.GetComponent<Image>().overrideSprite = Resources.Load(CsvMng.Instance.bookData[i.ToString()]["Path"], typeof(Sprite)) as Sprite;
                go.GetComponent<Button>().interactable = true;
                go.GetComponentInChildren<Text>().text = "";
            }
            else
            {
                go.GetComponentInChildren<Text>().text = i.ToString();
                go.GetComponent<Button>().interactable = false;
            }
        }
    }
}
