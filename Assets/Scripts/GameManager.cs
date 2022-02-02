using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Object References")]
    public GameObject canvas;
    public GameObject confettiWindow;
    public GameObject startButton;
    public GameObject finishedWindow;
    public GameObject title;
    public GameObject howToPlayWindow;
    public List<GameObject> gridButtons;
    public TileManager tileManager;
    
    [Header("Gameplay")]
    public Text moveTextObj;
    public Text timerTextObj;
    public float timer;
    public int moves;
    public bool playing = false;
    private bool gridSelected;
    
    [Header("Audio")]
    public AudioSource sfxAudio;
    public AudioSource musicAudio;
    public Sprite sfxAudioOnImage;
    public Sprite sfxAudioOffImage;
    public Sprite sfxMusicOnImage;
    public Sprite sfxMusicOffImage;

    // Start is called before the first frame update
    void Start()
    {
        tileManager = GameObject.FindGameObjectWithTag("TileManager").GetComponent<TileManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playing)
        {
            timer += Time.deltaTime;
            int minutes = Mathf.FloorToInt(timer / 60f);
            int seconds = Mathf.FloorToInt(timer - minutes * 60f);
            string timeString = string.Format("{0:0}:{1:00}", minutes, seconds);

            timerTextObj.text = timeString;
        }
    }

    public void BeginGame()
    {
        if (gridSelected)
        {
            playing = true;
            startButton.SetActive(false);
            Destroy(title);
            foreach (GameObject gridButton in gridButtons)
            {
                gridButton.SetActive(false);
            }

            foreach (GameObject button in GameObject.FindGameObjectsWithTag("DifficultyButtons"))
            {
                button.SetActive(false);
            }
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void RandomlyPushButtons(int gridX, int gridY, GameObject[,] tiles)
    {
        //3x3 grid will randomly push 3 times, 5x5 will push 5, 7x7 randoms 7 times. 
        for (int i = 0; i < gridX; i++)
        {
            int randX = Random.Range(0, gridX);
            int randY = Random.Range(0, gridY);
            //need to treat random buttons as if player clicked them
            tileManager.Clicked(tiles[randX,randY]);
        }
        
    }

    public void UpdateMoves()
    {
        moves++;
        moveTextObj.text = moves.ToString();
        sfxAudio.PlayOneShot(sfxAudio.clip);
    }

    public void ToggleSFXAudio(bool isMusic)
    {
        if (isMusic)
        {
            musicAudio.volume = 0.33f - musicAudio.volume;
            if (musicAudio.volume == 0.0f)
                musicAudio.transform.GetComponent<Image>().sprite = sfxMusicOffImage;
            else
                musicAudio.transform.GetComponent<Image>().sprite = sfxMusicOnImage;
        }
        else
        {
            sfxAudio.volume = 0.33f - sfxAudio.volume;
            if (sfxAudio.volume == 0.0f)
                sfxAudio.transform.GetComponent<Image>().sprite = sfxAudioOffImage;
            else
                sfxAudio.transform.GetComponent<Image>().sprite = sfxAudioOnImage;
        }
    }

    public void Winner()
    {
        Instantiate(confettiWindow, canvas.transform);
        playing = false;
        Instantiate(finishedWindow, canvas.transform);
    }

    public void GridSelection(int selection)
    {
        tileManager.DeleteGrid();
        switch (selection)
        {
            case 0:
                tileManager.gridX = 3;
                tileManager.gridY = 3;
                break;
            case 1:
                tileManager.gridX = 5;
                tileManager.gridY = 5;
                break;
            case 2:
                tileManager.gridX = 7;
                tileManager.gridY = 7;
                break;
        }
        tileManager.GenerateGrid();
        gridSelected = true;
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }

    public void ShowHowToPlay()
    {
        howToPlayWindow.SetActive(true);
    }

    public void HideHowToPlay()
    {
        howToPlayWindow.SetActive(false);
    }
    
}
