using UnityEngine;

public class IceBallSimple : MonoBehaviour
{
    [Header("Configuración")]
    [Tooltip("Fuerza de lanzamiento")]
    public float fuerzaLanzamiento = 10f;

    [Tooltip("Dirección de lanzamiento (normalizada automáticamente)")]
    public Vector3 direccionLanzamiento = Vector3.down;

    [Tooltip("Lanzar automáticamente al inicio")]
    public bool lanzarAlInicio = false;

    [Tooltip("Tiempo antes de autodestruirse (0 = nunca)")]
    public float tiempoAutodestruccion = 5f;

    private Rigidbody rb;
    private bool yaLanzada = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (rb == null)
        {
            Debug.LogError("¡La bola necesita un Rigidbody!");
            return;
        }

        if (lanzarAlInicio)
        {
            Lanzar();
        }

        // Autodestrucción si está configurada
        if (tiempoAutodestruccion > 0)
        {
            Destroy(gameObject, tiempoAutodestruccion);
        }
    }

    void Update()
    {
        // Lanzar con la tecla Espacio (para pruebas)
        if (Input.GetKeyDown(KeyCode.Space) && !yaLanzada)
        {
            Lanzar();
        }
    }

    public void Lanzar()
    {
        if (rb == null || yaLanzada) return;

        // Normalizar dirección y aplicar fuerza
        Vector3 direccion = direccionLanzamiento.normalized;
        rb.AddForce(direccion * fuerzaLanzamiento, ForceMode.Impulse);

        yaLanzada = true;
        Debug.Log("Bola de hielo lanzada!");
    }

    void OnCollisionEnter(Collision collision)
    {
        // Opcional: efectos al colisionar
        Debug.Log($"Bola colisionó con: {collision.gameObject.name}");
    }
}