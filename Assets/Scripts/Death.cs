using UnityEngine;

public class DeathZone : MonoBehaviour
{
    public FadeScreen fade;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            fade.FadeOutAndRestart();
        }
    }
}