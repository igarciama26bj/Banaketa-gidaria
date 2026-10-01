using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class PaketeTrigger : MonoBehaviour
{
    private ParticleSystem ps;
    private bool hasPakete = false;

    void Start()
    {
        ps = GetComponent<ParticleSystem>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!hasPakete && other.CompareTag("Pakete"))
        {
            Debug.Log("Paketea!");
            hasPakete = true;
            ps.Play();
            Destroy(other.gameObject);
        } else if (hasPakete && other.CompareTag("Bezero"))
        {
            Debug.Log("Bezeroa!");
            hasPakete = false;
            ps.Stop();
        }
    }
}
