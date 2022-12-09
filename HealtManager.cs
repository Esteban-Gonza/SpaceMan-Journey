using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealtManager : MonoBehaviour{

    public float life;
    [SerializeField] float noControlTime;

    PlayerController player;
    Animator playerAnimator;

    void Start(){
        
        player = GetComponent<PlayerController>();
        playerAnimator = GetComponent<Animator>();
    }

    public void TakeDamage(float damage){

        player.healtPoints -= damage;
    }

    public void TakeDamage(float damage, Vector2 playerPosition){

        player.healtPoints -= damage;
        playerAnimator.SetTrigger("Hit");
        StartCoroutine(LoseControl());
        StartCoroutine(NoCollision());
        player.Bouncing(playerPosition);
    }

    IEnumerator LoseControl(){

        player.canMove = false;
        yield return new WaitForSeconds(noControlTime);
        player.canMove = true;
    }

    IEnumerator NoCollision(){

        Physics2D.IgnoreLayerCollision(6,7, true);
        yield return new WaitForSeconds(noControlTime);
        Physics2D.IgnoreLayerCollision(6, 7, false);
    }
}