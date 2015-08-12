using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class JumpmanControls : MonoBehaviour {
    Rigidbody2D rb2d;
    [SerializeField]private Animator animator = null;
    [SerializeField]private GameObject spriteContainer = null;
    [SerializeField]private Transform groundCheck = null;
    //[SerializeField]private Transform ladderCheck = null;
    
    private Vector2 direction = Vector2.zero;
    private float walkForce = 8f;
    private float maxWalkSpeed = 3f;
    private float jumpForce = 50f;
    private float climbForce = 4f;
    private float maxClimbSpeed = 2f;
    private bool isOnGround = false;
    private bool hammerTime = false;
    private bool isOnLadder = false;

    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Physics based update
    void FixedUpdate()
    {
        Walk(this.direction);
        OrientCharacter(this.direction);
        Jump(this.direction);
        if (isOnLadder == true)
        {
            Climb(this.direction);
        }
    }

    void Update()
    {
        ProcessInput();
        CheckGround();
        if (isOnGround == true)
        {
            this.animator.SetBool("onGround", true);
        }
        if (hammerTime == true)
        {
            this.animator.SetBool("hammerTime", true);
        }
        else if (hammerTime == false)
        {
            this.animator.SetBool("hammerTime", false);
        }
    }

    // Processes the different types of inputs available
    private void ProcessInput()
    {
        ProcessWalking();
        ProcessJump();
        if (isOnLadder == true)
        {
            ProcessClimb();
            //this.rb2d.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionX;
        }
        //else if (isOnLadder == false)
        //{
        //    this.rb2d.constraints = RigidbodyConstraints2D.FreezeRotation;
        //}
    }

    // Changes the x vector depending on the keys pressed
    private void ProcessWalking()
    {
        this.direction = Vector2.zero;
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            this.direction += new Vector2(-1, 0);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            this.direction += new Vector2(1, 0);
        }
    }

    // Pushes the player upwards on the y-axis
    private void ProcessJump()
    {
        if (Input.GetKey(KeyCode.Space) && isOnGround == true && hammerTime == false)
        {
            this.direction.y = 1;
        }
    }

    // Player goes up or down the ladder
    private void ProcessClimb()
    {
        if (Input.GetKey(KeyCode.UpArrow) && isOnLadder == true)
        {
            this.direction += new Vector2(0, 1);
            //this.animator.SetTrigger("Climbing");
        }
        else if (Input.GetKey(KeyCode.DownArrow) && isOnLadder == true)
        {
            this.direction += new Vector2(0, -1);
            //this.animator.SetTrigger("Climbing");
        }
    }

    // Orients the character to the proper direction they are walking towards to
    private void OrientCharacter(Vector2 direction)
    {
        Vector3 spriteScale = this.spriteContainer.transform.localScale;
        // Movement direction towards the right
        if (direction.x > 0)
        {
            spriteScale.x = -3;
        }
        // Movement direction towards the left
        else if (direction.x < 0)
        {
            spriteScale.x = 3;
        }
        this.spriteContainer.transform.localScale = spriteScale;
    }

    // Walking function
    private void Walk(Vector2 direction)
    {
        this.rb2d.AddForce(direction * this.walkForce);
        float horizontalSpeed = Mathf.Abs(this.rb2d.velocity.x);
        if (Mathf.Abs(horizontalSpeed) > this.maxWalkSpeed)
        {
            Vector2 newVelocity = this.rb2d.velocity;
            float multiplier = (this.rb2d.velocity.x > 0) ? 1 : -1;
            newVelocity.x = multiplier * maxWalkSpeed;
            this.rb2d.velocity = newVelocity;
        }
        this.animator.SetFloat("HorizontalSpeed", horizontalSpeed);
    }

    // Climbing function
    private void Climb(Vector2 direction)
    {
        this.rb2d.AddForce(direction * this.climbForce);
        float verticalSpeed = Mathf.Abs(this.rb2d.velocity.y);
        if (Mathf.Abs(verticalSpeed) > this.maxClimbSpeed)
        {
            Vector2 newVelocity = this.rb2d.velocity;
            float multiplier = (this.rb2d.velocity.y > 0) ? 1 : -1;
            newVelocity.y = multiplier * maxClimbSpeed;
            this.rb2d.velocity = newVelocity;
        }
        this.animator.SetFloat("VerticalSpeed", verticalSpeed);
    }

    // Jumping function
    private void Jump(Vector2 direction)
    {
        if (direction.y > 0)
        {
            this.rb2d.AddForce(Vector2.up * this.jumpForce);
            direction.y = 0;
            this.animator.SetBool("onGround", false);
        }
    }

    // Checks if the player is in contact with the ground
    private void CheckGround()
    {
        Collider2D collider = Physics2D.OverlapPoint(this.groundCheck.transform.position);
        this.isOnGround = (collider != null);
    }
    
    // Check if the player is in contact with the ladder
    //private void CheckLadder()
    //{
    //    Collider2D collider = Physics2D.OverlapPoint(this.ladderCheck.transform.position);
    //    this.isOnLadder = (gameObject.tag == "Ladder");
    //}

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
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "WinLadder" && Input.GetKeyDown(KeyCode.UpArrow))
        {
            this.animator.SetTrigger("WinTrigger");
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Ladder")
        {
            isOnLadder = true;
            this.animator.SetBool("onLadder", true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Ladder")
        {
            isOnLadder = false;
            this.animator.SetBool("onLadder", false);
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
