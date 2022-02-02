using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ButtonFlip : MonoBehaviour
{
    
    public Sprite buttonOn;
    public Sprite buttonOff;
    public bool on = false;

    public int x, y;

    public SpriteRenderer spriteRend;

    public void Start()
    {
        transform.DOScale(1.5f, Random.Range(0.75f, 1.5f));
    }

    public void Flip()
    {
        transform.DOComplete();
        transform.DOPunchScale(new Vector3(0.05f, 0.05f, 0.05f), 0.25f, 10, 1f);
        on = !on;
        spriteRend.sprite = on ? buttonOn : buttonOff;
    }
    
}
