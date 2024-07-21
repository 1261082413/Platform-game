using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; 

public class DetectionZone : MonoBehaviour
{
    public UnityEvent NoCollidersRemain; 
    public List<Collider2D> detectedColliders = new List<Collider2D>();
    Collider2D col;

    private void Awake()
    {
        col = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("OnTriggerEnter2D");
        detectedColliders.Add(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("OnTriggerExit2D");
        detectedColliders.Remove(collision);
        if (detectedColliders.Count <= 0)
        {
            Debug.Log("NoCollidersRemain");
            KnightController myController= gameObject.GetComponentInParent<KnightController>();
            if(myController !=null)
            {
                myController.FlipDirection();
            }
        }
      
    }

    

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
