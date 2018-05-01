using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public GameObject boomPrefab;

    void OnCollisionEnter(Collision other)
    {
        if (boomPrefab)
        {
            Instantiate(boomPrefab, transform.position, transform.rotation);
        }

        Destroy(this.gameObject);
    }
}
