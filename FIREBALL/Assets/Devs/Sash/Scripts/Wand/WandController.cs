using UnityEngine;
using System; // Para System.Type

public class WandController : MonoBehaviour
{
    [Header("Projectile settings")]
    public GameObject fireballPrefab; 
    public Transform shootPoint; 
    public float launchForce = 10f;

    [Header("Behavior assets")]
    public PhysicsMaterial redBouncyMaterial;
    public string redBallTargetTag = "PuzzleTarget";

    [Header("Ammo system")]
    private System.Type defaultBehavior = typeof(OrangeHeatBehavior);
    private System.Type currentBehavior;
    private int specialShotsRemaining = 0;
    
    public event Action<System.Type, int> onBehaviorChanged;
    public event Action onAmmoUsed;

    void Start() {
        currentBehavior = defaultBehavior;
        Debug.Log(currentBehavior.Name);
    
    }

    public void ShootFireball() {
        if (fireballPrefab == null || shootPoint == null || currentBehavior == null) return;
    
        GameObject newBall = FireballPoolManager.Instance.GetFireball();
        newBall.transform.position = shootPoint.position;
        newBall.transform.rotation = shootPoint.rotation;

        Component newBehaviorComponent = newBall.AddComponent(currentBehavior);

        IProjectileBehavior behaviorInterface = newBehaviorComponent as IProjectileBehavior;
        if (behaviorInterface != null) behaviorInterface.Initialize(this);

        BaseProjectile projectileScript = newBall.GetComponent<BaseProjectile>();
        if (projectileScript != null) projectileScript.Launch(shootPoint.forward, launchForce);
    
        if (specialShotsRemaining > 0) {
            specialShotsRemaining--;
            onAmmoUsed?.Invoke();
            
            if (specialShotsRemaining == 0) {
                currentBehavior = defaultBehavior;
                onBehaviorChanged?.Invoke(currentBehavior, 0);
            }
        }
    }

    public void LoadSpecialShots(IGem gem) {
        if (gem == null) return;

        currentBehavior = gem.GetBehaviorType();
        specialShotsRemaining = gem.GetShotCount();
        
        Debug.Log($"{currentBehavior.Name}, {specialShotsRemaining} shots");
        onBehaviorChanged?.Invoke(currentBehavior, specialShotsRemaining);
    }
}