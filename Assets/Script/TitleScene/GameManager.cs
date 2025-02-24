using UnityEngine;
using UnityEngine.XR;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Management;
using System.Collections;
using UnityEngine.Rendering;

public class GameManager : MonoBehaviour
{
    private void Start()
    {
        DisableXR();
    }

    public void ActiveVR()
    {
        StartCoroutine(StartVR());
    }

    public void DisableXR()
    {
        // XR�@�\�𖳌��������\�[�X���������
        XRGeneralSettings.Instance.Manager.StopSubsystems();
        XRGeneralSettings.Instance.Manager.DeinitializeLoader();
        Debug.Log("XR�@�\������������܂���");
    }

    public IEnumerator StartVR()
    {
        //Debug.Log("Start VR Mode");

        //XR�̗L����
        yield return XRGeneralSettings.Instance.Manager.InitializeLoader();
        if (XRGeneralSettings.Instance.Manager.activeLoader != null)
        {
            XRGeneralSettings.Instance.Manager.StartSubsystems();
        }

        SceneManager.LoadScene(1);
    }

}
