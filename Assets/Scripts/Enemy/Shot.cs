using System;
using Extensions;
using R3;
using Scripts.Enemy.Behaviour;
using UnityEngine;

public class Shot : BossAttack
{
    private Vector3 CDirection = Vector3.right;
    public Vector2 moveRange;
    public float moveSpeed;
    public Vector2 ShootRange;
    private bool WasInShootRange = true;
    public GameObject Bullet;
    void Shoot()
    {
        Instantiate(Bullet, transform.position, Quaternion.identity);
        WasInShootRange = true;
    }
    void BossMovementLoop()
    {
        if (bossTransform.position.x < moveRange.x)
            CDirection = Vector3.right;
        
        else if (bossTransform.position.x > moveRange.y) 
            CDirection = Vector3.left;
        
        bossTransform.position += CDirection * moveSpeed;

        if (ShootRange.x < bossTransform.position.x && bossTransform.position.x < ShootRange.y)
        {
            if (WasInShootRange == false) 
                Shoot();
        }
        else if (WasInShootRange == true)
        { 
            Shoot(); 
            WasInShootRange = false;
        }
    }

    public override void Attack()
    {
        Observable.IntervalFrame(1).Subscribe(BossMovementLoop).AddTo(isAttackingDisposable);
        Observable.Timer(TimeSpan.FromSeconds(3)).Subscribe(FinishAttack).AddTo(isAttackingDisposable);
    }
}
