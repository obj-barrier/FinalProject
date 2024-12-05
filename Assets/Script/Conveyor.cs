using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Conveyor : MonoBehaviour
{
    public GameObject cardPrefab;
    public LineRenderer aimLine;
    public float spawnDelay = 2f;
    public float startY = 100f;
    public float cardSpacing = 200f;
    public float cardX = 100f;

    private bool isAiming = false;
    private int aimingIndex = -1;
    private Vector3 aimingSource;

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

        if (isAiming)
        {
            aimLine.enabled = true;
            aimLine.SetPosition(0, Camera.main.ScreenToWorldPoint(aimingSource));
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 1f;
            aimLine.SetPosition(1, Camera.main.ScreenToWorldPoint(mousePos));

            if (Input.GetAxis("Fire1") > 0)
            {
                TryAttack();
            }
        }
        else
        {
            aimLine.enabled = false;
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
        card.section = (Card.Section)Random.Range(1, 4);
        card.UpdateVisuals();
        
        Button button = newCard.GetComponent<Button>();
        button.onClick.AddListener(() => OnCardSelect(newCard, slotIndex));
        
        cardSlots[slotIndex] = newCard;
    }

    private void OnCardSelect(GameObject card, int slotIndex)
    {
        isAiming = true;
        aimingIndex = slotIndex;
        aimingSource = card.transform.position;
        aimingSource.z = 1f;
        SoundManager.Instance.PlayCardSelect();
    }

    private void TryAttack()
    {
        isAiming = false;
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 1f;
        Vector3 viewPos = Camera.main.transform.position;
        if(Physics.SphereCast(viewPos, 1f, Camera.main.ScreenToWorldPoint(mousePos) - viewPos, out RaycastHit hit, 100f))
        {
            if (hit.transform.TryGetComponent<EnemyController>(out var enemy))
            {
                Card attackCard = cardSlots[aimingIndex].GetComponent<Card>();
                if (attackCard.region == enemy.region && attackCard.section == enemy.section)
                {
                    enemy.Damage(attackCard.damage);
                    Destroy(attackCard.gameObject);
                    SoundManager.Instance.PlayCardAttack(attackCard.section);
                }
                else
                {
                    SoundManager.Instance.PlayCardAttackFail(); 
                }
            }
            else
            {
                SoundManager.Instance.PlayCardAttackFail(); 
            }
        }
        else
        {
            SoundManager.Instance.PlayCardAttackFail(); 
        }
    }

    public void DiscardAll()
    {
        foreach (GameObject card in cardSlots)
        {
            Destroy(card);
        }
    }
}