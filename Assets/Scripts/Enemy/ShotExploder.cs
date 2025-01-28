using System;
using System.Collections;
using Extensions;
using R3;
using UnityEngine;

public class ShotExploder : MonoBehaviour
{
    public float bulletSpeed;
    public float breakHeight = -1f;
    public GameObject exploder;

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position += transform.up * -1 * bulletSpeed;
        BulletGone();
    }

    void BulletGone()
    {
        if (transform.position.y < breakHeight)
        {
            explosion();
            Destroy(gameObject);
        }
    }

    void explosion()
    {
        Instantiate(exploder, transform.position, Quaternion.identity);
    }


}
