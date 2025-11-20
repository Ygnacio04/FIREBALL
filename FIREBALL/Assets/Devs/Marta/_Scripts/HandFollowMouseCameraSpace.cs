using UnityEngine;

public class HandFollowMouseCameraSpace : MonoBehaviour
{
    public Camera cam;           // Cámara del player
    public Transform hand;       // La bola / "hand"
    public float distance = 1.5f; // Distancia fija delante de la cámara
    public float smooth = 15f;   // Suavidad del movimiento

    void Update()
    {
        // Posición del ratón en píxeles
        Vector3 mousePos = Input.mousePosition;

        // Z = distancia desde la cámara hacia delante
        mousePos.z = distance;

        // Convertimos de coordenadas de pantalla a mundo,
        // a 'distance' unidades delante de la cámara
        Vector3 targetWorldPos = cam.ScreenToWorldPoint(mousePos);

        // Movimiento suave de la bola hacia esa posición
        hand.position = Vector3.Lerp(hand.position, targetWorldPos, smooth * Time.deltaTime);
    }
}
