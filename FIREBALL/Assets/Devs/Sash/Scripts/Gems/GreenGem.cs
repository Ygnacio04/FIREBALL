using UnityEngine;

public class GreenGem : MonoBehaviour, IGem
{
    [SerializeField]
    private int shotCount = 6;

    public System.Type GetBehaviorType()
    {
        return typeof(GreenBehavior);
    }

    public int GetShotCount()
    {
        return shotCount;
    }
}