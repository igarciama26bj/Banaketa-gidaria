using TMPro;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CarLifes : MonoBehaviour
{
    [SerializeField] private float minSpeed = 0f;
    [SerializeField] private float lifes = 0f;
    [SerializeField] private TMP_Text lifesText;
    [SerializeField] private TMP_Text deathText;

    void Start()
    {
        lifesText.SetText(lifes.ToString());
        deathText.enabled = false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        print(collision.relativeVelocity.magnitude);
        if (collision.relativeVelocity.magnitude > minSpeed)
        {
            lifes--;
            lifesText.SetText(lifes.ToString());
        }
        if (lifes == 0)
        {
            Destroy(gameObject);
            deathText.enabled = true;
        }
    }
}
