using UnityEngine;

public class ArrowButton : MonoBehaviour
{
    public RotatingStair stair; // referencia al RotatingStair
    public int direction = 1;   // 1 = derecha, -1 = izquierda

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Fireball"))
        {
            if (stair != null)
            {
                stair.Rotate(direction);
            }
        }
    }
}
