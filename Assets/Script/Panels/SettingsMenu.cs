using UnityEngine;

public class SettingsMenu : MonoBehaviour
{
    public GameObject SettingsMenuPanel;

    public void OpenPanel()
    {
        if (SettingsMenuPanel != null)
        {
            SettingsMenuPanel.SetActive(true);
        }
    }
}
