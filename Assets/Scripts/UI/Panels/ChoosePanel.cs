using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChoosePanel : BasePanel
{
    public void ViewCollegeInfo(string college)
    {
        UIMng.Instance.PushPanel(UIPanelType.ConfirmPanel, college);
    }
}
