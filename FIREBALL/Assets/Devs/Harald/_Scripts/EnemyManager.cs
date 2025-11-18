using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    int hp = 1000;
    int damage = 100;
    float baseSpeed = 5.0f;
    float currentSpeed;
    [SerializeField] private float cooldownTime = 5.0f;

    private IFreezable freezableComponent;
    private IHeatable heatableComponent;

    private Coroutine freezeCoroutine;
    
    void Awake()
    {
        freezableComponent = GetComponent<IFreezable>();
        heatableComponent = GetComponent<IHeatable>();

        currentSpeed = baseSpeed;
    }

    void Start()
    {
        if(freezableComponent != null)
        {
            if (freezeCoroutine != null)
            {
                StopCoroutine(freezeCoroutine);
            }
            freezeCoroutine = StartCoroutine(FreezeEffect(cooldownTime));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Aplicar congelación
    public void ApplyFreeze()
    {
        if (freezableComponent != null)
        {
            // 1. Cambiar color y aplicar efecto visual (lo hace SimpleFreezable)
            freezableComponent.ApplyFreeze();

            // 2. Reducir velocidad por 5 segundos
            if (freezeCoroutine != null)
            {
                StopCoroutine(freezeCoroutine); // Detener cualquier congelación anterior
            }
            freezeCoroutine = StartCoroutine(FreezeEffect(5.0f));
        }
    }

    // Aplicar congelación
    private System.Collections.IEnumerator FreezeEffect(float duration)
    {
        float speedReductionFactor = 0.5f; // Por ejemplo, reducir la velocidad al 50%
        currentSpeed = baseSpeed * speedReductionFactor;
        
        yield return new WaitForSeconds(duration);

        // Volver a la velocidad normal y reestablecer el color (deberías añadir lógica de 'Unfreeze' en SimpleFreezable)
        currentSpeed = baseSpeed;
        // freezableComponent.Unfreeze(); // *Si añades este método a la interfaz*
    }

    // Aplicar quemadura
    public void ApplyBurn()
    {
        // Reducir HP en 100 puntos
        hp -= damage;
    }

    // Behavior de estado 1

}
