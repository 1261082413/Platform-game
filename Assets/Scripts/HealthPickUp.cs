using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickUp : MonoBehaviour
{
    public int healthRestore = 20;

    public Vector3 spinRotationSpeed = new Vector3(0,100,0);
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        DamageComponent damage = collision.GetComponent<DamageComponent>();
        if(damage)
        {
            bool wasHealed =  damage.Heal(healthRestore);
            if(wasHealed)
            {
                Destroy(gameObject);
            }
            
        }
    }

    // Update is called once per frame
    public void Update()
    {
        transform.eulerAngles += spinRotationSpeed * Time.deltaTime;
    }
}
