using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour{

    [SerializeField] float speed;
    [SerializeField] float obstacleDistance;
    [SerializeField] float groundDistance;
    [SerializeField] float enemyDamage;
    [SerializeField] bool moviendoDerecha;
    [SerializeField] Transform obstacleController;
    [SerializeField] Transform groundController;
    [SerializeField] LayerMask platformMask;

    Rigidbody2D rb;
    AudioSource audioSource;
    Animator enemyAnimator;

    RaycastHit2D raycastHitObstacle;

    void Start(){
        
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        enemyAnimator = GetComponent<Animator>();
    }

    void FixedUpdate(){

        RaycastHit2D raycastHitGround = Physics2D.Raycast(groundController.position, Vector2.down, groundDistance);

        rb.velocity = new Vector2(speed, rb.velocity.y);

        if(raycastHitGround == false){

            Girar();
        }

        if(moviendoDerecha == true){

            raycastHitObstacle = Physics2D.Raycast(obstacleController.position, Vector2.right, obstacleDistance, platformMask);
            Debug.DrawRay(obstacleController.transform.position, obstacleController.transform.position + Vector3.right * obstacleDistance);
        }
        else{

            raycastHitObstacle = Physics2D.Raycast(obstacleController.position, Vector2.left, obstacleDistance, platformMask);
            Debug.DrawRay(obstacleController.transform.position, obstacleController.transform.position + Vector3.left * obstacleDistance);
        }

        if (IsTouchingObstacles()){

            Girar();
        }
    }

    bool IsTouchingObstacles(){

        return raycastHitObstacle.collider != null;
    }

    void Girar(){

        moviendoDerecha = !moviendoDerecha;
        transform.eulerAngles = new Vector3(0f, transform.eulerAngles.y + 180, 0f);
        speed *= -1;
    }

    void OnCollisionEnter2D(Collision2D collision){

        if (collision.gameObject.CompareTag("Player")){

            collision.gameObject.GetComponent<HealtManager>().TakeDamage(enemyDamage, collision.GetContact(0).normal);
            CameraSystemController.cameraInstance.MoveCamera(5, 5, 0.5f);
            audioSource.Play();
        }
    }

    void DestroyRocket(){

        Destroy(gameObject);
    }

    void OnDrawGizmos(){
        
        Gizmos.color = Color.green;
        Gizmos.DrawLine(groundController.transform.position, groundController.transform.position + Vector3.down * groundDistance);
    }
}