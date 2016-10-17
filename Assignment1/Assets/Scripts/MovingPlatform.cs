/*
 * Keenan Johnstone - 11119412 - kbj182
 * Oct 4th 2016 - CMPT306
 */

using UnityEngine;
using System.Collections;

public class MovingPlatform : MonoBehaviour {

    public bool movingUp = true;
    public float vertTravelDist = 10f;
    public float vertSpeed = 0.03f;

    private float initY;

    private Rigidbody2D rb2d;

    // Use this for initialization
    void Start ()
    {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        initY = rb2d.position.y;
    }
	
	// Update is called once per frame
	void Update ()
    {
        //I tried to use a cosine function over time and While I got it to work I found it very diffuclt to
        //Logically fine tune, the new method allows for very easy fine tuning of max height and travel speed
        //rb2d.transform.position = new Vector2(rb2d.position.x, rb2d.position.y + Mathf.Cos(Time.time)/20);

        if (movingUp)
        {
            rb2d.transform.position = new Vector2(rb2d.position.x, rb2d.position.y + vertSpeed);
            if (rb2d.position.y >= initY + vertTravelDist)
                movingUp = false;
        }
        else
        {
            rb2d.transform.position = new Vector2(rb2d.position.x, rb2d.position.y - vertSpeed);
            if (rb2d.position.y <= initY)
                movingUp = true;
        }
            
    }
}
