using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleButtonManager : MonoBehaviour
{
    [SerializeField] DeviceTypeManager deviceTypeManager;
    void Start()
    {

    }
    public void OnTapPC()
    {
        deviceTypeManager.SetIsMobile(false);
        SceneManager.LoadScene(1);
    }
    public void OnTapMobile()
    {
        deviceTypeManager.SetIsMobile(true);
        SceneManager.LoadScene(1);
    }
}
