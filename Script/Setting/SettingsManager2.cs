using UnityEngine;

public class SettingsManager2 : MonoBehaviour
{
    public GameObject settingsPanel; // 引用设置界面
    private bool isSettingsOpen = false; // 设置界面是否打开

    private void Update()
    {
        // 检查按下1键
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleSettings();
        }
    }

    public void ToggleSettings()
    {
        isSettingsOpen = !isSettingsOpen;

        // 打开或关闭设置界面
        settingsPanel.SetActive(false);
    }
}

