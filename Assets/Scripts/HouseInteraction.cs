using UnityEngine;

public class HouseInteraction : MonoBehaviour
{
    public Transform insidePoint;
    public Transform outsidePoint;

    private bool nearDoor = false;
    private bool isInside = false;

    public GameObject player;

    float insideTimer = 0f;
    public float maxInsideTime = 5f;

    // ENTRY COOLDOWN
    public float enterCooldown = 3f;
    private float nextEnterTime = 0f;

    void Update()
    {
        // ENTER / EXIT
        if (nearDoor && Time.time >= nextEnterTime && Input.GetKeyDown(KeyCode.E))
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

        // INSIDE TIMER
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

        // Hide player
        player.GetComponent<SpriteRenderer>().enabled = false;

        // Disable physics
        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
        rb.simulated = false;
        rb.linearVelocity = Vector2.zero;

        // Disable movement
        Playermovement pm = player.GetComponent<Playermovement>();
        pm.enabled = false;

        // Healing
        PlayerHealth ph = player.GetComponent<PlayerHealth>();
        ph.StartHealing();
        ph.isHealing = true;
        ph.isInsideHouse = true;

        Debug.Log("Entered House");
    }

    void ExitHouse()
    {
        isInside = false;
        insideTimer = 0f;

        // START COOLDOWN
        nextEnterTime = Time.time + enterCooldown;

        // Move player outside
        player.transform.position = outsidePoint.position;

        // Enable visuals
        player.GetComponent<SpriteRenderer>().enabled = true;

        // Enable physics
        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
        rb.simulated = true;
        rb.linearVelocity = Vector2.zero;

        // Enable movement
        Playermovement pm = player.GetComponent<Playermovement>();
        pm.enabled = true;

        // Reset animation
        Animator anim = player.GetComponent<Animator>();
        anim.SetBool("isMoving", false);

        // Reset healing
        PlayerHealth ph = player.GetComponent<PlayerHealth>();
        ph.isInsideHouse = false;
        ph.isHealing = false;

        Debug.Log("Exited House");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
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