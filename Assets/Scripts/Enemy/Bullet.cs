using System;
using System.Collections;
using Extensions;
using R3;
using UnityEngine;

// Script for the movement+removal of basic bullets

public class Bullet : MonoBehaviour
{
    public float bulletSpeed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(BulletGone());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position += transform.up * -1 * bulletSpeed;
    }

    IEnumerator BulletGone()
    {
        yield return new WaitForSeconds(4);
        Destroy(gameObject);    
    }
}
