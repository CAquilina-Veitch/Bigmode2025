using Scripts.Enemy.Behaviour;
using UnityEngine;

public class sideShot : BossAttack
{
    public GameObject Bullet;
    private Vector3 sideShoot;
    public int numProjectile;

    void ShootSide(float x, float y, float rotation)
    {
        sideShoot = new Vector3(x, y, 0);
        Instantiate(Bullet, sideShoot, Quaternion.Euler(0, 0, rotation));
        FinishAttack(); // finish instantly to move on to another attack
    }

    public override void Attack()
    {
        for (int i = 0; i < numProjectile; i++)
        {
            float ypos = Random.Range(-3f, 3f);
            int xpos = Random.Range(0, 2);
            xpos = xpos == 0 ? -1 : 1;
            ShootSide(xpos*6, ypos, xpos*-90);
        }
    }
}
