using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    [SerializeField] private float delay;
    
    private void Start() => Destroy(gameObject, delay);
}
