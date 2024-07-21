using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public int attackDamage = 10;
    Collider2D attackCollider;
    public Vector2 knockBack = Vector2.zero;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DamageComponent damage = collision.GetComponent<DamageComponent>();
        if (damage != null)
        {
            Debug.Log("test");
            Vector2 deliveredKnockback = transform.parent.localScale.x > 0 ? knockBack : new Vector2(-knockBack.x, knockBack.y); // Corrected here
            bool gotHit = damage.Hit(attackDamage, deliveredKnockback);
        }
    }
}
