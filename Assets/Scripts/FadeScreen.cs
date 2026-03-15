using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;


public class FadeScreen : MonoBehaviour
{
    public Image blackScreen;
    public float fadeSpeed = 1f;

    public void FadeOutAndRestart()
    {
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        Color c = blackScreen.color;

        while (c.a < 1f)
        {
            c.a += Time.deltaTime * fadeSpeed;
            blackScreen.color = c;
            yield return null;
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
