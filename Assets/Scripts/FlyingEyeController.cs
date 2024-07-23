using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEyeController : MonoBehaviour
{
    public float flightSpeed = 2f;
    public DetectionZone biteDetectionZone;
    public List<Transform> wayPoints;
    Animator animator;
    Rigidbody2D rb;
    DamageComponent damage;
    Transform nextWayPoint;
   public int wayPointNum = 0;

    public bool _hasTarget = false;
    public float wayPointReachedDistance;

    public Collider2D deathCollider;

    public bool hasTarget
    {
        get
        {
            return _hasTarget;
        }
        private set
        {
            _hasTarget = value;
            animator.SetBool("hasTarget", value);
        }
    }

    public bool canMove
    {
        get
        {
            return animator.GetBool("canMove");
        }
    }

    public void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        damage = GetComponent<DamageComponent>();
    }

    // Start is called before the first frame update
    void Start()
    {
        nextWayPoint = wayPoints[wayPointNum];
    }

    private void OnEnable()
    {
        damage.damagebleDeath.AddListener(OnDeath); // Corrected to AddListener
    }

    // Update is called once per frame
    void Update()
    {
        hasTarget = biteDetectionZone.detectedColliders.Count > 0;
    }

    private void FixedUpdate()
    {
        if (damage.isAlive)
        {
            if (canMove)
            {
                Flight();
            }
            else
            {
                rb.velocity = Vector3.zero;
            }
        }
    }

    private void Flight()
    {
        Vector2 directionToWayPoint = (nextWayPoint.position - transform.position).normalized;
        float distance = Vector2.Distance(nextWayPoint.position, transform.position);
        rb.velocity = directionToWayPoint * flightSpeed;
        UpdateDirection();
        if (distance <= wayPointReachedDistance)
        {
            wayPointNum++;
            if (wayPointNum >= wayPoints.Count)
            {
                wayPointNum = 0;
            }
            nextWayPoint = wayPoints[wayPointNum];
        }
    }


    private void UpdateDirection()
    {
        
        Vector3 locScale = transform.localScale; // Corrected to localScale
        if (transform.localScale.x > 0)
        {
            if (rb.velocity.x < 0)
            {
                transform.localScale = new Vector3(-1 * locScale.x, locScale.y, locScale.z);
            }
            else
            {
                if (rb.velocity.x > 0)
                {
                    transform.localScale = new Vector3(1 * locScale.x, locScale.y, locScale.z); // Adjusted to flip correctly
                }
            }
        }
        else
        {
            if (rb.velocity.x > 0)
            {
                transform.localScale = new Vector3(-1 * locScale.x, locScale.y, locScale.z); // Adjusted to flip correctly
            }
            else
            {
                if (rb.velocity.x < 0)
                {
                    transform.localScale = new Vector3(1 * locScale.x, locScale.y, locScale.z); // Adjusted to flip correctly
                }
            }
        }
    }

   public void OnDeath()
    {
        rb.gravityScale = 2f;
        rb.velocity = new Vector2(0, rb.velocity.y);
        deathCollider.enabled = true;
    }
}
