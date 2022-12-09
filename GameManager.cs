using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public enum GameState{

    Ui, inGame, gameOver
}

public enum GameDifficulty{

    easy, medium, hard, reallyHard, wtf
}

public class GameManager : MonoBehaviour{

    [SerializeField] AudioSource gameOverSound;
    [SerializeField] AudioSource changeDifficultySound;
    public GameObject gameOverCanvas;
    public GameObject onGameCanvas;

    public static GameManager sharedInstance;
    public GameState currentGameState = GameState.Ui;
    public GameDifficulty currentDifficulty = GameDifficulty.easy;
    public int colectableObjects = 0;

    Background background;
    PlayerController playerController;

    void Awake(){

        if (sharedInstance == null){
        
            sharedInstance = this;
        }
    }
    void Start() {

        background = GameObject.Find("Background").GetComponent<Background>();
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        currentGameState = GameState.inGame;
        background.xCordinate = 1;
    }

    void Update(){

        if (Input.GetKey(KeyCode.F) && currentGameState == GameState.gameOver){

            StartGame();
        }

        if(colectableObjects < 5){

            EasyDificulty();
            currentDifficulty = GameDifficulty.easy;
        }
        else if(colectableObjects >= 5 && colectableObjects < 15){

            MediumDificulty();
            currentDifficulty = GameDifficulty.medium;
            changeDifficultySound.Play();
        }
        else if(colectableObjects >= 15 && colectableObjects < 30){

            HardDificulty();
            currentDifficulty = GameDifficulty.hard;
            changeDifficultySound.Play();
        }
        else if(colectableObjects >= 30 && colectableObjects < 50){

            ReallyHardDificulty();
            currentDifficulty = GameDifficulty.reallyHard;
            changeDifficultySound.Play();
        }
        else if (colectableObjects >= 50){

            WtfDificulty();
            currentDifficulty = GameDifficulty.wtf;
            changeDifficultySound.Play();
        }
    }

    public void StartGame(){

        SetGamestate(GameState.inGame);
        colectableObjects = 0;
    }

    public void GameOver(){

        SetGamestate(GameState.gameOver);
    }
    
    public void BackToUi(){

        SetGamestate(GameState.Ui);
    }

    void SetGamestate(GameState newGameState){

        if (newGameState == GameState.Ui) {

            onGameCanvas.SetActive(false);
            gameOverCanvas.SetActive(false);

            background.movementSpeed = 0;
            background.xCordinate = 0;
            background.yCordinate = 0;
        }
        else if (newGameState == GameState.inGame) {

            onGameCanvas.SetActive(true);
            gameOverCanvas.SetActive(false);


            background.movementSpeed = 5f;
            background.xCordinate = 1f;
            background.yCordinate = 0;

            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            playerController.StartGame();
        }
        else if (newGameState == GameState.gameOver) {

            gameOverSound.Play();

            background.movementSpeed = 0;
            background.xCordinate = 0;
            background.yCordinate = 0;
        }

        this.currentGameState = newGameState;
    }

    void EasyDificulty(){

        background.movementSpeed = 3.5f;
        playerController.runSpeed = 5f;
        playerController.jumpForce = 6f;
    }

    void MediumDificulty(){

        background.movementSpeed = 4.5f;
        playerController.runSpeed = 6f;
        playerController.jumpForce = 6.5f;
    }

    void HardDificulty(){

        background.movementSpeed = 5.5f;
        playerController.runSpeed = 7f;
        playerController.jumpForce = 7f;
    }

    void ReallyHardDificulty(){

        background.movementSpeed = 6.5f;
        playerController.runSpeed = 8f;
        playerController.jumpForce = 7f;
    }

    void WtfDificulty(){

        background.movementSpeed = 7.5f;
        playerController.runSpeed = 9f;
        playerController.jumpForce = 7f;
    }

    public void CollectObject(Colectable collectable){

        colectableObjects += collectable.value;
    }
}