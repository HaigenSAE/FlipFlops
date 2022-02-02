using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ScrollingImage : MonoBehaviour
{
   
    public Vector3[] wayPoints;

    private Tween activeTween;

    void Start()
    {
        activeTween = transform.DOPath(wayPoints, 120.0f, PathType.CatmullRom, PathMode.Ignore);
    }

    void Update()
    {
        if (activeTween.IsComplete())
        {
            activeTween = transform.DOPath(wayPoints, 120.0f, PathType.CatmullRom, PathMode.Ignore);
        }
        
    }
    
}
