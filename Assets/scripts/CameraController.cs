using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform target; 
    [SerializeField] private float distancie = 6f;
    [SerializeField] private float  height = 2f;
    [SerializeField] private float rotationSpeed = 20f; 

    float currentYawX = 0;
    float currentYawY = 0;

    void Update()
    {
        float mouseX = Mouse.current.delta.ReadValue().x; 
        float mouseY = Mouse.current.delta.ReadValue().y; 
        currentYawX += mouseX * rotationSpeed * Time.deltaTime; 
        currentYawY += mouseY * rotationSpeed * Time.deltaTime;
    }

    void LateUpdate()
    {
        if(target == null)
        {
            return; 
        }
        Quaternion rotation = Quaternion.Euler(0f, currentYawX, currentYawY); 

        Vector3 offset = new Vector3(0, height, -distancie); 

        transform.position = target.position + rotation * offset; 

        transform.rotation = Quaternion.LookRotation(target.position - transform.position); 
    }
}
