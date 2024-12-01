using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;

public class BrightnessManager : MonoBehaviour
{
    public Slider brightnessSlider;
    public PostProcessProfile brightnessProfile;
    private AutoExposure exposure;

    private void Start()
    {
        if (brightnessProfile.TryGetSettings(out exposure))
        {
            float savedBrightness = PlayerPrefs.GetFloat("Brightness", 1.0f);
            exposure.keyValue.value = savedBrightness;

            if (brightnessSlider != null)
            {
                brightnessSlider.value = savedBrightness;
                brightnessSlider.onValueChanged.AddListener(AdjustBrightness);
            }
        }
        else
        {
            Debug.LogError("AutoExposure not found in the PostProcessProfile!");
        }
    }

    private void Update()
    {
        PlayerPrefs.SetFloat("Brightness", exposure.keyValue.value);
        if(brightnessSlider)
            exposure.keyValue.value = brightnessSlider.value;
    }

    public void AdjustBrightness(float value)
    {
        if (value != 0)
        {
            exposure.keyValue.value = value;
            brightnessSlider.value = value;
            // 保存新的亮度值到 PlayerPrefs
            PlayerPrefs.SetFloat("Brightness", value);
        }
        else
        {
            exposure.keyValue.value = 0.05f;
            brightnessSlider.value = 0.05f;
        }
    }
}