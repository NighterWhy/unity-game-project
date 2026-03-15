using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;


public class NPCFinalDialogueAndCode : MonoBehaviour
{
    [Header("Final UI")]
    public GameObject mainMenuButton;

    int remainingAttempts = 3;
    Color defaultCodeColor;


    [Header("Dialogue")]
    public string dialogueText = "Text for the NPC dialogue goes here.";
    public float displayTime = 5f;

    public TextMeshProUGUI subtitleText;
    public TextMeshProUGUI interactText;

    bool playerInRange = false;
    float timer = 0f;
    bool showingSubtitle = false;
    bool dialogueFinished = false;

    [Header("Final Code")]
    public TMP_Text codeText;
    public TMP_Text resultText;
    public Image screenPanel;

    public string correctCode = "123456789";

    string currentCode = "";
    bool codeActive = false;

    void Start()
    {
        subtitleText.alpha = 0;
        interactText.alpha = 0;
        codeText.gameObject.SetActive(false);
        resultText.text = "";
        screenPanel.color = new Color(0, 0, 0, 0);
        defaultCodeColor = codeText.color;
        mainMenuButton.SetActive(false);

    }

    void Update()
    {
        // 🔹 F ile diyalog
        if (playerInRange && !dialogueFinished && Input.GetKeyDown(KeyCode.F))
        {
            ShowSubtitle();
        }

        // 🔹 Diyalog süresi
        if (showingSubtitle)
        {
            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                HideSubtitle();
            }
        }

        // 🔹 Şifre aktifse klavye dinle
        if (!codeActive) return;

        foreach (char c in Input.inputString)
        {
            if (char.IsDigit(c) && currentCode.Length < 9)
            {
                currentCode += c;
                UpdateCodeDisplay();
            }

            if (c == '\b' && currentCode.Length > 0)
            {
                currentCode = currentCode[..^1];
                UpdateCodeDisplay();
            }

            if (c == '\n' || c == '\r')
            {
                CheckCode();
            }
        }
    }

    void ShowSubtitle()
    {
        subtitleText.text = dialogueText;
        subtitleText.alpha = 1;
        interactText.alpha = 0;

        timer = displayTime;
        showingSubtitle = true;
    }

    void HideSubtitle()
    {
        subtitleText.alpha = 0;
        showingSubtitle = false;
        dialogueFinished = true;

        // 🔥 DİYALOG BİTTİ → ŞİFRE AÇ
        ActivateCode();
    }

    void ActivateCode()
    {
        codeActive = true;
        codeText.gameObject.SetActive(true);
        UpdateCodeDisplay();
    }

    void UpdateCodeDisplay()
    {
        string t = "";
        for (int i = 0; i < 9; i++)
            t += i < currentCode.Length ? currentCode[i] + " " : "_ ";
        codeText.text = t;
    }

    void CheckCode()
    {
        if (currentCode == correctCode)
            StartCoroutine(Success());
        else
            StartCoroutine(Fail());
    }

    IEnumerator Success()
    {
        codeActive = false;
        codeText.gameObject.SetActive(false);

        screenPanel.color = Color.black;
        resultText.text = "OYUNU BAŞARIYLA BİTİRDİN";

        // 🖱 Fareyi aç
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        yield return new WaitForSecondsRealtime(1.5f);

        // 🎮 Ana menü butonunu aç
        mainMenuButton.SetActive(true);

        Time.timeScale = 0f;
    }



    IEnumerator Fail()
    {
        remainingAttempts--;

        // 🔴 şifre alanını kırmızı yap
        codeText.color = Color.red;

        yield return new WaitForSeconds(1.2f);

        // hak bittiyse → level 1
        if (remainingAttempts <= 0)
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(1); // Level 1 index
            yield break;
        }

        // renk + input reset
        codeText.color = defaultCodeColor;
        currentCode = "";
        UpdateCodeDisplay();
    }



    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !dialogueFinished)
        {
            playerInRange = true;
            interactText.alpha = 1;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            interactText.alpha = 0;
        }
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu"); // sahne adını yaz
    }

}
