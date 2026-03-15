using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    public AudioSource footstepSource;

    public float minMoveSpeed = 0.1f;
    public KeyCode runKey = KeyCode.LeftShift;

    [Header("Pitch Settings")]
    public float walkPitch = 1f;
    public float runPitch = 1.35f;

    private CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        HandleFootsteps();
    }

    void HandleFootsteps()
    {
        if (!controller.isGrounded || controller.velocity.magnitude < minMoveSpeed)
        {
            if (footstepSource.isPlaying)
                footstepSource.Stop();
            return;
        }

        bool isRunning = Input.GetKey(runKey);

        footstepSource.pitch = isRunning ? runPitch : walkPitch;

        if (!footstepSource.isPlaying)
            footstepSource.Play();
    }
}
