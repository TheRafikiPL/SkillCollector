using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIController : MonoBehaviour
{
    public static UIController instance;
    Label skillText;
    private void Start() 
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        var root = GetComponent<UIDocument>().rootVisualElement;
        skillText = root.Q<Label>("SkillName");
    }
    public void UpdateSkillText(string skillName)
    {
        skillText.text = skillName;
    }
}
