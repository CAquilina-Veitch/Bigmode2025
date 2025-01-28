using System.Diagnostics.CodeAnalysis;
using UnityEngine;

public class ExpandingShot : MonoBehaviour
{
    public Vector2 shootRange;
    private bool WasInShootRange = false;
    public GameObject ExpanderShot;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (shootRange.x < transform.position.x && transform.position.x < shootRange.y)
        {
            if (WasInShootRange == false)
            {
                Expand();
            }
        }
        else
        {
            if (WasInShootRange == true)
            {
                WasInShootRange = false;
            }
        }
    }
    void Expand()
    {
        Instantiate(ExpanderShot, transform.position, Quaternion.identity);
        WasInShootRange = true;
    }
}
