using UnityEngine;

public class JarManager : MonoBehaviour
{
    public void CheckForWin()
    {
        if (transform.childCount <= 1)
        {
            Debug.Log("ALL JARS DESTROYED!");
        }
    }
}
