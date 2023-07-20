using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    //Movement
    BoxCollider2D boxCollider2D;
    Vector3 moveDelta;
    [SerializeField]
    float movementSpeed;
    Vector3 knockbackVector;
    [SerializeField]
    float knockbackRecoverySpeed;

    //Stats
    [SerializeField]
    protected int health = 10;
    [SerializeField]
    protected int maxHealth = 10;
    [SerializeField]
    protected string deathSound;
    [SerializeField]
    string damageSound;
    
    void Start()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
        knockbackVector = Vector3.zero;
    }
    protected void Move(Vector2 v)
    {
        Move(v.x,v.y);
    }
    protected void Move(float x, float y)
    {
        moveDelta = new Vector3(x,y,0);
        moveDelta += knockbackVector;
        knockbackVector = Vector3.Lerp(knockbackVector,Vector3.zero,knockbackRecoverySpeed);

        transform.Translate(moveDelta*movementSpeed*Time.deltaTime);
    }
    public virtual void TakeDamage(int damage, Vector3 knockback)
    {
        health-=damage;
        if(health<=0)
        {
            Death();
            return;
        }
        SetKnockback(knockback);
        AudioController.instance.PlaySound(damageSound);
    }
    public void Heal(int hp)
    {
        health+=hp;
        if(health>maxHealth)
        {
            health = maxHealth;
        }
    }
    virtual protected void Death()
    {
        Destroy(this.gameObject);
    }
    protected void SetKnockback(Vector3 knockback)
    {
        knockbackVector = knockback;
    }
}
