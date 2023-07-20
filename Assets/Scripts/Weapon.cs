using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : DamageDealing
{
    SpriteRenderer spriteRenderer;
    BoxCollider2D boxCollider2D;
    Animator animator;

    void Start() 
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }
    
    protected override void Attack()
    {
        base.Attack();
        animator.SetTrigger("Swing");
    }
    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag == "Enemy")
        {
            Vector3 knock = (other.transform.position - transform.position).normalized * usableSkillInfo.pushForce;
            other.GetComponent<Enemy>().TakeDamage(usableSkillInfo.damage, knock);
        }
    }
}
