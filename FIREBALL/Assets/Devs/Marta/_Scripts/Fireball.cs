using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Fireball : MonoBehaviour
{
    public float lifeTime =20f;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }
}
