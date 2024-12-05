using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public enum Region { Red, Green, Blue }
    public enum Section { Forest, Thunder, Bow, Sword, Castle }

    public Region region;
    public Section section;
    public int damage = 1;

    public GameObject iconSword;
    public GameObject iconBow;
    public GameObject iconThunder;

    private Image cardBackground;

    void Awake()
    {
        cardBackground = GetComponent<Image>();
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
        switch (section)
        {
            case Section.Thunder:
                iconThunder.SetActive(true);
                break;
            case Section.Bow:
                iconBow.SetActive(true);
                break;
            case Section.Sword:
                iconSword.SetActive(true);
                break;
        }
    }
}
