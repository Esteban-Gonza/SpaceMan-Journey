using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityPotion : MonoBehaviour{

    GameObject player;
    AudioSource audioSource;
    SpriteRenderer sprite;
    CircleCollider2D cirCollider;


    void Awake(){

        sprite = GetComponent<SpriteRenderer>();
        cirCollider = GetComponent<CircleCollider2D>();
        audioSource = GetComponent<AudioSource>();
        player = GameObject.Find("Player");
    }

    void Hide(){

        sprite.enabled = false;
        cirCollider.enabled = false;
    }

    void OnTriggerEnter2D(Collider2D collision){

        if (collision.gameObject.CompareTag("Player")){

            Hide();
            audioSource.Play();
            StartCoroutine(ChangeGravity());
        }
    }

    IEnumerator ChangeGravity(){

        player.GetComponent<PlayerController>().playerRigid.gravityScale = 0.6f;
        player.GetComponent<PlayerController>().playerRenderer.color = new Color(150, 165, 255, 255);
        yield return new WaitForSeconds(5f);
        player.GetComponent<PlayerController>().playerRigid.gravityScale = 1;
        player.GetComponent<PlayerController>().playerRenderer.color = new Color(255, 255, 255, 255);
    }
}