using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DiscardButton : MonoBehaviour
{
    public TextMeshProUGUI discardText;
    public float cooldown;

    private float nextActive;
    private Button button;

    internal void Start()
    {
        button = GetComponent<Button>();
    }

    internal void Update()
    {
        float timeRem = nextActive - Time.timeSinceLevelLoad;
        if (timeRem < 0)
        {
            discardText.text = "Discard All";
            button.interactable = true;
        }
        else
        {
            discardText.text = Mathf.Ceil(timeRem).ToString();
        }
    }

    public void StartCntDown()
    {
        button.interactable = false;
        nextActive = Time.timeSinceLevelLoad + cooldown;
    }
}
