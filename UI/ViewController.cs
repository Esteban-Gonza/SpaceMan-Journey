using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ViewController : MonoBehaviour{

    public TextMeshProUGUI coinsText, scoreText, maxScoreText;
    public Text coinsTextGO, scoreTextGO;
    PlayerController player;

    void Start(){
        
        player = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    void Update(){
        
        if(GameManager.sharedInstance.currentGameState == GameState.inGame){

            int coins = GameManager.sharedInstance.colectableObjects;
            float score = (player.GetTraveledDistance());
            float maxScore = (PlayerPrefs.GetFloat("maxscore", 0f));
            int coinsGO = GameManager.sharedInstance.colectableObjects;
            float scoreGO = (player.GetTraveledDistance());

            coinsText.text = coins.ToString();
            scoreText.text = score.ToString("f1");
            maxScoreText.text = maxScore.ToString("f1");
            coinsTextGO.text = coinsGO.ToString();
            scoreTextGO.text = scoreGO.ToString("f1");
        }
    }
}
