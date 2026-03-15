using UnityEngine;

public class PlayerJumpSound : MonoBehaviour
{
    public AudioSource jumpSource;

    private CharacterController controller;
    private bool wasGrounded;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        wasGrounded = controller.isGrounded;
    }

    void Update()
    {
        if (Time.timeScale == 0) return;

        if (wasGrounded && !controller.isGrounded && controller.velocity.y > 0.1f)
        {
            jumpSource.Play();
        }

        wasGrounded = controller.isGrounded;
    }
}
