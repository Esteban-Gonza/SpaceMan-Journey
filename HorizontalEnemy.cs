using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalEnemy : MonoBehaviour{

    [SerializeField] float speed;
    [SerializeField] float obstacleDistance;
    [SerializeField] float enemyDamage;
    [SerializeField] bool moviendoArriba;
    [SerializeField] Transform obstacleController;
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

        rb.velocity = new Vector2(rb.velocity.x, speed);

        if (moviendoArriba == true){

            raycastHitObstacle = Physics2D.Raycast(obstacleController.position, Vector2.up, obstacleDistance, platformMask);
            Debug.DrawRay(obstacleController.transform.position + Vector3.up * obstacleDistance, obstacleController.transform.position);
        }else{

            raycastHitObstacle = Physics2D.Raycast(obstacleController.position, Vector2.down, obstacleDistance, platformMask);
            Debug.DrawRay(obstacleController.transform.position + Vector3.down * obstacleDistance, obstacleController.transform.position);
        }

        if (IsTouchingObstacles()){

            Girar();
        }
    }

    bool IsTouchingObstacles(){

        return raycastHitObstacle.collider != null;
    }

    void Girar(){

        moviendoArriba = !moviendoArriba;
        transform.eulerAngles = new Vector3(0f, 0f, transform.eulerAngles.z + 180);
        speed *= -1;
    }

    void OnCollisionEnter2D(Collision2D collision){

        if (collision.gameObject.CompareTag("Player")){

            collision.gameObject.GetComponent<HealtManager>().TakeDamage(enemyDamage, collision.GetContact(0).normal);
            CameraSystemController.cameraInstance.MoveCamera(5, 5, 0.5f);
            audioSource.Play();
        }
    }
}
