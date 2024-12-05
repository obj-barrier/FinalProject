using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    internal void OnTriggerEnter(Collider other)
    {
        other.GetComponent<EnemyController>().DestroyTower(transform.position);
        GameManager.Instance.TowerDestroyed();
        SoundManager.Instance.PlayTowerDestroy();
        Destroy(gameObject);
    }
}
