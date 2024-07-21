using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage = 10; // Change float to int
    public Vector2 moveSpeed = new Vector2(3f, 0);

    public Vector2 knockBack = new Vector2(0, 0);
    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = new Vector2(moveSpeed.x * transform.localScale.x, moveSpeed.y); // Corrected here
    }

    private void OnTriggerEnter2D(Collider2D collision) // Corrected here
    {
        Debug.Log("projectile");
        DamageComponent damageComponent = collision.GetComponent<DamageComponent>();
        if (damageComponent != null)
        {
            Vector2 deliveredKnockback = transform.localScale.x > 0 ? knockBack : new Vector2(-knockBack.x, knockBack.y); // Corrected here
            bool gotHit = damageComponent.Hit(damage, deliveredKnockback); // Corrected here
            if(gotHit)
            {
                Destroy(gameObject);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
