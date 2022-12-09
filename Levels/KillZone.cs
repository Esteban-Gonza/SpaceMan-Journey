using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZone : MonoBehaviour{

    GameObject player;
    [SerializeField] GameObject gameOverCanvas;
    [SerializeField] GameObject onGameCanvas;

    void Start(){

        player = GameObject.Find("Player");
    }

    void OnTriggerEnter2D(Collider2D collision){

        if (collision.tag == "Player") {

            onGameCanvas.SetActive(false);
            gameOverCanvas.SetActive(true);

            player.GetComponent<PlayerController>().PlayerDeath();
        }
    }
}
