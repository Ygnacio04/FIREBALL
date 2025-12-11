using UnityEngine;
using UnityEngine.Events;

public class Lever : MonoBehaviour
{

    [SerializeField]
    private UnityEvent OnLeverOn;

    [SerializeField]
    private UnityEvent OnLeverOff;

    private void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.name)
        {
            case "LeverOn":
                OnLeverOn.Invoke();
                break;
            case "LeverOff":
                OnLeverOff.Invoke();
                break;
        }

        }
}
