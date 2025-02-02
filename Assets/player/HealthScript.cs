using Audio;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class HealthScript : MonoBehaviour
{
    public float Health = 3f;
    //death
    public float timeToDie = 2f;
    public float deathTimer;
    public bool dead = false;
    //invuln
    public float invTime = 1f;
    public float invTimer;
    public bool invuln = false;
    public SoundEffectPlayer soundEffectPlayer;

    public Rigidbody2D myRigidBody;

    public void OnEnable()
    {
        Health = 3f;
        deathTimer = 0f;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {      
        if (invuln == false)
        {
            Health += -1;
            soundEffectPlayer.PlaySoundEffect(SoundEffectType.GetHit, pitch: 0.65f);
        }

        invuln = true;

        if (Health <= 0)
        {
            dead = true;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("collision");

        if (invuln == false)
        {
            Health += -1;
            soundEffectPlayer.PlaySoundEffect(SoundEffectType.GetHit, pitch: 0.65f);
        }

        invuln = true;

        if (Health <= 0)
        {
            dead = true;
        }
    }
    private void Update()
    {
        if (dead == true)
        {
            deathTimer += Time.deltaTime;

            if (deathTimer >= timeToDie)
            {
                gameObject.SetActive(false);
            }
        }
        if (invuln == true)
        {
            invTimer += Time.deltaTime;

            if (invTimer >= invTime)
            {
                invuln = false;
                invTimer = 0f;
            }
        }
    }
}
