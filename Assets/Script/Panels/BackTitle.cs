using JetBrains.Annotations;
using UnityEngine;

public class BackTitle : MonoBehaviour
{
    public GameObject SettingsMenuPanel;
    public void GoBackTitle()
    {
        SettingsMenuPanel.gameObject.SetActive(false);
      
    }
}
