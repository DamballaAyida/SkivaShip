using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    // Start is called before the first frame update
    public RawImage background;
    public RawImage clouds;
    public enum GameState { Idle, Playing,Ended,Hundido,Finished};
    public GameState gameState = GameState.Idle;
    public float parallaxSpeed = 0.0058f;
    public GameObject uiIdle;
    public GameObject uiPlaying;
    public GameObject uiGameOver;
    public GameObject uiFinish;
    public GameObject ship;
    public GameObject enemyGenerator;
    public Scene intro;
    public Scene principal;
    [Range (0f,120f)]
    public float timePass;
    public GameObject timer;
    public Text tiempo;
    public Text gemas;
    public Text scor;
    public static GameController instance;
    public GameObject gema1;
    public bool sinrecog1 = true;
    public GameObject gema2;
    public bool sinrecog2 = true;
    public GameObject gema3;
    public bool sinrecog3 = true;
    public GameObject gema4;
    public bool sinrecog4 = true;
    public GameObject gema5;
    public bool sinrecog5 = true;
    public float score;
    public bool segund = false;




    private void Awake()
    {
        instance = this;
        score = 0;
        
    }
    //private GameObject shipy;
    void Start()
    {
        ship.SetActive(false);
        //shipy = Instantiate(ship) as GameObject;
    }
    void StartPlaying()
    {
        gameState = GameState.Playing;

        uiIdle.SetActive(false);
        uiGameOver.SetActive(false);
        ship.SetActive(true);
        enemyGenerator.SendMessage("StartGenerator");
        uiPlaying.SetActive(true);
        timePass -= Time.deltaTime;
        tiempo.text = timePass.ToString("000");
    }
    // Update is called once per frame
    void Update()
    {
        Parallax(1);
        if (gameState == GameState.Idle && (Input.GetKeyDown("space") || Input.GetMouseButtonDown(0)))
        {
            StartPlaying();
        } else if (gameState == GameState.Playing)
        {
            timePass -= Time.deltaTime;
            score += Time.deltaTime;
            if (timePass > 2) {
                tiempo.text = timePass.ToString("000");
                Parallax(3);
                if (timePass < 70 && sinrecog1)
                {

                    gema1.SetActive(true);
                }
                if (timePass < 60 && sinrecog2)
                {
                    gema2.SetActive(true);
                }
                if (timePass < 40 && sinrecog3)
                {
                    gema3.SetActive(true);
                }
                if (timePass < 30 && sinrecog4)
                {
                    gema4.SetActive(true);
                }
                if (timePass < 20 && sinrecog5)
                {
                    gema5.SetActive(true);
                }
            }
            else if (timePass < 2 && timePass > 0)
            {
                tiempo.text = timePass.ToString("000");
                enemyGenerator.SendMessage("CancelGenerator");

            }
            else
            {
                tiempo.text = timePass.ToString("000");
                gameState = GameState.Finished;
                ship.SetActive(false);
                uiFinish.SetActive(true);


            }
        } else if (gameState == GameState.Ended)
        {
            uiPlaying.SetActive(false);
        }
        else if (gameState == GameState.Hundido)
        {
           
                uiPlaying.SetActive(false);
                uiGameOver.SetActive(true);
                if (Input.GetKeyDown("space") || Input.GetMouseButtonDown(0))
                {
                    RestarGame();
                }
       
        }
        else if (gameState == GameState.Finished)
        {
            scor.text = score.ToString();
            if (Input.GetKeyDown("space") || Input.GetMouseButtonDown(0))
            {
                if (!segund) { 
                    segund = true;
                    SceneManager.LoadScene("Second");
                    gameState = GameState.Playing;
                    enemyGenerator.SendMessage("ResetTimmer");
                }
                else
                {
                    RestarGame();
                }
            }
        }
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }

    }
    void Parallax(int numero)
    {
        float finalSpeed = (parallaxSpeed * Time.deltaTime)*numero;
        background.uvRect = new Rect(background.uvRect.x + finalSpeed, 0f, 1f, 1f);
        clouds.uvRect = new Rect(clouds.uvRect.x + finalSpeed*12, 0f, 1f, 1f);
    }
    public void RestarGame()
    {
        SceneManager.LoadScene("Principal");
        gameState = GameState.Playing;
    }
    public void ActualizaGema(string tgema=null)
    {
        if (tgema != null) { 
        gemas.text = tgema;
        }
    }
    public void CancelGem(int numero)
    {
        switch (numero) {
            case 1:
                sinrecog1 = false;
                break;
            case 2:
                sinrecog2 = false;
                break;
            case 3:
                sinrecog3 = false;
                break;
            case 4:
                sinrecog4 = false;
                break;
            case 5:
                sinrecog5 = false;
                break;
        }
        
    }
    public void IncrementaScore(int numero)
    {
        score = score + numero;
    }
}
