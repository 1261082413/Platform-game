using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchingDirection : MonoBehaviour
{
    public ContactFilter2D castFilter;
    public ContactFilter2D castFilterWall;
    public float groundDistance = 0.05f;
    public float WallCheckDistance = 0.2f;
    public float CeilingDistance = 0.05f;
    CapsuleCollider2D touchingCol;
    Animator animator;
    RaycastHit2D[] groundHits = new RaycastHit2D[5];
    RaycastHit2D[] wallHits = new RaycastHit2D[5];
    RaycastHit2D[] ceilingHits = new RaycastHit2D[5];

    public bool _isGrounded = true;
    [SerializeField]
    public bool isGrounded
    {
        get
        {
            return _isGrounded;
        }
        private set
        {
            _isGrounded = value;
            animator.SetBool("isGrounded", value);
        }
    }

    [SerializeField]
    private bool _isOnWall;
    private Vector2 wallCheckDirection => gameObject.transform.localScale.x > 0 ? Vector2.right : Vector2.left;
    public bool isOnWall
    {
        get
        {
            return _isOnWall;
        }
        private set
        {
            _isOnWall = value;
            animator.SetBool("isOnWall", value);
        }
    }

    [SerializeField]
    private bool _isOnCeiling;
    public bool isOnCeiling
    {
        get
        {
            return _isOnCeiling;
        }
        private set
        {
            _isOnCeiling = value;
            animator.SetBool("isOnCeiling", value);
        }
    }

    private void Awake()
    {
        touchingCol = GetComponent<CapsuleCollider2D>();
        animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
		castFilterWall.SetLayerMask(LayerMask.GetMask("Wall"));
		castFilter.SetLayerMask(LayerMask.GetMask("Ground"));
    }

    // Update is called once per frame
    void LateUpdate()
    {
        isGrounded = touchingCol.Cast(Vector2.down, castFilter, groundHits, groundDistance) > 0;
        isOnWall = touchingCol.Cast(wallCheckDirection, castFilterWall, wallHits, WallCheckDistance) > 0;
        isOnCeiling = touchingCol.Cast(Vector2.up, castFilter, ceilingHits, CeilingDistance) > 0;
    }
}
