using System;
using System.Collections;
using System.Xml.Serialization;
using Extensions;
using R3;
using UnityEngine;

// Script for spawning and movement of the fan of 8 bullets in 2nd half of expanding shot

public class ExpandBullet : MonoBehaviour
{
    public GameObject Bullets;
    public float bulletSpeed;
    public float breakHeight = -2f;

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
            SpreadShot();
            Destroy(gameObject);
        }
    }

    void SpreadShot()
    {
        Instantiate(Bullets, transform.position, Quaternion.Euler(0, 0, 0));
        Instantiate(Bullets, transform.position, Quaternion.Euler(0, 0, 45));
        Instantiate(Bullets, transform.position, Quaternion.Euler(0, 0, 90));
        Instantiate(Bullets, transform.position, Quaternion.Euler(0, 0, 135));
        Instantiate(Bullets, transform.position, Quaternion.Euler(0, 0, 180));
        Instantiate(Bullets, transform.position, Quaternion.Euler(0, 0, 225));
        Instantiate(Bullets, transform.position, Quaternion.Euler(0, 0, 270));
        Instantiate(Bullets, transform.position, Quaternion.Euler(0, 0, 315));
    }

}
