using UnityEngine;

public class DeviceTypeManager : MonoBehaviour
{
    public static DeviceTypeManager Instance { get; private set; } // シングルトンインスタンス

    bool isMobile;

    private void Awake()
    {
        // シングルトンの基本処理
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // シーンをまたいでも破棄されない
        }
        else
        {
            Destroy(gameObject); // すでに存在していたら削除
        }
    }

    public void SetIsMobile(bool isMobileBool)
    {
        isMobile = isMobileBool;
    }

    public bool GetIsMoblie()
    {
        return isMobile;
    }


}
