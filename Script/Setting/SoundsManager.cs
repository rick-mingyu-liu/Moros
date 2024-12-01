using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SoundsManager : MonoBehaviour
{
    //public static SoundsManager Instance;

    [SerializeField] private AudioSource bgmSource;
    [SerializeField] Slider volumeSlider;

    public AudioMixer mixer;

    public float volumevalue;
    public float value;
    

    private void Start()
    {
        if (volumeSlider)
        {
            volumeSlider.value = PlayerPrefs.GetFloat("Volume", 1.0f);
            bgmSource.volume = volumeSlider.value;
        }
        else
        {
            bgmSource.volume = PlayerPrefs.GetFloat("Volume", 1.0f);
        }
    }

    private void Update()
    {
        //mixer.SetFloat("Volume", bgmSource.volume);
        PlayerPrefs.SetFloat("Volume", bgmSource.volume);
        if (volumeSlider)
        {
            bgmSource.volume = volumeSlider.value;
            //value = volumeSlider.value;
        }
    }
/*    public void SetVolume(float volume)
    {
        volumevalue = volume;
    } */   
}
