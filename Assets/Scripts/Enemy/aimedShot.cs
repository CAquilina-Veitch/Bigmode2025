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
    public int numberOfShots;
    private int numberOfShotsLeft;
    void AimedShot()
    {
        GameObject firedShot = Instantiate(Bullet, bossTransform.position, Quaternion.identity);
        Vector3 direction = playerTransform.position - firedShot.transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        firedShot.transform.rotation = Quaternion.Euler(0, 0, angle + 90);
        WasInShootRange = true;
        
        numberOfShotsLeft--;
        if (numberOfShotsLeft <= 0) 
            FinishAttack();
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
                AimedShot();
        }
        else if (WasInShootRange == true)
        {
            AimedShot();
            WasInShootRange = false;
        }
    }

    public override void Attack()
    {
        numberOfShotsLeft = numberOfShots;
        Observable.IntervalFrame(1).Subscribe(BossMovementLoop).AddTo(isAttackingDisposable);
    }
}

