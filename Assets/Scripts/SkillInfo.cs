using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Skill", menuName = "ScriptableObjects/Skills", order = 1)]
public class SkillInfo : ScriptableObject
{
    [SerializeField]
    public string skillName;
    [SerializeField]
    public int damage;
    [SerializeField]
    public float pushForce;
    [SerializeField]
    public float cooldown;
    [SerializeField]
    public int count;
    [SerializeField]
    public float projectileSpeed;
    [SerializeField]
    public GameObject projectilePrefab;
    [SerializeField]
    public bool isReady = true;

    public void SetParams(SkillInfo s)
    {
        skillName = s.skillName;
        damage = s.damage;
        pushForce = s.pushForce;
        cooldown = s.cooldown;
        count = s.count;
        projectileSpeed = s.projectileSpeed;
        projectilePrefab = s.projectilePrefab;
    }
}
