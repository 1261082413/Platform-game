using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowAttack : MonoBehaviour
{
    public Transform launchPoint;
    public GameObject projectilePrefab;
    // Start is called before the first frame update

    public void FireProjectile()
    {
        GameObject projectile = Instantiate(projectilePrefab,launchPoint.position,projectilePrefab.transform.rotation);
        Vector3 origScale = projectile.transform.localScale;
        projectile.transform.localScale = new Vector3(
            origScale.x*transform.localScale.x >0 ?1:-1,
            origScale.y,
            origScale.z
        );
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
