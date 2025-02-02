using Scripts.Enemy.Behaviour;
using R3;
using Extensions;
using UnityEngine;

public class JumpAttack : BossAttack
{
    public float jumpNumber = 1f;
    public float jumpSpeed = 10f;
    public Vector2 moveRange;
    public float jumpStartTimer = 0f;
    [SerializeField] float acceleration;
    public Animator animator;
    private void Start()
    {
        acceleration = 0f;    
        animator = bossTransform.GetComponent<Animator>();
    }
    void BossMovementLoop()
    {
        Debug.Log("jump thing happen");

        if (jumpNumber != 0f)
        {
            animator.SetBool("jumping", true);

            if (acceleration >= 0f && jumpStartTimer >= 1f)
            {
                acceleration += 1.5f * Time.deltaTime;
              
            }

            if (acceleration == 0f)
            {
                jumpStartTimer += 3f * Time.deltaTime;
            }
          
        }


        if (jumpNumber == 0f)
        {
            animator.SetBool("jumping", false);
            bossTransform.rotation = Quaternion.identity;
            
        }
        //jump1
        if (jumpNumber == 1f)
        {bossTransform.position += (Vector3.up + Vector3.left) * jumpSpeed * acceleration * Time.deltaTime;}
        if (jumpNumber == 1f && bossTransform.position.x <= -moveRange.x)
        {
            bossTransform.rotation = Quaternion.Euler(bossTransform.rotation.eulerAngles + (new Vector3(0, 0, -90)));
            jumpNumber = 2;
            jumpStartTimer = 0f;
            acceleration = 0f;       
        }
        //2
        if (jumpNumber == 2f)
        { bossTransform.position += (Vector3.up + Vector3.right) * jumpSpeed * acceleration * Time.deltaTime; }
        if (jumpNumber == 2f && bossTransform.position.y >= moveRange.y)
        {
            bossTransform.rotation = Quaternion.Euler(bossTransform.rotation.eulerAngles + (new Vector3(0, 0, -90)));
            jumpNumber = 3;
            jumpStartTimer = 0f;
            acceleration = 0f;
        }
        //3
        if (jumpNumber == 3f)
        { bossTransform.position += (Vector3.down + Vector3.right) * jumpSpeed * acceleration * Time.deltaTime; }
        if (jumpNumber == 3f && bossTransform.position.x >= moveRange.x)
        {
            bossTransform.rotation = Quaternion.Euler(bossTransform.rotation.eulerAngles + (new Vector3(0, 0, -90)));
            jumpNumber = 4;
            jumpStartTimer = 0f;
            acceleration = 0f;
        }
        //4
        if (jumpNumber == 4f)
        { bossTransform.position += (Vector3.down + Vector3.left) * jumpSpeed * acceleration * Time.deltaTime; }
        if (jumpNumber == 4f && bossTransform.position.y <= -moveRange.y)
        {
            bossTransform.rotation = Quaternion.Euler(bossTransform.rotation.eulerAngles + (new Vector3(0, 0, -90)));
            jumpNumber = 0;
            jumpStartTimer = 0f;
            acceleration = 0f;
        }      

        if (jumpNumber == 0)
            FinishAttack();
    }
    public override void Attack()
    {
        bossTransform.rotation = Quaternion.identity;
        jumpNumber = 1;
        Observable.IntervalFrame(1).Subscribe(BossMovementLoop).AddTo(isAttackingDisposable);
    }
}
