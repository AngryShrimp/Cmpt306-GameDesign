/*
 * Keenan Johnstone - 11119412 - kbj182
 * Nov 4th 2016 - CMPT306
 */
using UnityEngine;
using System.Collections;

public class BoltContoller : MonoBehaviour {

    public float speed;
    public int damageDealt = 1;
    private Rigidbody2D rb2d;

    public int GetDamageDealt()
    {
        return damageDealt;
    }        

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
