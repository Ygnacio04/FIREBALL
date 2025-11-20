using UnityEngine;
using System.Collections;

public class RotatingStair : MonoBehaviour
{
    [Header("Rotación por paso")]
    public float stepAngle = 90f;   // grados que gira cada vez (90, 45, etc)
    public float rotateTime = 0.5f; // tiempo que tarda en girar

    private bool isRotating = false;

    // direction = 1 (derecha), -1 (izquierda)
    public void Rotate(int direction)
    {
        if (isRotating) return;
        StartCoroutine(RotateCoroutine(direction));
    }

    private IEnumerator RotateCoroutine(int direction)
    {
        isRotating = true;

        Quaternion startRot = transform.rotation;
        Quaternion targetRot =
            startRot * Quaternion.Euler(0f, stepAngle * direction, 0f);

        float elapsed = 0f;

        while (elapsed < rotateTime)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / rotateTime);
            transform.rotation = Quaternion.Slerp(startRot, targetRot, t);
            yield return null;
        }

        transform.rotation = targetRot;
        isRotating = false;
    }
}