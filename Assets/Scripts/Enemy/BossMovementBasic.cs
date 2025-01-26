using System.Diagnostics.CodeAnalysis;
using UnityEngine;

public class BossMovementBasic : MonoBehaviour
{
    public Vector2 XRange;
    private Vector3 CDirection = Vector3.right;
    public float speed;
    public GameObject Bullet;
    public Vector2 ShootRange;
    private bool WasInShootRange = false;

    void FixedUpdate()
    {
        if (transform.position.x < XRange.x)
        {
            CDirection = Vector3.right;
        }
        else if (transform.position.x > XRange.y)
        {
            CDirection = Vector3.left;
        }
        transform.position += CDirection * speed;

        if (ShootRange.x < transform.position.x && transform.position.x < ShootRange.y)
        {
            if (WasInShootRange == false)
            {
                Shoot();
            }
        }
        else
        {
            if (WasInShootRange == true)
            {
                Shoot();
                WasInShootRange = false;
            }
        }
    }
   
    void Shoot()
    {
        Instantiate(Bullet, transform.position, Quaternion.identity);
        WasInShootRange = true;
    }
}
