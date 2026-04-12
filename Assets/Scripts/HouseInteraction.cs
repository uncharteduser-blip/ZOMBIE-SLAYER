using UnityEngine;

public class HouseInteraction : MonoBehaviour
{
    public Transform insidePoint;
    public Transform outsidePoint; // door position

    private bool nearDoor = false;
    private bool isInside = false;

    public GameObject player;
    float insideTimer = 0f;
    public float maxInsideTime = 5f; // seconds




    void Update()
{
    if (nearDoor && Input.GetKeyDown(KeyCode.E))
    {
        if (!isInside)
        {
            EnterHouse();
        }
        else
        {
            ExitHouse();
        }
    }

    // LIMIT STAY TIME
    if (isInside)
    {
        insideTimer += Time.deltaTime;

        if (insideTimer >= maxInsideTime)
        {
            ExitHouse();
        }
    }
}
    

   void EnterHouse()
{
    isInside = true;

    player.transform.position = insidePoint.position;

    player.GetComponent<SpriteRenderer>().enabled = false;
    player.GetComponent<Rigidbody2D>().simulated = false;

    player.GetComponent<PlayerHealth>().StartHealing(); 
    player.GetComponent<PlayerHealth>().isHealing = true;
    player.GetComponent<PlayerHealth>().isInsideHouse = true;
}



  void ExitHouse()
{
    isInside = false;
    insideTimer = 0f;
    Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
    rb.simulated = true;
    rb.linearVelocity = Vector2.zero;
    // Move player outside
    player.transform.position = outsidePoint.position;

    // Enable visuals
    player.GetComponent<SpriteRenderer>().enabled = true;

    // Enable movement
    Playermovement pm = player.GetComponent<Playermovement>();
    pm.enabled = true;

    //  RESET MOVEMENT STATE
   // Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
    //rb.linearVelocity = Vector2.zero;

    // Reset animation (optional but good)
    Animator anim = player.GetComponent<Animator>();
    anim.SetBool("isMoving", false);

    // Reset health state
    PlayerHealth ph = player.GetComponent<PlayerHealth>();
    ph.isInsideHouse = false;
    ph.isHealing = false;

    Debug.Log("Exited House");
}

    void OnTriggerEnter2D(Collider2D other)
{   Debug.Log("TRIGGER WORKING");
    if (other.CompareTag("Player"))
    {
        nearDoor = true;
        Debug.Log("Near Door TRUE");
    }
}

void OnTriggerExit2D(Collider2D other)
{
    if (other.CompareTag("Player"))
    {
        nearDoor = false;
        Debug.Log("Near Door FALSE");
    }
}
}