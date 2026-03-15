using UnityEngine;
using TMPro;

public class NPCSimpleDialogue : MonoBehaviour
{
    public string dialogueText = "Text for the NPC dialogue goes here.";
    public float displayTime = 5f;

    public TextMeshProUGUI subtitleText;
    public TextMeshProUGUI interactText;

    private bool playerInRange = false;
    private float timer = 0f;
    private bool showingSubtitle = false;

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.F))
        {
            ShowSubtitle();
        }

        if (showingSubtitle)
        {
            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                HideSubtitle();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            interactText.alpha = 1;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            interactText.alpha = 0;
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
    }
}
