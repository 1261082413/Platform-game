using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirection), typeof(DamageComponent))]
public class KnightController : MonoBehaviour
{
    public float speed;//acceleration

    public float maxSpeed;
    public float walkStopRate = 0.05f;
    public DetectionZone attactZone;
    public DetectionZone cliffZone;
    Rigidbody2D rb;
    TouchingDirection touchingDirections;
    Animator animator;
    public enum WalkableDirection { Right, Left }

    private WalkableDirection _walkDirection;
    private Vector2 walkDirectionVector = Vector2.right;
    DamageComponent damage;

    public WalkableDirection WalkDirection
    {
        get { return _walkDirection; }
        set
        {
            if (_walkDirection != value)
            {
                gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x * -1, gameObject.transform.localScale.y);
                if (value == WalkableDirection.Right)
                {
                    walkDirectionVector = Vector2.right;
                }
                else if (value == WalkableDirection.Left)
                {
                    walkDirectionVector = Vector2.left;
                }
            }
            _walkDirection = value;
        }
    }

    public bool _hasTarget = false;

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

    public float attackCoolDown
    {
        get
        {
            return animator.GetFloat("attackCoolDown");
        }
        private set
        {
            animator.SetFloat("attackCoolDown",Mathf.Max(value,0));
        }
    }

    public void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        touchingDirections = GetComponent<TouchingDirection>();
        animator = GetComponent<Animator>();
        damage = GetComponent<DamageComponent>();
    }

    void Update()
    {
        hasTarget = attactZone.detectedColliders.Count > 0;
        if(attackCoolDown>0)
        {
            attackCoolDown -= Time.deltaTime;
        }
        
    }

    private void LateUpdate()
    {
        if (touchingDirections.isGrounded && touchingDirections.isOnWall )
        {
            Debug.Log("FixedUpdate call Flipdirection");
            FlipDirection();
        }
        if (!damage.isHit)
        {
            if (canMove && touchingDirections.isGrounded)
            {
                
                rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x + (speed*walkDirectionVector.x*Time.fixedDeltaTime),-maxSpeed,maxSpeed), rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(Mathf.Lerp(rb.velocity.x, 0, walkStopRate), rb.velocity.y);
            }
        }
    }

    public void FlipDirection()
    {
        Debug.Log("FlipDirection");
        if (WalkDirection == WalkableDirection.Right)
        {
            WalkDirection = WalkableDirection.Left;
        }
        else if (WalkDirection == WalkableDirection.Left)
        {
            WalkDirection = WalkableDirection.Right;
        }
        
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame

    public void onHit(int damage, Vector2 knockBack)
    {
        rb.velocity = new Vector2(knockBack.x, rb.velocity.y * knockBack.y);
    }
    
    public void OnCliffDetected()
    {
        if(touchingDirections.isGrounded)
        {
            FlipDirection();
        }
    }
}
