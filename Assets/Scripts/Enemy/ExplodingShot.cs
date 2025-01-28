using System.Diagnostics.CodeAnalysis;
using UnityEngine;

// Script for exploding shot

public class ExplodingShot : MonoBehaviour
{
    public Vector2 shootRange;
    private bool WasInShootRange = true;
    public GameObject ExplodeShot;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (shootRange.x < transform.position.x && transform.position.x < shootRange.y)
        {
            if (WasInShootRange == false)
            {
                Explode();
            }
        }
        else
        {
            if (WasInShootRange == true)
            {
                Explode();
                WasInShootRange = false;
            }
        }
    }
    void Explode()
    {
        Instantiate(ExplodeShot, transform.position, Quaternion.identity);
        WasInShootRange = true;
    }
}
