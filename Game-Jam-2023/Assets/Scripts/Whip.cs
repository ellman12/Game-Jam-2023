using UnityEngine;

public class Whip : MonoBehaviour
{
    [SerializeField]
    private AudioSource squish;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            squish.Play();
            Destroy(other.gameObject);
        }
            
    }
}
