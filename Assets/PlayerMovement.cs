using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Hareket Ayarları")]
    public float moveSpeed = 7f;      // Yürüme hızı
    public float groundDrag = 6f;     // Yer sürtünmesi (Bunu düşürürsen buz pisti olur)
    public float jumpForce = 12f;     // Zıplama gücü
    public float airMultiplier = 0.4f;// Havada kontrol oranı (CS:GO hissi için 0.4 iyidir)

    [Header("Zemin Kontrolü")]
    public float playerHeight = 2f;
    public LayerMask whatIsGround;    // Hangi objeler yer sayılacak?
    bool isGrounded;

    [Header("Referanslar")]
    public Transform orientation;     // Az önce oluşturduğumuz Orientation objesi

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; // Fizik motoru karakteri devirmesin
    }

    private void Update()
    {
        // Yere değiyor muyuz kontrolü (Raycast atıyoruz)
        isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);

        MyInput();
        SpeedControl();

        // Yer ve Hava Sürtünmesi Ayarı
        if (isGrounded)
            rb.linearDamping = groundDrag;
        else
            rb.linearDamping = 0; // Havada sürtünme yok, böylece hızını korursun!
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        // Zıplama
        if (Input.GetKey(KeyCode.Space) && isGrounded)
        {
            Jump();
        }
    }

    private void MovePlayer()
    {
        // Hareketi kameranın baktığı yöne (orientation) göre hesapla
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        // Yerdeyken normal hareket
        if (isGrounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);

        // Havadayken (Air Strafe)
        else if (!isGrounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

        // Maksimum hızı geçersen frenle
        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.linearVelocity = new Vector3(limitedVel.x, rb.linearVelocity.y, limitedVel.z);
        }
    }

    private void Jump()
    {
        // Y eksenindeki hızı sıfırla ki her seferinde tutarlı zıplasın
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }
}