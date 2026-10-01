using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class CarMovement : MonoBehaviour
{
    private Rigidbody2D rb;

    private int moveDirection = 0;
    private bool isBoosted = false;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float boost = 10f;

    private int rotationDirection = 0;
    [SerializeField] private float rotationSpeed = 120f;

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
        //if (isBoosted)
        //    transform.Translate(0, boost * moveDirection * Time.fixedDeltaTime, 0);
        //else
        //    transform.Translate(0, speed * moveDirection * Time.fixedDeltaTime, 0);
        
        rb.AddForce(speed * moveDirection * Time.fixedDeltaTime * transform.up, ForceMode2D.Force);

        float vx = rb.linearVelocityX;
        float vy = rb.linearVelocityY;
        if (vx > 3) vx = 3;
        if (vy > 3) vy = 3;
        rb.linearVelocity = new(vx, vy);

        if (rb.linearVelocityY > 0)
            rb.MoveRotation(rotationSpeed * rotationDirection * Time.fixedDeltaTime);
        else if (rb.linearVelocityY < 0)
            rb.MoveRotation(rb.rotation - rotationSpeed * rotationDirection * Time.fixedDeltaTime);

        Debug.Log($"Linear: {rb.linearVelocity.magnitude} Angular: {rb.angularVelocity}");
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
