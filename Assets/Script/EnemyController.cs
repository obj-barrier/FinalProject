using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed;
    public float towerRadius;

    private Vector3 center;
    private int hp = 3;
    private float angularSpeed;
    private bool inside = false;

    // Start is called before the first frame update
    internal void Start()
    {
        center = new Vector3(0f, transform.position.y, 0f);
        angularSpeed = speed * 180f / Mathf.PI;
    }

    internal void Update()
    {
        if (inside)
        {
            transform.RotateAround(center, Vector3.up, angularSpeed * Time.deltaTime / towerRadius);
        }
        else
        {
            transform.position += speed * Time.deltaTime * (center - transform.position).normalized;
            if (Vector3.Distance(transform.position, center) < towerRadius)
            {
                inside = true;
            }
        }
    }

    private void UpdateSize()
    {
        transform.localScale = hp * Vector3.one;
    }

    public void SetHp(int hp)
    {
        this.hp = hp;
        UpdateSize();
    }

    public void Damage(int damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            Destroy(gameObject);
            return;
        }
        UpdateSize();
    }

    public void DestroyTower(Vector3 towerPos)
    {
        hp -= 1;
        if (hp <= 0)
        {
            Destroy(gameObject);
            return;
        }
        UpdateSize();

        if (!inside)
        {
            transform.position = towerPos;
            inside = true;
        }
    }
}
