using System;
using Audio;
using Extensions;
using R3;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using Ext = Extensions.Extensions;

public class HealthScript : MonoBehaviour
{
    public ReactiveProperty<int>Health = new(3);
    public int maxHealth = 3;
    //death
    public float timeToDie = 2f;
    //invuln
    public float invTime = 1f;
    public bool invuln = false;
    public SoundEffectPlayer soundEffectPlayer;

    public Rigidbody2D myRigidBody;

    public void OnEnable()
    {
        Health.Value = maxHealth;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("Coillided with " + collision.tag);
        if (!collision.CompareTag(Ext.GameTag(GameTags.Boss))) return;
        
        if (invuln == false) 
            OnTakeDamage();
    }

    private void OnTakeDamage()
    {
        Health.Value--;
        soundEffectPlayer.PlaySoundEffect(SoundEffectType.GetHit, pitch: 0.65f);
        invuln = true;
        Observable.Timer(TimeSpan.FromSeconds(invTime)).Subscribe(_ => invuln = false).AddTo(this);
        if (Health.Value <= 0)
            OnDead();
            
    }

    private void OnDead()
    {
        Observable.Timer(TimeSpan.FromSeconds(timeToDie)).Subscribe(Dead).AddTo(this);
    }

    private void Dead()
    {
        gameObject.SetActive(false);
    }
}
