using System.Diagnostics.CodeAnalysis;
using DG.Tweening;
using UnityEngine;

public class BossMovementBasic : MonoBehaviour
{
    private Vector3 CDirection = Vector3.right;
    public Vector2 moveRange;
    public float moveSpeed;
    public Vector2 ShootRange;
    private bool WasInShootRange = true;
    public GameObject Bullet;
    private Vector3 sideShoot;
    public GameObject Player;

    void FixedUpdate()
    {
        if (transform.position.x < moveRange.x)
        {
            CDirection = Vector3.right;
        }
        else if (transform.position.x > moveRange.y)
        {
            CDirection = Vector3.left;
        }
        transform.position += CDirection * moveSpeed;

        if (ShootRange.x < transform.position.x && transform.position.x < ShootRange.y)
        {
            if (WasInShootRange == false)
            {
                //Shoot();
                //ShootSide(9,-2, 270);
               // ShootSide(-9,-2, 90);
                AimedShot();
            }
        }
        else
        {
            if (WasInShootRange == true)
            {
                //Shoot();
                //ShootSide(9, -2, 270);
                //ShootSide(-9, -2, 90);
                AimedShot();
                WasInShootRange = false;
            }
        }
    }
   
    void Shoot()
    {
        Instantiate(Bullet, transform.position, Quaternion.identity);
        WasInShootRange = true;
    }
    
    void ShootSide(float x, float y, float rotation)
    {
        sideShoot = new Vector3 (x, y, 0);
        Instantiate(Bullet, sideShoot, Quaternion.Euler(0,0,rotation));
        WasInShootRange = true;
    }
    
    void AimedShot()
    {
        GameObject firedShot = Instantiate(Bullet, transform.position, Quaternion.identity);
        Vector3 direction = Player.transform.position - firedShot.transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        firedShot.transform.rotation = Quaternion.Euler(0, 0, angle+90);
        WasInShootRange = true;
    }
}
