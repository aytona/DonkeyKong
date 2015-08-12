using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class JumpmanControls : MonoBehaviour {

    Rigidbody2D rb2d;
    [SerializeField]private Transform groundCheck = null;
    
    public bool isOnGround = false;                         // Feet touching the ground
    public bool hammerTime = false;                         // Hammer picked up
    public bool isOnLadder = false;                         // On a ladder trigger
    public int lives = 3;

    private bool standing;                                  // Mario's speed is 0
    private float jumpForce = 5f;                           // Mario's jump height
    private float speed = 10f;                              // Mario's walking speed
    private float climbSpeed = 5f;                          // Mario's climbing speed
    private Vector2 maxVelocity = new Vector2(3, 5);        // Max walking and climbing speed
    private Controller controller;                          // Input detector script
    private Animator animator;                              // Mario's animator

    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        controller = GetComponent<Controller>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        CheckGround();

        var forceX = 0f;
        var forceY = 0f;

        var absVelX = Mathf.Abs(rb2d.velocity.x);
        var absVelY = Mathf.Abs(rb2d.velocity.y);

        if (absVelY < .2f)
        {
            standing = true;
        }
        else
        {
            standing = false;
        }

        // Moving Left or Right
        if (controller.moving.x != 0)
        {
            if (absVelX < maxVelocity.x)
            {
                forceX = standing ? speed * controller.moving.x : (speed * controller.moving.x);
                transform.localScale = new Vector3(forceX > 0 ? -3 : 3, 3, 1);
            }
            if (hammerTime == false)
            {
                this.animator.SetInteger("AnimState", 1);
            }
            else if (hammerTime == true)
            {
                this.animator.SetInteger("AnimState", 6);
            }
        }
        // Standing
        else
        {
            if (hammerTime == false)
            {
                this.animator.SetInteger("AnimState", 0);
            }
            else if (hammerTime == true)
            {
                this.animator.SetInteger("AnimState", 5);
            }
        }
        // Moving Up or Down, Only if Mario is on a ladder trigger
        if (controller.moving.y > 0 && isOnLadder == true)
        {
            if (absVelY < maxVelocity.y)
            {
                forceY = controller.moving.y * climbSpeed;
                this.animator.SetInteger("AnimState", 4);
            }
            this.animator.SetInteger("AnimState", 3);
        }
        else if (absVelY > 0 && isOnGround == true)
        {
            this.animator.SetInteger("AnimState", 0);
        }
        // Jumping only if Mario is on the ground
        if (isOnGround == true && Input.GetKeyDown(KeyCode.Space))
        {
            this.rb2d.AddForce(Vector2.up * jumpForce);
            this.animator.SetInteger("AnimState", 2);
        }

        rb2d.AddForce(new Vector2(forceX, forceY));
    }

    // Checks if the player is in contact with the ground
    private void CheckGround()
    {
        Collider2D collider = Physics2D.OverlapPoint(this.groundCheck.transform.position);
        this.isOnGround = (collider != null);
    }

    // Colliders
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Hammer")
        {
            Destroy(other.gameObject);
            hammerTime = true;
            StartCoroutine(hammerTimer());
        }
        if (hammerTime == true && other.gameObject.tag == "Enemy")
        {
            PlayerData.Instance.Score += 100;
            Destroy(other.gameObject);
        }
        else if (hammerTime == false && other.gameObject.tag == "Enemy")
        {
            this.animator.SetTrigger("DeathTrigger");
        }
        if (other.gameObject.tag == "WinLadder" && Input.GetKey(KeyCode.UpArrow))
        {
            this.animator.SetTrigger("WinTrigger");
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Ladder")
        {
            isOnLadder = true;
            if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow))
            {
                this.animator.SetInteger("AnimState", 3);
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Ladder")
        {
            isOnLadder = false;
            this.animator.SetInteger("AnimState", 0);
        }
        if (other.gameObject.tag == "Score")
        {
            PlayerData.Instance.Score += 100;
            Destroy(other.gameObject);
        }
    }

    void WinTransition()
    {
        Application.LoadLevel("Win");
    }

    void LoseTransition()
    {
        if (PlayerData.Instance.Lives > 0)
        {
            Application.LoadLevel(Application.loadedLevel);
        }
        else if (PlayerData.Instance.Lives == 0)
        {
            Application.LoadLevel("Lose");
        }
    }

    // Hammer Timer
    private IEnumerator hammerTimer()
    {
        yield return new WaitForSeconds(5);
        hammerTime = false;
    }
}
