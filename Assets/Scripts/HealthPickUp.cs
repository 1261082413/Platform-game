using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickUp : MonoBehaviour
{
    public int healthRestore = 20;

    public Vector3 spinRotationSpeed = new Vector3(0,100,0);

    AudioSource pickupSource;

    private void Awake()
    {
        pickupSource = GetComponent<AudioSource>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        DamageComponent damage = collision.GetComponent<DamageComponent>();
        if(damage && damage.Health < damage.MaxHealth)
        {
            bool wasHealed =  damage.Heal(healthRestore);
            if(wasHealed)
            {
                if(pickupSource)
                {
                    AudioSource.PlayClipAtPoint(pickupSource.clip,gameObject.transform.position,pickupSource.volume);
                }
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
