using UnityEngine;

public class PlayerRotate : MonoBehaviour
{
    public float rotationSpeed = 100f;   // Velocidad de giro

    void Update()
    {
        float horizontal = 0f;

        // A → -1   |   D → +1
        if (Input.GetKey(KeyCode.A))
            horizontal = -1f;
        else if (Input.GetKey(KeyCode.D))
            horizontal = 1f;

        // Rotamos sobre el eje Y del jugador
        transform.Rotate(0f, horizontal * rotationSpeed * Time.deltaTime, 0f);
    }
}
