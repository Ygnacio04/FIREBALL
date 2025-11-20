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

    [Header("UI Diegética")]
    //public Transform ammoBarRoot;
    public Transform ammoFill;
    public float maxBarHeight = 0.2f;

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
            UpdateAmmoBar();
            
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
        UpdateAmmoBar();
        
        Debug.Log($"{currentBehavior.Name}, {specialShotsRemaining} shots");
        onBehaviorChanged?.Invoke(currentBehavior, specialShotsRemaining);
    }

    private void UpdateAmmoBar()
    {
        if(ammoFill == null) return;

        float maxShots = 5f;

        float normalized = Mathf.Clamp01(specialShotsRemaining / maxShots);
        ammoFill.localScale = new Vector3(
            ammoFill.localScale.x,
            ammoFill.localScale.y,
            ammoFill.localScale.z * normalized
        );
    }
}