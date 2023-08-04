using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealing : MonoBehaviour
{
    [SerializeField]
    protected SkillInfo usableSkillInfo;
    public void EnableAttack()
    {
        usableSkillInfo.isReady = true;
    }
    void Update() 
    {
        if(Input.GetMouseButtonDown(0))
        {
            if(usableSkillInfo.isReady)
            {
                Attack();
            }
        }
    }
    protected virtual IEnumerator AttackCooldown(float cooldown)
    {
        yield return new WaitForSeconds(cooldown);
        usableSkillInfo.isReady = true;
    }

    protected virtual void Attack()
    {
        usableSkillInfo.isReady = false;
        StartCoroutine(AttackCooldown(usableSkillInfo.cooldown));
        AudioController.instance.PlaySound(usableSkillInfo.effectSound);
    }
}
