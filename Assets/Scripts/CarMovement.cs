using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class CarMovement : MonoBehaviour
{
    private Rigidbody2D rb;

    private int moveDirection = 0;
    private bool isBoosted = false;
    [SerializeField] private float speed = 3f;
    [SerializeField] private float boostMultiplier = 2f;

    private int rotationDirection = 0;
    [SerializeField] private float rotationSpeed = 45f;

    [SerializeField] private TMP_Text boostText;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boostText.enabled = false;
    }

    void Update()
    {
        if (Keyboard.current.wKey.isPressed) moveDirection = 1;
        else if (Keyboard.current.sKey.isPressed) moveDirection = -1;
        else moveDirection = 0;

        if (Keyboard.current.aKey.isPressed) rotationDirection = 1;
        else if (Keyboard.current.dKey.isPressed) rotationDirection = -1;
        else rotationDirection = 0;
    }

    void FixedUpdate()
    {
        if (isBoosted)
            rb.AddForce(speed * boostMultiplier * moveDirection * Time.fixedDeltaTime * transform.up, ForceMode2D.Impulse);
        else
            rb.AddForce(speed * moveDirection * Time.fixedDeltaTime * transform.up, ForceMode2D.Impulse);

        if (moveDirection >= 0)
            rb.AddTorque(rotationSpeed * rotationDirection * Time.fixedDeltaTime);
        else if (moveDirection < 0)
            rb.AddTorque(-rotationSpeed * rotationDirection * Time.fixedDeltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Boost"))
        {
            isBoosted = true;
            boostText.enabled = true;
            Destroy(other.gameObject);
        }
    }
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        isBoosted = false;
        boostText.enabled = false;
    }
}
