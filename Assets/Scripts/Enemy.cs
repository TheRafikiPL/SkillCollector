using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : Character
{
    [SerializeField]
    int damage;
    Vector3 startingPosition;
    [SerializeField]
    float attackCooldown;
    bool isAttackReady = true;
    [SerializeField]
    float chaseRange;
    [SerializeField]
    float triggerRange;
    Transform playerTransform;
    bool chasing;
    bool collidingWithPlayer = false;
    [SerializeField]
    Slider healthBar;

    public override void TakeDamage(int damage, Vector3 knockback)
    {
        base.TakeDamage(damage, knockback);
        UpdateSlider();
    }
    void Start() 
    {
        startingPosition = transform.position;
        playerTransform = GameObject.Find("Player").transform;
        healthBar.maxValue = maxHealth;
        UpdateSlider();
    }
    void FixedUpdate() 
    {
        if(Vector3.Distance(playerTransform.position, startingPosition)< chaseRange)
        {
            chasing = Vector3.Distance(playerTransform.position, startingPosition) < triggerRange;
            if(chasing)
            {
                if(!collidingWithPlayer)
                {
                    Move((playerTransform.position - transform.position).normalized);
                }
            }
            else
            {
                Move(startingPosition - transform.position);
            }
        }
        else
        {
            Move(startingPosition - transform.position);
            chasing = false;
        }
    }
    IEnumerator AttackCountdown()
    {
        yield return new WaitForSeconds(attackCooldown);
        isAttackReady = true;
    }
    void OnCollisionStay2D(Collision2D other) 
    {
        if(other.gameObject.tag == "Player" && isAttackReady)
        {
            collidingWithPlayer = true;
            isAttackReady = false;
            other.gameObject.GetComponent<Player>().TakeDamage(damage, Vector3.zero);
            StartCoroutine(AttackCountdown());
        }    
    }
    private void OnCollisionExit2D(Collision2D other) 
    {
        collidingWithPlayer = false;
    }
    void UpdateSlider()
    {
        healthBar.value = health;
    }
    protected override void Death()
    {
        AudioController.instance.PlaySound(deathSound);
        base.Death();
    }
}
