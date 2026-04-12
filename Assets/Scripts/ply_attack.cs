using UnityEngine;

public class ply_attack : MonoBehaviour
{ public Animator animator;
public float atkrange=5f;
public Transform Atkpoint;
public LayerMask enemylayer;

   void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            A1();
        }

    }

    public void A1()
    {
        //Play an attack animation
        animator.SetTrigger("Punch");
       
       
        //Detect enemy in range
       Collider2D[] hitenemies= Physics2D.OverlapCircleAll(Atkpoint.position,atkrange,enemylayer);


        //Damage enemies
        foreach( Collider2D enemy in hitenemies)
        {
            enemy.GetComponent<en_movement>().Damage(40);
        }
    }

}
