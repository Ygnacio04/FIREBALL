using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]

public class RedBounceBehavior : MonoBehaviour, IProjectileBehavior {
    private PhysicsMaterial bouncyMaterial;
    private string targetTag;
    public int maxBounces = 5;
    private int currentBounces = 0;
    private Collider objectCollider;

    public void Initialize(WandController wand) {
        objectCollider = GetComponent<Collider>();
        this.bouncyMaterial = wand.redBouncyMaterial;
        this.targetTag = wand.redBallTargetTag;

        if (bouncyMaterial != null) objectCollider.material = bouncyMaterial;
        currentBounces = 0;
    }

    void OnCollisionEnter(Collision collision) {
        currentBounces++;

        bool hitTarget = collision.gameObject.CompareTag(targetTag);
        bool outOfBounces = currentBounces >= maxBounces;

        if (hitTarget || outOfBounces) {
            if (objectCollider != null) objectCollider.material = null; 
            FireballPoolManager.Instance.ReturnFireball(this.gameObject);
        }
    }
}