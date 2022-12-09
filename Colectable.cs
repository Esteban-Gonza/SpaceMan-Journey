using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ColectablesType{

    money, livePotion, jumpPotion, gravityPotion
}

public class Colectable : MonoBehaviour{

    public ColectablesType type = ColectablesType.money;
    public int value = 1;

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

    void Collected(){

        Hide();

        switch (this.type){

            case ColectablesType.money:
                GameManager.sharedInstance.CollectObject(this);
                audioSource.Play();
                break;

            case ColectablesType.livePotion:
                player.GetComponent<PlayerController>().CollectHealt(this.value);
                audioSource.Play();
                Debug.Log("Live potion colected");
                break;
        }
    }

    void OnTriggerEnter2D(Collider2D collision){

        if (collision.tag == "Player"){

            Collected();
        }
    }
}