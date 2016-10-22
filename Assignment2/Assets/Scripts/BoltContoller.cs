/*
 * Keenan Johnstone - 11119412 - kbj182
 * Oct 21st 2016 - CMPT306
 */
using UnityEngine;
using System.Collections;

public class BoltContoller : MonoBehaviour {

    public float speed;
    private Rigidbody2D rb2d;
    // Use this for initialization
    void Start()
    {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        //rb2d.AddForce(gameObject.transform.up * speed);
        rb2d.velocity = rb2d.transform.up * speed;   
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if(col.gameObject.tag == "Boundary")
            Destroy(gameObject);
    }
}
