using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Tweener : MonoBehaviour
{
    public enum effects
    {
        fadeIn,
        fadeOut,
        scaleIn,
        scaleOut,
        fall,
        fallBounce
    }

    [Header("Target Effects")]
    public effects[] beginEffects;
    public effects[] endEffects;
    public bool hovering = false;
    private bool hover;

    [Header("Scale In/Out Targets")]
    public float targetInScale;
    public float targetInDuration = 1.0f;
    public float targetOutScale;
    public float targetOutDuration = 1.0f;

    [Header("Fall Down Variables")] 
    public Vector2 startPos;
    public float minFallDuration = 1.0f;
    public Vector3[] bouncePath;

    [Header("Shake Effect Variables")]
    public float shakeScale = 0.5f;
    public float shakeDuration = 0.25f;
    public int shakeVibrato = 10;
    public float shakeElasticity = 1f;

    public float delayModifier = 0.25f;
    private float randomDelay = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        BeginFX();
    }

    public void BeginFX()
    {
        foreach (effects fx in beginEffects)
        {
            randomDelay = Random.Range(minFallDuration, minFallDuration + delayModifier);
            switch (fx)
            {
                case effects.fadeIn:
                    if (GetComponent<Image>() != null)
                    {
                        GetComponent<Image>().color = new Color(GetComponent<Image>().color.r,
                            GetComponent<Image>().color.b, GetComponent<Image>().color.g, 0.0f);
                        GetComponent<Image>().DOFade(1.0f, 1.0f);
                    }
                    break;
                case effects.scaleIn:
                    transform.localScale = new Vector3(0.0f, 0.0f, 0.0f);
                    randomDelay = Random.Range(targetInDuration, targetInDuration + delayModifier);
                    transform.DOScale(targetInScale, randomDelay);
                    break;
                case effects.fall:
                    Vector2 curPos = transform.localPosition;
                    transform.position = startPos;
                    transform.DOLocalMove(curPos, randomDelay);
                    break;
                case effects.fallBounce:
                    transform.localPosition = bouncePath[0];
                    transform.DOLocalPath(bouncePath, randomDelay, PathType.Linear, PathMode.Ignore);
                    break;
            }
        }

        hover = hovering;
    }

    void Update()
    {
        if (hover)
        {
            if (transform.localPosition.y <= bouncePath[bouncePath.Length - 1].y + 0.1f)
            {
                transform.DOLocalMoveY(bouncePath[bouncePath.Length - 1].y + 15.0f, 1.0f);
            }
            else if (transform.localPosition.y >= bouncePath[bouncePath.Length - 1].y + 14.0f)
            {
                transform.DOLocalMoveY(bouncePath[bouncePath.Length - 1].y, 1.0f);
            }

        }
    }

    public void Shake()
    {
        transform.DOPunchScale(new Vector3(shakeScale, shakeScale, shakeScale), shakeDuration, shakeVibrato, shakeElasticity);
    }

    public void EndFX()
    {
        foreach (effects fx in endEffects)
        {
            switch (fx)
            {
                case effects.fadeOut:
                    if(GetComponent<Image>() != null)
                        GetComponent<Image>().DOFade(0.0f, 1.0f);
                    break;
                case effects.scaleOut:
                    transform.DOScale(targetOutScale, targetOutDuration);
                    break;
            } 
        }
    }
}
