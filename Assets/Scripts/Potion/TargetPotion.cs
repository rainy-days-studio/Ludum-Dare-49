using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetPotion : MonoBehaviour
{
    // Images and text for target potion
    [SerializeField]
    private Image liquid;
    private Text text;

    // Initialise variables
    private void Awake()
    {
        text = GetComponentInChildren<Text>();
        gameObject.SetActive(false);
    }

    // Set target colour
    public void setTargetColour(Colour colour)
    {
        liquid.color = colour.getUnityColour();
        text.text = colour.getName();
        gameObject.SetActive(true);
    }

    // Disable this target potion
    public void disable()
    {
        gameObject.SetActive(false);
    }
}
