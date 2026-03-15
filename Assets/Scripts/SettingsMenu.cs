using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    public GameObject settingsPanel;
    public GameObject mainMenuPanel;

    [Header("Audio")]
    public AudioMixer audioMixer;

    [Header("Graphics")]
    public TMPro.TMP_Dropdown qualityDropdown;

    public Slider volumeSlider;


    void Start()
{
    float savedVolume = PlayerPrefs.GetFloat("Volume", 1f);
    volumeSlider.SetValueWithoutNotify(savedVolume);
    SetVolume(savedVolume);

    settingsPanel.SetActive(false);

    if (PlayerPrefs.HasKey("Fullscreen"))
        Screen.fullScreen = PlayerPrefs.GetInt("Fullscreen") == 1;

    if (PlayerPrefs.HasKey("Quality"))
    {
        int q = PlayerPrefs.GetInt("Quality");
        QualitySettings.SetQualityLevel(q);
        qualityDropdown.value = q;
    }
}


    public void OpenSettings()
    {
        mainMenuPanel.SetActive(false);
        settingsPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }

    // 🔊 SES

public void SetVolume(float volume)
{
    volume = Mathf.Clamp(volume, 0.0001f, 1f);
    float dB = Mathf.Log10(volume) * 20f;
    audioMixer.SetFloat("MyExposedParam", dB);
    PlayerPrefs.SetFloat("Volume", volume);
}





    // 🖥 FULLSCREEN
    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        PlayerPrefs.SetInt("Fullscreen", isFullscreen ? 1 : 0);
    }

    // 🎮 GRAFİK
    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        PlayerPrefs.SetInt("Quality", qualityIndex);
    }
}
