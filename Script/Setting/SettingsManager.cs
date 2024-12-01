using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    public GameObject settingsPanel; // 引用设置界面
    public GameObject settingsPanel1; // 引用设置界面
    private bool isSettingsOpen = false; // 设置界面是否打开

    private void Update()
    {
        // 检查按下Esc键
        if (Input.GetKeyDown(KeyCode.Escape) && !settingsPanel1.activeInHierarchy)
        {
            ToggleSettings();
        }
    }

    public void ToggleSettings()
    {
        isSettingsOpen = !isSettingsOpen;

        // 打开或关闭设置界面
        settingsPanel.SetActive(isSettingsOpen);

        // 暂停或恢复游戏
        Time.timeScale = isSettingsOpen ? 0 : 1;

    }

    public void CloseSettings()
    {
        isSettingsOpen = false;
        settingsPanel.SetActive(false);
        Time.timeScale = 1;
    }
}

