using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class JumpmanControls : MonoBehaviour {

    // WHY CHANGE RIGIDBODY
    new Rigidbody2D rb2d;

    private Animator animator = null;
    private GameObject spriteContainer = null;
    private Transform groundCheck = null;

    // Animation states
    private bool hasHammer = false;
    private bool onLadder = false;
    private bool jump = false;
    private bool death = false;

    // Movement variables
    private Vector2 direction = Vector2.zero;
    private float walkForce = 10f;
    private float maxWalkSpeed = 4f; 
    private float jumpForce = 1f;

    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        OrientCharacter(this.direction);
        Walk(this.direction);
        Jump(this.direction);
    }

    private void OrientCharacter(Vector2 direction)
    {
        Vector3 spriteScale = this.spriteContainer.transform.localScale;
        if (direction.x > 0)
        {
            spriteScale.x = -3;
        }
        else if (direction.x < 0)
        {
            spriteScale.x = 3;
        }
        this.spriteContainer.transform.localScale = spriteScale;
    }

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
        // TODO: Animator settings
        // this.animator.settrigger("walking");
    }

    private void Jump(Vector2 direction)
    {
        
    }
}
