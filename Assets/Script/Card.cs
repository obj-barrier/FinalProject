using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public enum Region { Red, Blue, Green }
    public enum Section { Top, Middle, Bottom }   
    public Region region;
    public Section section;
    public int damage = 1;
    private Image cardBackground;

    void Awake()
    {
        cardBackground = GetComponent<Image>();
        UpdateVisuals();
    }
    public void UpdateVisuals()
    {
        switch (region)
        {
            case Region.Red:
                cardBackground.color = new Color(1f, 0.2f, 0.2f, 1f); 
                break;
            case Region.Blue:
                cardBackground.color = new Color(0.2f, 0.2f, 1f, 1f); 
                break;
            case Region.Green:
                cardBackground.color = new Color(0.2f, 1f, 0.2f, 1f); 
                break;
        }
    }

}
