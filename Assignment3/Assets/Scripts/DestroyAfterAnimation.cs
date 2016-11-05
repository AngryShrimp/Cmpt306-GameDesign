/*
 * Keenan Johnstone - 11119412 - kbj182
 * Oct 21st 2016 - CMPT306
 */
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class DestroyAfterAnimation : MonoBehaviour {

    public float delay = 0f;
    // Use this for initialization
    void Start ()
    {
        Destroy(gameObject, this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length + delay);
        if(gameObject.tag == "Player")
            StartCoroutine(ExecuteAfterTime(1.3f));
    }
    IEnumerator ExecuteAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
