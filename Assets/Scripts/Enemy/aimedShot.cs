using System.Diagnostics.CodeAnalysis;
using DG.Tweening;
using Extensions;
using R3;
using Scripts.Enemy.Behaviour;
using UnityEngine;
using UnityEngine.TestTools;

public class aimedShot : BossAttack
{
    private Vector3 CDirection = Vector3.right;
    public Vector2 moveRange;
    public float moveSpeed;
    public Vector2 ShootRange;
    private bool WasInShootRange = true;
    public GameObject Bullet;
    public GameObject Player;
    void AimedShot()
    {
        GameObject firedShot = Instantiate(Bullet, transform.position, Quaternion.identity);
        Vector3 direction = Player.transform.position - firedShot.transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        firedShot.transform.rotation = Quaternion.Euler(0, 0, angle + 90);
        WasInShootRange = true;
    }
    void BossMovementLoop()
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
                AimedShot();
            }
        }
        else
        {
            if (WasInShootRange == true)
            {
                AimedShot();
                WasInShootRange = false;
            }
        }
    }

    public override void Attack()
    {
        Observable.IntervalFrame(1).Subscribe(BossMovementLoop);
    }
}

