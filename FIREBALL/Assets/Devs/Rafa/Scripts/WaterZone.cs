using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterZone : MonoBehaviour
{
    [Header("Configuración del Puente")]
    [Tooltip("Prefab de la plataforma de hielo (Debe tener el script IcePlatform)")]
    public GameObject plataformaHieloPrefab;

    [Tooltip("Duración del puente en segundos")]
    public float duracionPuente = 15f;

    [Tooltip("Altura sobre el agua")]
    public float alturaPlataforma = 0.15f;

    void OnCollisionEnter(Collision collision)
    {
        BlueFreezeBehavior freezeAttack = collision.gameObject.GetComponent<BlueFreezeBehavior>();

        if (freezeAttack != null) {
            Vector3 puntoImpacto = collision.contacts[0].point;

            CrearPlataformaHielo(puntoImpacto);
        }
    }

    void CrearPlataformaHielo(Vector3 posicion)
    {
        Vector3 posicionPlataforma = new Vector3(
            posicion.x,
            transform.position.y + alturaPlataforma,
            posicion.z
        );

        GameObject nuevaPlataforma = Instantiate(plataformaHieloPrefab, posicionPlataforma, Quaternion.identity);

        IcePlatform scriptPlataforma = nuevaPlataforma.GetComponent<IcePlatform>();
        if (scriptPlataforma != null) scriptPlataforma.Initialize(duracionPuente);
        else Debug.LogWarning("El prefab de hielo no tiene el script IcePlatform asignado.");

        Debug.Log($"Plataforma creada en {posicionPlataforma}.");
    }
}