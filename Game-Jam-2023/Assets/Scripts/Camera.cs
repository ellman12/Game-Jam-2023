using Cinemachine;
using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    
    private void Start()
    {
        virtualCamera.Follow = GameObject.Find("Player").transform;
    }
}
