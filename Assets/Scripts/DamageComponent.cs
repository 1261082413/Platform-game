using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DamageComponent : MonoBehaviour
{
    public UnityEvent<int, Vector2> damageableHit;
    Animator animator;
    public int _maxhealth = 100;

    [SerializeField]
    public int health
    {
        get
        {
            return _maxhealth;
        }
        set
        {
            _maxhealth = value;
        }
    }

    public int _health = 100;

    [SerializeField]
    public int Health
    {
        get
        {
            return _health;
        }
        set
        {
            _health = value;
            if (_health <= 0)
            {
                isAlive = false;
            }
        }
    }

    [SerializeField]
    private bool _isAlive = true;
    [SerializeField]
    private bool isInvisible = false;

    private float timeSinceHit = 0;
    public float invincibilityTime = 0.25f;

    public bool isAlive
    {
        get
        {
            return _isAlive;
        }
        set
        {
            _isAlive = value;
            animator.SetBool("isAlive", value);
        }
    }

    public bool isHit
    {
        get
        {
            return animator.GetBool("isHit");
        }
        private set
        {
            animator.SetBool("isHit", value);
        }
    }

    public void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Update()
    {
        if (isInvisible)
        {
            if (timeSinceHit >= invincibilityTime)
            {
                isInvisible = false;
                timeSinceHit = 0;
            }
            timeSinceHit += Time.deltaTime;
        }
    }

    public bool Hit(int damage, Vector2 knockBack)
    {
        if (isAlive && !isInvisible)
        {
            Health -= damage;
            isInvisible = true;
            animator.SetTrigger("hit");
            isHit = true;
            damageableHit?.Invoke(damage, knockBack);
            CharacterEvents.characterDamaged.Invoke(gameObject, damage);

            return true;
        }
        return false;
    }

    public bool Heal(int healthRestore)
    {
        if(isAlive && Health < _maxhealth)
        {
            int maxHeal = Mathf.Max(_maxhealth-Health,0);
            int actualHeal = Mathf.Min(healthRestore,_maxhealth);
            Health += actualHeal;

            CharacterEvents.characterHealed(gameObject,healthRestore);
            return true;
        }
        return false;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
}