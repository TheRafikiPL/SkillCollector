using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : Character
{
    [SerializeField]
    Slider healthBar;

    void Start() 
    {
        healthBar.maxValue = maxHealth;
        UpdateSlider();
    }
    void UpdateSlider()
    {
        healthBar.value = health;
    }
    public override void TakeDamage(int damage, Vector3 knockback)
    {
        base.TakeDamage(damage, knockback);
        UpdateSlider();
    }
}
