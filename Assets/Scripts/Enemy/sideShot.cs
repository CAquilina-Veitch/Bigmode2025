using Scripts.Enemy.Behaviour;
using UnityEngine;

public class sideShot : BossAttack
{
    public Vector2 moveRange;
    public float moveSpeed;
    public Vector2 ShootRange;
    public GameObject Bullet;
    private Vector3 sideShoot;

    void ShootSide(float x, float y, float rotation)
    {
        sideShoot = new Vector3(x, y, 0);
        Instantiate(Bullet, sideShoot, Quaternion.Euler(0, 0, rotation));
        FinishAttack(); // finish instantly to move on to another attack
    }

    public override void Attack()
    {
        ShootSide(-9, -2, 90);
    }
}
