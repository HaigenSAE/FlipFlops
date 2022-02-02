using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerController : MonoBehaviour
{

    public TileManager tileManager;
    public GameManager gameManager;

    public AudioClip tileClick;

    // Start is called before the first frame update
    void Start()
    {
        tileManager = GameObject.FindGameObjectWithTag("TileManager").GetComponent<TileManager>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit;
        //clicky click
        if (Input.GetMouseButtonDown(0))
        {
            hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            CheckHit(hit);
        }
        else if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position), Vector2.zero);
            CheckHit(hit);
        }
    }

    void CheckHit(RaycastHit2D incHit)
    {
        if (incHit.collider != null)
        {
            if(incHit.transform.CompareTag("Tile"))
            {
                if (gameManager.playing)
                {
                    tileManager.Clicked(incHit.transform.gameObject);
                    gameManager.UpdateMoves();
                }
            }
        }
    }
}
