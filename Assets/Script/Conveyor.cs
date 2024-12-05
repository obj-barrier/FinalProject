using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class Conveyor : MonoBehaviour
{
    public GameObject cardPrefab;
    public float spawnDelay = 2f;
    public float startY = 100f;
    public float cardSpacing = 200f; 
    public float cardX = 100f;    
    private float spawnTimer = 0f;
    private GameObject[] cardSlots;
    private Vector2[] fixedPositions;

    void Start()
    {
        cardSlots = new GameObject[5];
        fixedPositions = new Vector2[5];
        for (int i = 0; i < 5; i++)
        {
            fixedPositions[i] = new Vector2(cardX, startY + (i * cardSpacing));
        }
        for (int i = 0; i < cardSlots.Length; i++)
        {
            MakeCard(i);
        }
    }

    void Update()
    {
        for (int i = 0; i < cardSlots.Length; i++)
        {
            if (cardSlots[i] == null && spawnTimer <= 0)
            {
                MakeCard(i);
                spawnTimer = spawnDelay;
                break; 
            }
        }
        if (spawnTimer > 0)
        {
            spawnTimer -= Time.deltaTime;
        }
    }
    private void MakeCard(int slotIndex)
    {
        GameObject newCard = Instantiate(cardPrefab, transform);
        RectTransform rect = newCard.GetComponent<RectTransform>();
        rect.anchorMin = new Vector2(0, 0);
        rect.anchorMax = new Vector2(0, 0);
        rect.pivot = new Vector2(0.5f, 0.5f);
        rect.anchoredPosition = fixedPositions[slotIndex];
        
        Card card = newCard.GetComponent<Card>();
        card.region = (Card.Region)Random.Range(0, 3);
        card.section = (Card.Section)Random.Range(0, 3);
        card.UpdateVisuals();
        
        Button button = newCard.GetComponent<Button>();
        button.onClick.AddListener(() => OnCardUsed(newCard, slotIndex));
        
        cardSlots[slotIndex] = newCard;
    }

    private void OnCardUsed(GameObject usedCard, int slotIndex)
    {
        Destroy(usedCard);
        cardSlots[slotIndex] = null;
    }
}