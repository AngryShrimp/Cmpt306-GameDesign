/*
 * Keenan Johnstone - 11119412 - kbj182
 * Nov 4th 2016 - CMPT306
 */
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class DestroyOnContact : MonoBehaviour
{
    public GameObject explosion;
    public GameObject playerExplosion;
    private GameController gameController;
    public int scoreValue;

    void Start()
    {
        GameObject gameControllerObject = GameObject.Find("GameController");
        if (gameControllerObject != null)
            gameController = gameControllerObject.GetComponent<GameController>();
        Debug.Log(gameController);
        if (gameController == null)
            Debug.Log("Cannot Find GameController");
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        //Ignore leaving the boundary
        if (col.tag == "Boundary")
            return;
        //Kill the player if the enemy collides with it
        if (col.tag == "Player")
        {
            Instantiate(playerExplosion, col.transform.position, col.transform.rotation);
            Instantiate(explosion, transform.position, transform.rotation);
            Destroy(col.gameObject);
            Destroy(gameObject);
        }
        //Decriment HP/die if HP is 0 when shot by weapons fire
        if(col.tag == "WeaponFire")
        {
            BoltContoller bolt = col.gameObject.GetComponent<BoltContoller>();
            Enemy enemyShip = gameObject.GetComponent<Enemy>();
            enemyShip.takeDamage(bolt.GetDamageDealt());
            Destroy(col.gameObject);
            if (enemyShip.GetHP() <= 0)
            {
                gameController.AddScore(scoreValue);
                Instantiate(explosion, transform.position, transform.rotation);
                Destroy(gameObject);
            }
        }
    }
}
