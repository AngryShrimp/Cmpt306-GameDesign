/*
 * Keenan Johnstone - 11119412 - kbj182
 * Nov 4th 2016 - CMPT306
 */
using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    public float speed = 10f;
    public int hp = 3;

    //AI Variables
    /*
     * The values chosen allow for easy testing of each state.
     * In a 'balanced' scenario, the aggro range would be much larger
     * With smaller Aggro ranges you can "toy" with the enemy to test the AI
     */
    
    //Should normally be around 10 (roughly 1/3 of the map)
    public float aggroRange = 6f;

    //With this at 5, the enemies sit realitively close to their spawn
    public float spawnTetherRange = 5f;

    //Should normally be around 12-15 (or 1/2 of the map)
    public float threatRange = 8f;
    
    /* If this value is bigger than (or even close to) the aggro size, the enemies can 
     * seemingly turn and fight when the nearest ally is pretty far away, amking them seem
     * Suicidal.
     */
    public float friendlyRange = 4f;
    
    /*  Set this at about 1/3 max HP because if it is too high, the enimies run away to easily
     *  Unless you want 'scardy-cat' enemies
     */
    public int HPThreshold = 1;

    private float currentVelocity;
    private float currentDirection;
    private float playerDist;
    private float spawnDist;
    private float rot = 0f;
    private Transform player;
    private Vector3 spawn;

    private Rigidbody2D rb2d;
    private Animator anim;

    delegate void EnemyDelagate();
    EnemyDelagate AIController;


    public int GetHP()
    {
        return hp;
    }

    /*
     * Damage the enemey ship by a given value
     * 
     * pre: value is of type INT
     * post: None
     * Return: None
     */
    public void takeDamage(int value)
    {
        hp = hp - value;
        if (hp < 0)
            hp = 0;
    }

    void Start()
    {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spawn = gameObject.transform.position;
        AIController = PlayerInThreatZone;

        InvokeRepeating("Tick", 0, 0.1f);

        if (GameObject.FindGameObjectWithTag("Player") == null)
            player = gameObject.transform;
        else
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
            playerDist = Vector3.Distance(gameObject.transform.position, player.transform.position);
        }
        spawnDist = Vector3.Distance(gameObject.transform.position, spawn);

    }

    private void Tick()
    {
        AIController();
    }

    void Update()
    {
        if (GameObject.FindGameObjectWithTag("Player") == null)
            player = gameObject.transform;
        else
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
            playerDist = Vector3.Distance(gameObject.transform.position, player.transform.position);
        }
        spawnDist = Vector3.Distance(gameObject.transform.position, spawn);
        
    }

    void FixedUpdate()
    {
        rb2d.transform.eulerAngles = new Vector3(0, 0, currentDirection);
        if (currentVelocity == 0)
            rb2d.velocity = new Vector2(0f, 0f);
        else
            rb2d.AddForce(gameObject.transform.up * currentVelocity);

        //Weirdly enough the gravity wasn't staying at 0 when the ship was moving causing a weird "lurching"
        //effect that would drag the ship down on occasion. This fixes that. ¯\_(ツ)_/¯
        rb2d.gravityScale = 0f;
        anim.SetFloat("speed", currentVelocity);
    }

    void AIStart()
    {
        //Start of our descision Tree
        AIController = PlayerInThreatZone;
    }

    void PlayerInThreatZone()
    {
        //Yes, check if in Aggro range
        if (playerDist < threatRange)
            AIController = PlayerInAggroRange;

        //No, Check if too Far from spawn
        else
            AIController = FarFromSpawn;
    }

    void PlayerInAggroRange()
    {
        //Yes, Check if Allies are near
        if (playerDist < aggroRange)
            AIController = AlliesClose;

        //No, is HP low?
        else
            AIController = HPLow;
    }

    void FarFromSpawn()
    {
        //Yes, move to Spawn
        if (spawnDist > spawnTetherRange)
            AIController = MoveToSpawn;

        //No? Random Idle
        else
            AIController = RandomIdle;
    }

    void RandomIdle()
    {
        //Move in a random direction
        if (Random.value >= 0.5f)
            AIController = RandomMove;

        //Or Sit still
        else
            AIController = Idle;
    }

    void HPLow()
    {
        //Yes, Run away
        if (hp <= HPThreshold)
            AIController = Retreat;

        //No, Attack
        else
            AIController = Attack;
    }

    void AlliesClose()
    {
        bool allyNear = false;

        GameObject[] enemyList = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject g in enemyList)
        {
            if(g != gameObject)
            {
                float allyDist = Vector3.Distance(g.gameObject.transform.position, gameObject.transform.position);
                if (allyDist < friendlyRange)
                    allyNear = true;
            }
        }
        //Yes, Attack
        if (allyNear)
            AIController = Attack;

        //No, is HP low?
        else
            AIController = HPLow;

    }

    void Attack()
    {
        //CHAAAAAAAAAAAAAAAARGE
        if (player != null)
            rot = Mathf.Atan2((player.transform.position.y - transform.position.y), (player.transform.position.x - transform.position.x)) * Mathf.Rad2Deg - 90;

        currentDirection = rot;
        currentVelocity = speed;
        AIController = AIStart; 
    }

    void Retreat()
    {
        //RUN AWAAAAAAAAAAAAAAAAAAY
        if (player != null)
            rot = Mathf.Atan2((player.transform.position.y - transform.position.y), (player.transform.position.x - transform.position.x)) * Mathf.Rad2Deg + 90;
        currentDirection = rot;
        currentVelocity = speed;
        AIController = AIStart;
    }

    void MoveToSpawn()
    {
        //E.T. Goooooo Hooooooome
        rot = Mathf.Atan2((spawn.y - rb2d.transform.position.y), (spawn.x - transform.position.x)) * Mathf.Rad2Deg - 90;
        currentDirection = rot;
        currentVelocity = speed;
        AIController = AIStart;
    }

    void Idle()
    {
        //Zzzzzz
        currentVelocity = 0;
        AIController = AIStart;
    }

    void RandomMove()
    {
        //Let's go.... THIS WAY
        currentDirection = Random.Range(0,360);
        currentVelocity = speed;
        AIController = AIStart;
    }



}
