using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChooseMng : MonoBehaviour
{
    public void ViewCollegeInfo(string college)
    {
        Debug.Log(UIPanelType.InfoPanel.ToString());
        UIMng.Instance.PushPanel(UIPanelType.InfoPanel, college);
    }
}
