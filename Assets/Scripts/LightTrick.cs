using UnityEngine;

public class LightFollowAndFade : MonoBehaviour
{
    public Light levelLight;                 // Sahnedeki ışık
    public float moveThreshold = 0.05f;      // Hareket algılama eşiği
    public float darkIntensity = 0f;         // Karanlık yoğunluk
    public float normalIntensity = 1.5f;     // Açık ışık yoğunluk
    public float fadeSpeed = 3f;             // Açılma yumuşak hızı
    public float waitBeforeLight = 0.4f;     // Durunca ışığın açılmadan önce bekleme süresi

    CharacterController cc;
    float stopTimer = 0f;
    bool wasMovingLastFrame = false;

    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Işığı oyuncunun üstünde tut
        if (levelLight != null)
            levelLight.transform.position = transform.position + Vector3.up * 2f;

        // Yatay hız (dikey hız karışmasın)
        Vector3 vel = new Vector3(cc.velocity.x, 0, cc.velocity.z);
        bool isMoving = vel.magnitude > moveThreshold;

        // --- Oyuncu hareket ediyor ise ---
        if (isMoving)
        {
            levelLight.intensity = darkIntensity;   // ANINDA kapat
            stopTimer = 0f;                          // Bekleme reset
            wasMovingLastFrame = true;
        }
        else
        {
            if (wasMovingLastFrame)
            {
                // Yeni durdu
                stopTimer = 0f;
                wasMovingLastFrame = false;
            }

            stopTimer += Time.deltaTime;

            // oyuncu durdu → WAIT → açılmaya başla
            if (stopTimer >= waitBeforeLight)
            {
                levelLight.intensity = Mathf.Lerp(
                    levelLight.intensity,
                    normalIntensity,
                    Time.deltaTime * fadeSpeed
                );
            }
        }
    }
}
