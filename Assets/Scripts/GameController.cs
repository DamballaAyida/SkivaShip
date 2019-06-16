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
    public enum GameState { Idle, Playing,Ended,Hundido};
    public GameState gameState = GameState.Idle;
    public float parallaxSpeed = 0.0058f;
    public GameObject uiIdle;
    public GameObject uiPlaying;
    public GameObject uiGameOver;
    public GameObject ship;
    public GameObject enemyGenerator;
    public Scene intro;
    public Scene principal;
    //private GameObject shipy;
    void Start()
    {
        ship.SetActive(false);
        //shipy = Instantiate(ship) as GameObject;
    }

    // Update is called once per frame
    void Update()
    {
        Parallax(1);
        if (gameState == GameState.Idle && (Input.GetKeyDown("space") || Input.GetMouseButtonDown(0)))
        {
            //ship.SendMessage("UpdateState", "Aparece");
            //ship.SendMessage("Revive");
            gameState = GameState.Playing;
            uiIdle.SetActive(false);
            uiGameOver.SetActive(false);
            ship.SetActive(true);
            enemyGenerator.SendMessage("StartGenerator");
        } else if (gameState == GameState.Playing)
        {
            uiPlaying.SetActive(true);
            Parallax(3);
        } else if (gameState == GameState.Ended)
        {
            uiPlaying.SetActive(false);
        }
        if (gameState == GameState.Hundido)
        {

            uiGameOver.SetActive(true);
            if (Input.GetKeyDown("space") || Input.GetMouseButtonDown(0))
            {
                RestarGame();
            }
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


}
