using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillHand : DamageDealing
{
    public void ChangeSkill(SkillInfo s)
    {
        usableSkillInfo = s;
        EnableAttack();
    }
    protected override void Attack()
    {
        if(usableSkillInfo.count>1)
        {
            base.Attack();
            usableSkillInfo.count--;
            UIController.instance.UpdateSkillText(usableSkillInfo.skillName + " " + usableSkillInfo.count);
            CreateProjectile();
            return;
        }
        base.Attack();
        CreateProjectile();
        Player temp = GetComponentInParent<Player>();
        temp.RemoveFromEq(temp.CurrentDamage);
    }
    void CreateProjectile()
    {
        Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (Vector2)((worldMousePos - transform.position));
        direction.Normalize();
        gameObject.Instantiate(usableSkillInfo.projectilePrefab, transform.position, Quaternion.identity, usableSkillInfo.damage, 
        usableSkillInfo.pushForce, usableSkillInfo.projectileSpeed, direction);
    }
}
