/*
 * Keenan Johnstone - 11119412 - kbj182
 * Oct 4th 2016 - CMPT306
 */

using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    [HideInInspector] public bool jump = false;
    public float maxVelocity = 7f;
    public float velocity = 50f;
    public float jumpStrength = 1200f;
    public float fallZone = -10f;
    
    public Transform groundCheck;
    public LayerMask whatIsGround;
    public float groundRadius = 0.2f;
    private bool grounded = false;
    private bool facingRight = true;

    private Rigidbody2D rb2d;
    private Animator anim;

	// Use this for initialization
	void Start () {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (grounded && Input.GetButtonDown("Jump"))
        {
            anim.SetBool("ground", false);
            rb2d.AddForce(new Vector2(0, jumpStrength));
        }

        if (rb2d.transform.position.y < fallZone)
        {
            rb2d.transform.position = new Vector2(-17.62f, -3.63f);
            rb2d.velocity = new Vector2(0f, 0f);
            if (!facingRight)
                Flip();
        }
    }

    void FixedUpdate()
    {
        //Check if we are on the ground, true if we are
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
        anim.SetBool("ground", grounded);
        anim.SetFloat("vertVelocity", rb2d.velocity.y);

        //Get the horzontal input (left and right arrow keys) and send that to the animator
        float horzInput = Input.GetAxis("Horizontal");

        //speed must be Absolute, else we have issues decting movement
        //as the way I set up the animator controller it looks for speed (directionless) not velocity
        //but I still want velcoty for movement and such
        anim.SetFloat("speed", Mathf.Abs(horzInput));

        //Check max positive velocity
        if (rb2d.velocity.x > maxVelocity)
        {
            rb2d.velocity = new Vector2(maxVelocity, rb2d.velocity.y);
        }
        //Check Max negative velocity
        if (rb2d.velocity.x < -maxVelocity)
        {
            rb2d.velocity = new Vector2(-maxVelocity, rb2d.velocity.y);
        }
        //Add the force to the player
        rb2d.AddForce(Vector2.right * velocity * horzInput);

        //Check if we need to flip the character
        if (horzInput > 0 && !facingRight)
            Flip();
        else if (horzInput < 0 && facingRight)
            Flip();

    }

    //When called flips the direction the player is facing
    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
