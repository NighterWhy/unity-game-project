using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalLoader : MonoBehaviour
{
    public string targetScene = "Level_02";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(targetScene);
        }
    }
}
