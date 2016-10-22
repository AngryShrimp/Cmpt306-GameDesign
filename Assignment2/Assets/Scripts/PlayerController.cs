/*
 * Keenan Johnstone - 11119412 - kbj182
 * Oct 21st 2016 - CMPT306
 */
using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float speed;
    public float rotSpeed;
    public float xMin, xMax, yMin, yMax;
    public float nextFire;
    public float fireRate;

    public GameObject bolt;
    public Transform boltSpawn;

    private bool turnRight = true;

    private Rigidbody2D rb2d;
    private Animator anim;
    private AudioSource sound;

    // Use this for initialization
    void Start ()
    {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sound = GetComponent<AudioSource>();
    }

    void Update()
    {
        if(Input.GetButton("Fire") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(bolt, boltSpawn.position, boltSpawn.rotation);
            sound.Play();
        }
        
    }
	
	void FixedUpdate ()
    {
        //Weirdly enough the gravity wasn't staying at 0 when the ship was moving causing a weird "lurching"
        //effect that would drag the ship down on occasion. This fixes that. ¯\_(ツ)_/¯
        rb2d.gravityScale = 0f;

        //Get the vertical and horizontal and send that to the animator
        float horzInput = Input.GetAxis("Horizontal");
        float vertInput = Input.GetAxis("Vertical");

        anim.SetFloat("speed", Mathf.Abs(vertInput));
        anim.SetFloat("rot", Mathf.Abs(horzInput));

        //Addforces based on input (note: horzontal axis is backwards)
        rb2d.AddForce(gameObject.transform.up * speed * vertInput);
        rb2d.MoveRotation(rb2d.rotation + (rotSpeed * horzInput * -1));

        //Clamp the ship to remain in the camera area
        rb2d.position = new Vector3
                            (
                                Mathf.Clamp(rb2d.position.x, xMin, xMax),
                                Mathf.Clamp(rb2d.position.y, yMin, yMax),
                                0.0f
                            );

        //Check if we need to flip the character
        if (horzInput > 0 && !turnRight)
            Flip();
        else if (horzInput < 0 && turnRight)
            Flip();

    }

    void Flip()
    {
        turnRight = !turnRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
