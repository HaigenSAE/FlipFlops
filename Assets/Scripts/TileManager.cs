using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public GameObject buttonObj;
    public GameObject blockObj;
    public GameObject[,] tiles;
    public int gridX = 4;
    public int gridY = 4;
    public bool solved = false;
    public bool finished = false;

    private GameManager gm;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    public void GenerateGrid()
    {
        transform.position = new Vector2(-(float)gridX / 2 * 1.5f + 0.75f, -(float)gridY / 2 * 1.5f + 0.75f);
        //generate a grid
        tiles = new GameObject[gridX, gridY];
        for (int x = 0; x < gridX; x++)
        {
            for (int y = 0; y < gridY; y++)
            {
                GameObject go;
                go = Instantiate(buttonObj, new Vector2(transform.position.x + (x * 1.5f), transform.position.y + (y * 1.5f)), transform.rotation);
                //assign all buttons off
                go.GetComponent<ButtonFlip>().x = x;
                go.GetComponent<ButtonFlip>().y = y;
                tiles[x, y] = go;
            }
        }
        //once grid is generated, hit random buttons based on grid size as if the player was hitting them
        //handle this in gamemanager
        gm.RandomlyPushButtons(gridX, gridY, tiles);
    }

    public void DeleteGrid()
    {
        if (tiles != null)
        {
            for (int x = 0; x < gridX; x++)
            {
                for (int y = 0; y < gridY; y++)
                {
                    Destroy(tiles[x, y]);
                }
            }
        }
    }
    
    public void Clicked(GameObject go)
    {
        ButtonFlip goFlip = go.GetComponent<ButtonFlip>();
        bool scanned = false;
        //Flip clicked tile
        goFlip.Flip();
        //FlipFlop adjacent tiles
        if (goFlip.x - 1 >= 0)
            tiles[goFlip.x - 1, goFlip.y].GetComponent<ButtonFlip>().Flip();
        if (goFlip.x + 1 < gridX)
            tiles[goFlip.x + 1, goFlip.y].GetComponent<ButtonFlip>().Flip();
        if (goFlip.y - 1 >= 0)
            tiles[goFlip.x, goFlip.y - 1].GetComponent<ButtonFlip>().Flip();
        if (goFlip.y + 1 < gridY)
            tiles[goFlip.x, goFlip.y + 1].GetComponent<ButtonFlip>().Flip();

        //Check to see if all tiles are on
        for (int x = 0; x < gridX; x++)
        {
            for (int y = 0; y < gridY; y++)
            {
                if (tiles[x,y].GetComponent<ButtonFlip>().on)
                {
                    solved = true;
                }
                else
                {
                    solved = false;
                    scanned = true;
                    break;
                }
            }

            if (scanned)
            {
                break;
            }
            
        }

        if (solved)
        {
            //Winner
            finished = true;
            gm.Winner();
        }
        
    }
}
