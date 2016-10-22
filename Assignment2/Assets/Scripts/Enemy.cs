/*
 * Keenan Johnstone - 11119412 - kbj182
 * Oct 21st 2016 - CMPT306
 */
using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    public float speed = 10f;
    private Transform player;

    private float rot = 0f;
    private Rigidbody2D rb2d;

    void Start()
    {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        if (GameObject.FindGameObjectWithTag("Player") == null)
            player = gameObject.transform;
        else
            player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void FixedUpdate()
    {
        //There was a bug if the player was destroyed the enemies would crash the game.
        if (player != null)
            rot = Mathf.Atan2((player.transform.position.y - transform.position.y), (player.transform.position.x - transform.position.x)) * Mathf.Rad2Deg - 90;

        transform.eulerAngles = new Vector3(0, 0, rot);

        //Weirdly enough the gravity wasn't staying at 0 when the ship was moving causing a weird "lurching"
        //effect that would drag the ship down on occasion. This fixes that. ¯\_(ツ)_/¯
        rb2d.gravityScale = 0f;

        rb2d.AddForce(gameObject.transform.up * speed);
    }
}
