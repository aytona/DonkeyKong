using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class JumpmanControls : MonoBehaviour {
    Rigidbody2D rb2d;
    [SerializeField]private Animator animator = null;
    [SerializeField]private GameObject spriteContainer = null;
    
    private Vector2 direction = Vector2.zero;
    private float walkForce = 10f;
    private float maxWalkSpeed = 4f;

    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Walk(this.direction);
    }

    void Update()
    {
        ProcessInput();
    }

    private void ProcessInput()
    {
        ProcessWalking();
    }

    // Changes the x vector depending on the keys pressed
    private void ProcessWalking()
    {
        this.direction = Vector2.zero;
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            this.direction += new Vector2(-1, 0);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            this.direction += new Vector2(1, 0);
        }
    }

    // Orients the character to the proper direction they are walking towards to
    private void OrientCharacter(Vector2 direction)
    {
        Vector3 spriteScale = this.spriteContainer.transform.localScale;
        // Movement direction towards the right
        if (direction.x > 0)
        {
            spriteScale.x = 3;
        }
        // Movement direction towards the left
        else if (direction.x < 0)
        {
            spriteScale.x = -3;
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
        this.animator.SetTrigger("Walking");
    }
}
