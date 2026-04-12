using UnityEngine;

public class Attack : MonoBehaviour
{  public Animator animator;
   public int MaxHealth=100;
   int currentHealth;

    void Start()
    {
        currentHealth=MaxHealth;
    }
    

    public void Damage(int damage)
    {
        currentHealth-=damage;

        //play hurt animation


        if (currentHealth<= 0)
        {
            Die();
        }
    }

    void Die()
    {
       //animation
       animator.SetBool("IsDead",true);
       //remove enemy 
        GetComponent<Collider2D>().enabled=false;
        this.enabled=false;
    }

}
