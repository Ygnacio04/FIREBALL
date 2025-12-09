using UnityEngine;

public class WandShooter : MonoBehaviour
{
    public Camera cam;                 // Cámara
    public Transform hand;             // Mano / punto de salida
    public GameObject fireballPrefab;  // Prefab de la bola

    public float speed = 20f;          // Velocidad inicial
    public float extraUp = 0f;         // Opcional: pequeño plus hacia arriba

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        if (cam == null || hand == null || fireballPrefab == null) return;

        // 1) Dirección del rayo que sale de la cámara hacia el ratón
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        Vector3 dir = ray.direction.normalized;

        // 2) Instanciamos la bola en la POSICIÓN del hand
        GameObject fb = Instantiate(fireballPrefab, hand.position, Quaternion.identity);

        // 3) Le damos velocidad en la dirección del ratón
        Rigidbody rb = fb.GetComponent<Rigidbody>();
        if (rb != null)
        {
            // opcional: añadir un pelín hacia arriba para curva extra
            Vector3 finalDir = dir + Vector3.up * extraUp;
            rb.linearVelocity = finalDir.normalized * speed;
        }
    }
}
