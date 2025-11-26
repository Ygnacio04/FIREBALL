using UnityEngine;
using UnityEngine.AI;
using System.Collections;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(EnemyManager))] 
public class EnemyAi : MonoBehaviour, IHeatable, IFreezable
{
    public Transform playerTarget;
    public NavMeshAgent Agent { get; private set; }
    private EnemyManager enemyManager;
    public MeshRenderer meshRenderer;
    public Color colorCongelado = Color.blue;
    public Color colorQuemado = Color.red;
    private Color colorOriginal;

    [Header("settings")]
    public float patrolSpeed = 2.5f;
    public float chaseSpeed = 4.5f;
    public float detectionRadius = 10f;
    public float attackRange = 1.5f;
    public float fleeDuration = 4f; 
    
    private IEnemyState currentState;
    public bool IsStunned { get; set; } = false;

    void Awake()
    {
        Agent = GetComponent<NavMeshAgent>();
        enemyManager = GetComponent<EnemyManager>(); 
        
        if (meshRenderer == null) meshRenderer = GetComponentInChildren<MeshRenderer>();
        
        if (playerTarget == null) {
            var playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null) playerTarget = playerObj.transform;
        }
    }

    void Start()
    {
        if (meshRenderer != null) colorOriginal = meshRenderer.material.color;
        ChangeState(new PatrolState());
    }

    void Update()
    {
        if (currentState != null && !IsStunned) currentState.Update(this);
    }

    public void ChangeState(IEnemyState newState) {
        if (currentState != null) currentState.Exit(this);
        currentState = newState;
        if (currentState != null) currentState.Enter(this);
    }

    public void ApplyHeat() {
        StartCoroutine(VisualFlashRoutine(colorQuemado, 2.0f)); 
        ChangeState(new FleeState());

        if(enemyManager != null) {
            enemyManager.ApplyBurn();
        }
    }

    public void ApplyFreeze() {
        if (IsStunned) return;
        StartCoroutine(FreezeRoutine());
    }

    private IEnumerator FreezeRoutine()
    {
        IsStunned = true;
        Agent.isStopped = true;
        
        if (meshRenderer != null) meshRenderer.material.color = colorCongelado;

        yield return new WaitForSeconds(3.0f); 
        
        if (meshRenderer != null) meshRenderer.material.color = colorOriginal;
        
        Agent.isStopped = false;
        IsStunned = false;
    }
    
    private IEnumerator VisualFlashRoutine(Color targetColor, float duration)
    {
        if (meshRenderer != null) meshRenderer.material.color = targetColor;
        
        yield return new WaitForSeconds(duration);
        
        if (meshRenderer != null && !IsStunned) 
            meshRenderer.material.color = colorOriginal;
    }
    
    public float DistanceToPlayer() {
        if (playerTarget == null) return float.MaxValue;
        return Vector3.Distance(transform.position, playerTarget.position);
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}