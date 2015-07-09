using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class JumpmanControls : MonoBehaviour {
    Rigidbody2D rb2d;
    [SerializeField]private Animator animator = null;
    [SerializeField]private GameObject spriteContainer = null;
    [SerializeField]private Transform groundCheck = null;
    
    private Vector2 direction = Vector2.zero;
    private float walkForce = 8f;
    private float maxWalkSpeed = 3f;
    private float jumpForce = 50f;
    private bool isOnGround = false;

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
    }

    void Update()
    {
        ProcessInput();
        CheckGround();
        if (isOnGround == true)
        {
            this.animator.SetBool("onGround", true);
        }
    }

    // Processes the different types of inputs available
    private void ProcessInput()
    {
        ProcessWalking();
        ProcessJump();
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
        if (Input.GetKey(KeyCode.Space) && isOnGround == true)
        {
            this.direction.y = 1;
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
}
