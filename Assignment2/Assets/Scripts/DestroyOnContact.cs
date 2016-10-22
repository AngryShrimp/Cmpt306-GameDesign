/*
 * Keenan Johnstone - 11119412 - kbj182
 * Oct 21st 2016 - CMPT306
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
        if (col.tag == "Boundary")
            return;
        if(col.tag == "Player")
        {
            Instantiate(playerExplosion, col.transform.position, col.transform.rotation);
            Invoke("pauseGame", 1f);
        }
        gameController.AddScore(scoreValue);
        Instantiate(explosion, transform.position, transform.rotation);
        
        Destroy(col.gameObject);
        Destroy(gameObject);
    }
}
