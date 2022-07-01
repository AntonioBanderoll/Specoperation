using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BodyPart
{
    StomachHit, HeadHitZomb
}


public class PartHit : MonoBehaviour
{
    [SerializeField] private BodyPart part;
    [SerializeField] private int damage=10;


    private void OnMouseDown()
    {
        Putler.Instance.Hit(part, damage);
    }
    
}
