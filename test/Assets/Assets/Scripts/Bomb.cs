using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour {

	 public GameObject boomPrefab;

    void OnCollisionEnter(Collision other)
    {
        if (boomPrefab && other.gameObject.tag == "Player")
        {
            Instantiate(boomPrefab, transform.position, transform.rotation);
        }

        Destroy(this.gameObject,5f);
    }
}
