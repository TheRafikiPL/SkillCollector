using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    int damage;
    [SerializeField]
    float pushForce;
    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag == "Enemy")
        {
            Vector3 knock = (other.transform.position - transform.position).normalized * pushForce;
            other.GetComponent<Enemy>().TakeDamage(damage, knock);
            Destroy(this.gameObject);
        }
        else if(other.tag == "Wall")
        {
            Destroy(this.gameObject);
        }
    }
    public void SetParameters(int damage, float pushForce)
    {
        this.damage = damage;
        this.pushForce = pushForce;
    }
}

public static class ProjectileExtension
{
    public static Object Instantiate(this Object thisObj, Object original, Vector3 position, Quaternion rotation, int damage, float pushForce, float speed, Vector2 direction)
    {
        GameObject temp = Object.Instantiate(original, position, rotation) as GameObject;
        temp.GetComponent<Projectile>().SetParameters(damage, pushForce);
        temp.GetComponent<Rigidbody2D>().velocity = direction * speed;
        return temp;
    }
}
