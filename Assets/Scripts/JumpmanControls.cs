using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class JumpmanControls : MonoBehaviour {

    Rigidbody2D rb2d;
    [SerializeField]private Animator animator = null;
    // [SerializeField]private GameObject spriteContainer = null;
    [SerializeField]private Transform groundCheck = null;
    
    private bool isOnGround = false;                    // Feet touching the ground
    private bool hammerTime = false;                    // Hammer picked up
    private bool isOnLadder = false;                    // On a ladder trigger
    private bool standing;                              // Mario's speed is 0
    private float speed = 10f;                          // Mario's walking speed
    private float climbSpeed = 5f;                      // Mario's climbing speed
    private Vector2 maxVelocity = new Vector2(3, 5);    // Max walking and climbing speed
    private Controller controller;                      // Input detector script

    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        controller = GetComponent<Controller>();
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

        if (controller.moving.x != 0)
        {
            if (absVelX < maxVelocity.x)
            {
                forceX = standing ? speed * controller.moving.x : (speed * controller.moving.x);
                transform.localScale = new Vector3(forceX > 0 ? 1 : -1, 1, 1);
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
        if (other.gameObject.tag == "WinLadder" && Input.GetKey(KeyCode.UpArrow))
        {
            this.animator.SetTrigger("WinTrigger");
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Ladder" && controller.moving.y > 0)
        {
            isOnLadder = true;
            this.animator.SetInteger("AnimState", 3);
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

    void winTransition()
    {
        Application.LoadLevel("Win");
    }

    // Hammer Timer
    private IEnumerator hammerTimer()
    {
        yield return new WaitForSeconds(5);
        hammerTime = false;
    }
}
