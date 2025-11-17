using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterZone : MonoBehaviour
{
    [Header("Configuración del Puente")]
    [Tooltip("Prefab de la plataforma de hielo")]
    public GameObject plataformaHieloPrefab;

    [Tooltip("Duración del puente en segundos")]
    public float duracionPuente = 15f;

    [Tooltip("Tamaño de cada plataforma")]
    public Vector3 tamanoPlatforma = new Vector3(2f, 0.2f, 2f);

    [Tooltip("Altura sobre el agua")]
    public float alturaPlataforma = 0.15f;

    [Header("Visual")]
    [Tooltip("Material de hielo")]
    public Material materialHielo;

    [Header("Audio (Opcional)")]
    public AudioClip sonidoCreacion;
    public AudioClip sonidoRomperse;

    private AudioSource audioSource;
    private List<GameObject> plataformasActivas = new List<GameObject>();

    void Start()
    {
        // Configurar AudioSource si hay sonidos
        if (sonidoCreacion != null || sonidoRomperse != null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Si no hay prefab asignado, crear uno automáticamente
        if (plataformaHieloPrefab == null)
        {
            CrearPrefabPorDefecto();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // Detectar si es una bola de hielo
        if (collision.gameObject.CompareTag("IceBall"))
        {
            // Obtener punto de impacto
            Vector3 puntoImpacto = collision.contacts[0].point;

            // Crear plataforma en ese punto
            CrearPlataformaHielo(puntoImpacto);

            // Opcional: destruir la bola de hielo
            Destroy(collision.gameObject);
        }
    }

    void CrearPlataformaHielo(Vector3 posicion)
    {
        // Ajustar posición: misma X y Z, pero justo sobre el agua
        Vector3 posicionPlataforma = new Vector3(
            posicion.x,
            transform.position.y + alturaPlataforma,
            posicion.z
        );

        // Instanciar la plataforma
        GameObject nuevaPlataforma = Instantiate(plataformaHieloPrefab, posicionPlataforma, Quaternion.identity);
        nuevaPlataforma.transform.localScale = tamanoPlatforma;

        // Agregar a la lista de plataformas activas
        plataformasActivas.Add(nuevaPlataforma);

        // Reproducir sonido de creación
        if (sonidoCreacion != null && audioSource != null)
        {
            audioSource.PlayOneShot(sonidoCreacion);
        }

        // Iniciar temporizador para destruir
        StartCoroutine(DestruirPlataformaDespuesDeTiempo(nuevaPlataforma, duracionPuente));

        Debug.Log($"Plataforma creada en {posicionPlataforma}. Durará {duracionPuente} segundos.");
    }

    IEnumerator DestruirPlataformaDespuesDeTiempo(GameObject plataforma, float tiempo)
    {
        // Esperar el tiempo especificado
        yield return new WaitForSeconds(tiempo);

        // Efecto visual antes de destruir (opcional)
        StartCoroutine(EfectoRomperse(plataforma));
    }

    IEnumerator EfectoRomperse(GameObject plataforma)
    {
        if (plataforma == null) yield break;

        // Reproducir sonido
        if (sonidoRomperse != null && audioSource != null)
        {
            audioSource.PlayOneShot(sonidoRomperse);
        }

        // Efecto de parpadeo
        Renderer renderer = plataforma.GetComponent<Renderer>();
        if (renderer != null)
        {
            for (int i = 0; i < 6; i++)
            {
                renderer.enabled = !renderer.enabled;
                yield return new WaitForSeconds(0.15f);
            }
        }

        // Remover de lista y destruir
        plataformasActivas.Remove(plataforma);
        Destroy(plataforma);

        Debug.Log("Plataforma destruida");
    }

    void CrearPrefabPorDefecto()
    {
        // Crear un cubo como prefab básico
        plataformaHieloPrefab = GameObject.CreatePrimitive(PrimitiveType.Cube);
        plataformaHieloPrefab.name = "PlataformaHielo";

        // Aplicar material si existe
        if (materialHielo != null)
        {
            plataformaHieloPrefab.GetComponent<Renderer>().material = materialHielo;
        }
        else
        {
            // Crear material azul claro por defecto
            Material matHielo = new Material(Shader.Find("Standard"));
            matHielo.color = new Color(0.7f, 0.9f, 1f, 0.8f);
            matHielo.SetFloat("_Metallic", 0.3f);
            matHielo.SetFloat("_Glossiness", 0.9f);
            plataformaHieloPrefab.GetComponent<Renderer>().material = matHielo;
        }

        // Desactivar para usar como prefab
        plataformaHieloPrefab.SetActive(false);

        Debug.Log("Prefab de plataforma creado automáticamente");
    }

    // Método público para crear plataforma manualmente (útil para debugging)
    public void CrearPlataformaEnPosicion(Vector3 posicion)
    {
        CrearPlataformaHielo(posicion);
    }

    // Limpiar todas las plataformas activas
    public void LimpiarTodasLasPlataformas()
    {
        foreach (GameObject plataforma in plataformasActivas)
        {
            if (plataforma != null)
            {
                Destroy(plataforma);
            }
        }
        plataformasActivas.Clear();
        Debug.Log("Todas las plataformas limpiadas");
    }
}