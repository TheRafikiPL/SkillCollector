using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public Vector2 MousePosition { get; set; }
    void Update() 
    {
        transform.right = (MousePosition - (Vector2)transform.position).normalized;
    }
}
