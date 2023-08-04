using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : Character
{
    [SerializeField]
    GameObject baseWeapon;
    [SerializeField]
    GameObject skillHand;
    [SerializeField]
    int currentDamage;
    [SerializeField]
    List<SkillInfo> skillEquipment;
    [SerializeField]
    int skillEquipmentSize;
    Hand hand;
    [SerializeField]
    Slider healthBar;
    void Start() 
    {
        skillEquipment = new List<SkillInfo>();
        hand = GetComponentInChildren<Hand>();
        healthBar.maxValue = maxHealth;
        UpdateSlider();
    }
    void Update()
    {
        if(Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            ChangeDamage(1);
        }
        else if(Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            ChangeDamage(-1);
        }
        hand.MousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if(hand.MousePosition.x - transform.position.x > 0)
        {
            transform.localScale = Vector3.one;
            hand.gameObject.transform.localScale = Vector3.one;
        }
        else if (hand.MousePosition.x - transform.position.x < 0)
        {
            transform.localScale = new Vector3(-1,1,1);
            hand.gameObject.transform.localScale = new Vector3(-1,-1,1);
        }
    }
    void FixedUpdate() 
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        Move(x,y);
    }
    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag == "Item")
        {
            if(skillEquipment.Count < skillEquipmentSize)
            {
                SkillInfo skill = ScriptableObject.CreateInstance<SkillInfo>();
                skill.SetParams(other.GetComponent<Item>().skill);
                AddToEq(skill);
                Destroy(other.gameObject);
            }
        }
    }
    void AddToEq(SkillInfo s)
    {
        if(skillEquipment.Count < skillEquipmentSize)
        {
            skillEquipment.Add(s);
        }
    }
    void RemoveFromEq(SkillInfo s)
    {
        skillEquipment.Remove(s);
        ChangeDamage(0);
    }
    public void RemoveFromEq(int ind=0)
    {
        skillEquipment.RemoveAt(ind);
        ChangeDamage(0);
    }
    void ChangeDamage(int i)
    {
        currentDamage += i;
        CorrectCurrentDamage();
        if(currentDamage > -1)
        {
            baseWeapon.SetActive(false);
            skillHand.SetActive(true);
            skillHand.GetComponent<SkillHand>().ChangeSkill(skillEquipment[currentDamage]);
            UIController.instance.UpdateSkillText(skillEquipment[currentDamage].skillName + " " + skillEquipment[currentDamage].count);
            return;
        }
        baseWeapon.SetActive(true);
        baseWeapon.GetComponent<Weapon>().EnableAttack();
        skillHand.SetActive(false);
        UIController.instance.UpdateSkillText("");
    }
    void CorrectCurrentDamage()
    {
        if(currentDamage >= skillEquipment.Count)
        {
            currentDamage = skillEquipment.Count - 1;
            return;
        }
        if(currentDamage < -1)
        {
            currentDamage = -1;
        }
    }
    public int CurrentDamage
    { 
        get
        {
            return currentDamage;
        }
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
    protected override void Death()
    {
        AudioController.instance.PlaySound(deathSound);
        SceneManager.LoadScene(0);
        AudioController.instance.PlayMenuBackMusic();
    }
}
