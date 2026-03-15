using UnityEngine;

public class WrongBlock : MonoBehaviour
{
    public float delay = 0.3f;

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.collider.CompareTag("Player")) return;
        Invoke(nameof(Drop), delay);
    }

    void Drop()
    {
        gameObject.AddComponent<Rigidbody>();
        Destroy(gameObject, 1.5f);
    }
}
