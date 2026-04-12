using UnityEngine;

public class en_movement : MonoBehaviour
{
    public Rigidbody2D rb;
    public Transform ply;
    public Animator animator;

    private bool isMoving;
    private bool isDead = false;

    public float moveSpeed;
    public float detectrange = 5f;
    public float attackRange = 1.5f;

    public int MaxHealth = 100;
    int currentHealth;

    public int attackDamage = 10;
    public float attackCooldown = 1f;
    private float lastAttackTime;
    PlayerHealth ph;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        currentHealth = MaxHealth;
        ph = ply.GetComponent<PlayerHealth>();
    }

    
    
    
    
    
    
    public void Damage(int damage)
    {
        if (isDead) return;

        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    
    
    
    
    
    
    
    void Die()
    {
        isDead = true;

        animator.SetBool("IsDead", true);

        rb.linearVelocity = Vector2.zero;
        rb.simulated = false;

        GetComponent<Collider2D>().enabled = false;

        Destroy(gameObject, 1f);
        GameManager.instance.AddKill();
    }






void FixedUpdate()
{
    if (isDead) return;

    //  STOP sensing player if inside house
    if (ph != null && ph.isInsideHouse)
    {
        rb.linearVelocity = Vector2.zero;
        isMoving = false;
        return;
    }

    Vector2 dir = (ply.position - transform.position).normalized;
    float dis = Vector2.Distance(ply.position, transform.position);

    if (dis > attackRange)
    {
        Move(dir);
    }
    else
    {
        Attack();
    }

    animator.SetFloat("moveX", dir.x);
    animator.SetFloat("moveY", dir.y);
    animator.SetBool("isMoving", isMoving);
}
 
 
 
 
 
    void Move(Vector2 dir)
{
    Vector2 separation = GetSeparationVector();

    Vector2 finalDir = (dir + separation).normalized;

    rb.linearVelocity = finalDir * moveSpeed;
    isMoving = true;
}

   
   
   
   
   
   
    void Attack()
{
    rb.linearVelocity = Vector2.zero;
    isMoving = false;

    if (Time.time >= lastAttackTime + attackCooldown)
    {
        lastAttackTime = Time.time;

        animator.SetTrigger("Attack");

        PlayerHealth ph = ply.GetComponent<PlayerHealth>();
        if (ph != null)
        {
            ph.TakeDamage(attackDamage);
        }
    }
}





Vector2 GetSeparationVector()
{
    float radius = 1.2f;
    float force = 2f;

    Collider2D[] nearby = Physics2D.OverlapCircleAll(transform.position, radius);

    Vector2 separation = Vector2.zero;

    foreach (Collider2D col in nearby)
    {
        if (col.gameObject != gameObject && col.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            Vector2 diff = transform.position - col.transform.position;
            separation += diff.normalized / diff.magnitude;
        }
    }

    return separation * force;
}
}
