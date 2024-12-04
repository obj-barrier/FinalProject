using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallController : MonoBehaviour
{
    internal void OnTriggerEnter(Collider other)
    {
        other.GetComponent<EnemyController>().Damage(1);
        Destroy(gameObject);
    }
}
