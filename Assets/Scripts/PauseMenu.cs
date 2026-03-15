using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;

public class PauseManager : MonoBehaviour
{
    [Header("Paneller")]
    public GameObject pauseMenuHolder; // Tüm menüyü tutan ana obje
    public GameObject mainButtonsPanel; // Butonların olduğu panel
    public GameObject settingsPanel;    // Ayarların olduğu panel

    [Header("Ayarlar")]
    public AudioMixer audioMixer;       // Ses kontrolü için
    public Slider volumeSlider;         // Ses slider'ı referansı
    public TMP_Dropdown qualityDropdown; // Kalite dropdown referansı
    public Toggle fullscreenToggle;      // Tam ekran toggle referansı

    // Oyunun durup durmadığını takip eden değişken
    public static bool isPaused = false;

    void Start()
    {
        // Oyun başladığında menü kapalı olsun
        pauseMenuHolder.SetActive(false);
        settingsPanel.SetActive(false); // Ayarlar kapalı başlasın

        // --- KAYITLI AYARLARI YÜKLE ---
        
        // 1. Sesi Yükle
        float savedVol = PlayerPrefs.GetFloat("Volume", 1f);
        if(volumeSlider) volumeSlider.value = savedVol;
        SetVolume(savedVol);

        // 2. Kaliteyi Yükle
        if (PlayerPrefs.HasKey("Quality"))
        {
            int q = PlayerPrefs.GetInt("Quality");
            QualitySettings.SetQualityLevel(q);
            if(qualityDropdown) qualityDropdown.value = q;
        }

        // 3. Fullscreen Yükle
        if (PlayerPrefs.HasKey("Fullscreen"))
        {
            bool isFull = PlayerPrefs.GetInt("Fullscreen") == 1;
            Screen.fullScreen = isFull;
            if(fullscreenToggle) fullscreenToggle.isOn = isFull;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Eğer Ayarlar paneli açıksa, ESC'ye basınca önce ayarları kapatıp butonlara dön
            if (settingsPanel.activeSelf)
            {
                CloseSettings();
            }
            else
            {
                // Değilse oyunu durdur veya devam ettir
                if (isPaused) ResumeGame();
                else PauseGame();
            }
        }
    }

    // --- TEMEL FONKSİYONLAR ---

    public void PauseGame()
    {
        pauseMenuHolder.SetActive(true);
        mainButtonsPanel.SetActive(true);
        Time.timeScale = 0f; // Zamanı dondur
        
        Cursor.lockState = CursorLockMode.None; // Mouse serbest bırak
        Cursor.visible = true;
        
        isPaused = true;
    }

    public void ResumeGame()
    {
        pauseMenuHolder.SetActive(false);
        settingsPanel.SetActive(false);
        Time.timeScale = 1f; // Zamanı devam ettir
        
        Cursor.lockState = CursorLockMode.Locked; // Mouse'u kilitle
        Cursor.visible = false;
        
        isPaused = false;
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f; // Ana menüye giderken zamanı düzeltmeyi unutma
        SceneManager.LoadScene("MainMenu"); // Sahne adı
    }

    // --- AYAR PANELİ GEÇİŞLERİ ---

    public void OpenSettings()
    {
        mainButtonsPanel.SetActive(false);
        settingsPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
        mainButtonsPanel.SetActive(true);
    }

    // --- AYAR FONKSİYONLARI ---

    public void SetVolume(float volume)
    {
        // Ses 0'a inince hata vermesin diye kontrol
        float safeVolume = Mathf.Clamp(volume, 0.0001f, 1f);
        float dB = Mathf.Log10(safeVolume) * 20f;
        
        if(audioMixer) audioMixer.SetFloat("MyExposedParam", dB);
        PlayerPrefs.SetFloat("Volume", volume);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        PlayerPrefs.SetInt("Quality", qualityIndex);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        PlayerPrefs.SetInt("Fullscreen", isFullscreen ? 1 : 0);
    }
}