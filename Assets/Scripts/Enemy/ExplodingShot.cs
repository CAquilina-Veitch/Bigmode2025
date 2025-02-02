using System.Diagnostics.CodeAnalysis;
using Extensions;
using R3;
using Scripts.Enemy.Behaviour;
using UnityEngine;

// Script for exploding shot

public class ExplodingShot : BossAttack
{
    private Vector3 CDirection = Vector3.right;
    public Vector2 moveRange;
    public float moveSpeed;
    public Vector2 shootRange;
    private bool WasInShootRange = true;
    public GameObject ExplodeShot;

    void BossMovementLoop()
    {
        if (bossTransform.position.x < moveRange.x)
            CDirection = Vector3.right;
        
        else if (bossTransform.position.x > moveRange.y) 
            CDirection = Vector3.left;
        
        bossTransform.position += CDirection * moveSpeed * Time.deltaTime;

        if (shootRange.x < bossTransform.position.x && bossTransform.position.x < shootRange.y)
        {
            if (WasInShootRange == false) 
                Explode();
        }
        else if (WasInShootRange == true)
        { 
            Explode(); 
            WasInShootRange = false;
        }
    }
    void Explode()
    {
        Instantiate(ExplodeShot, bossTransform.position, Quaternion.identity);
        WasInShootRange = true;
        FinishAttack();
    }

    public override void Attack()
    {
        Observable.IntervalFrame(1).Subscribe(BossMovementLoop).AddTo(isAttackingDisposable);
    }
}
