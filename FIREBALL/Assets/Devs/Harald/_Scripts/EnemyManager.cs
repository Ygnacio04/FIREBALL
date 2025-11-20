using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    int hp = 1000;
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
        // Movimiento izquierda-derecha entre límites fijos
        float leftLimit = -5f;
        float rightLimit = 5f;

        // Dirección basada en el signo de localScale.x (no requiere campos adicionales)
        float dir = Mathf.Sign(transform.localScale.x);

        // Mover
        transform.Translate(Vector3.right * dir * currentSpeed * Time.deltaTime, Space.World);

        // Girar al llegar a los límites
        if (transform.position.x >= rightLimit && dir > 0f)
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
        else if (transform.position.x <= leftLimit && dir < 0f)
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
        
        Debug.Log("Velocidad actual: " + currentSpeed + ", HP: " + hp);
    }

    // Aplicar congelación
    public void ApplyFreeze()
    {
        if (freezableComponent != null)
        {
            freezableComponent.ApplyFreeze();

            if (freezeCoroutine != null)
            {
                StopCoroutine(freezeCoroutine);
            }
            freezeCoroutine = StartCoroutine(FreezeEffect(5.0f));
        }
    }

    // Aplicar congelación
    private System.Collections.IEnumerator FreezeEffect(float duration)
    {
        float speedReductionFactor = 0.5f;
        currentSpeed = baseSpeed * speedReductionFactor;
        
        yield return new WaitForSeconds(duration);

        currentSpeed = baseSpeed;
    }

    // Aplicar quemadura
    public void ApplyBurn()
    {
        heatableComponent?.ApplyHeat();
        
        // Reducir HP en 2 puntos
        hp -= 2;
    }

    // Behavior de estado 1

}
