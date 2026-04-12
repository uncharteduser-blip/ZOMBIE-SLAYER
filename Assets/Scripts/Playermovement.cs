using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Playermovement : MonoBehaviour
{
    public float speed = 5f;
    public float sprintMultiplier = 2f;

    private Rigidbody2D rb;
    private Vector2 moveInput;
    private bool isSprinting;
     public Animator animator;
    private bool isMoving;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    // Update is called once per frame
    void FixedUpdate()
    {Debug.Log("Movement script running");
      if (Input.GetKey(KeyCode.LeftShift))
        {
            isSprinting=true;
        }
      else
         {
           isSprinting=false;
         }

      
      
      
      
      
        if (rb.linearVelocity != new Vector2(0,0))
          {
           isMoving=true; 
          }
        else
         {
            isMoving=false;
         }
      
      
      
      
      
        animator.SetFloat("moveX",moveInput.x);
        animator.SetFloat("moveY",moveInput.y);
        animator.SetBool("isMoving",isMoving);
        animator.SetBool("isSprinting",isSprinting);
      
      
      
      float moveSpeed=isSprinting ? speed*sprintMultiplier :speed;
      rb.linearVelocity=moveInput*moveSpeed;




      

    }
    void OnEnable()
{
    GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
}

   
   
   
   
   
    void Update()
    {
      if (moveInput != Vector2.zero)
       {
         animator.SetFloat("LastMoveX", moveInput.x);
         animator.SetFloat("LastMoveY", moveInput.y);
        }
    }




    public void OnMove(InputAction.CallbackContext context)
    {
     moveInput = context.ReadValue<Vector2>();
    }
 



   

}
