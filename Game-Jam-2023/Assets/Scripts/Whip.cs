using UnityEngine;

public class Whip : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
            Destroy(other.gameObject);
    }
}
