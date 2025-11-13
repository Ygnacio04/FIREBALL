using UnityEngine;

public class BlueGem : MonoBehaviour, IGem {
    [SerializeField]
    private int shotCount = 3;
    public System.Type GetBehaviorType() {
        return typeof(BlueFreezeBehavior);
    }

    public int GetShotCount() {
        return shotCount;
    }
}