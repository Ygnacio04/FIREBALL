using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class InteractableFuncs : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField]
    private Color hoverColor = Color.blue;

    [SerializeField]
    private Color selectedColor = Color.green;
    
    [SerializeField]
    private Color activatedColor = Color.red;

    private Color originalColor;

    private Material material;

    void Start()
    {
        
        material = GetComponent<MeshRenderer>().material;
        originalColor = material.color;
    }

    public void SetHoverColor()
    {
        if (!GetComponent<XRGrabInteractable>().isSelected)
            ChangeColor(hoverColor);
    }

    public void SetSelectedColor()
    {
        ChangeColor(selectedColor);
    }

    public void SetActivatedColor()
    {
        ChangeColor(activatedColor);
    }

    public void SetOriginalColor()
    {
        if (!GetComponent<XRGrabInteractable>().isSelected)
            ChangeColor(originalColor);
    }

    void ChangeColor(Color color)
    {
        material.color = color;
    }
}
