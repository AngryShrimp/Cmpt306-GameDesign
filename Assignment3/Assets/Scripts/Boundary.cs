/*
 * Keenan Johnstone - 11119412 - kbj182
 * Oct 21st 2016 - CMPT306
 */
using UnityEngine;
using System.Collections;

public class Boundary : MonoBehaviour {

	void OnTriggerExit2D(Collider2D col)
    {
        Destroy(col.gameObject);
    }
}
