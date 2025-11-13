using UnityEngine;

public class RedGem : MonoBehaviour, IGem {
    [SerializeField]
    private int shotCount = 3; 

    public System.Type GetBehaviorType() {
        return typeof(RedBounceBehavior);
    }

    public int GetShotCount() {
        return shotCount;
    }
}