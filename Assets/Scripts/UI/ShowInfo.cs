using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowInfo : MonoBehaviour
{
    public int id;

    public void ViewItemInfo()
    {
        Debug.Log(id);
        UIMng.Instance.PushPanel(UIPanelType.InfoPanel, CsvMng.Instance.bookData[id.ToString()]["Name"]);
    }
}
