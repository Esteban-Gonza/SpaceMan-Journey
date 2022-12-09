using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    //Movement parameters
    public float jumpForce = 6f;
    public float runSpeed = 5f;
    public bool canMove = true;
    
    //Components
    public Rigidbody2D playerRigid;
    public SpriteRenderer playerRenderer;
    CapsuleCollider2D playerColider;
    Animator playerAnim;

    //Jump methods parameters
    float coyoteTime = 0.2f;
    float coyoteTimeCounter;
    float jumpBufferTime = 0.2f;
    float jumpBufferCounter;
    [SerializeField] Vector2 bouncingSpeed;
    [SerializeField] LayerMask platformMask;

    Vector3 startedPosition;
    public float healtPoints;

    //Constants
    public const int INITIAL_HEALT = 100, MAX_HEALT = 200, MIN_HEALT = 10;
    const string ground_State = "IsOnTheGround";
    const string alive_State = "IsAlive";
    const string speed_State = "Speed";

    void Awake(){

        playerRigid = GetComponent<Rigidbody2D>();
        playerColider = GetComponent<CapsuleCollider2D>();
        playerAnim = GetComponent<Animator>();
        playerRenderer = GetComponent<SpriteRenderer>();
    }

    void Start(){

        playerAnim.SetBool(alive_State, true);
        playerAnim.SetBool(ground_State, false);
        startedPosition = this.transform.position;
    }

    void FixedUpdate() {

        playerRigid.constraints = RigidbodyConstraints2D.FreezeRotation;

        if(GameManager.sharedInstance.currentGameState == GameState.inGame){

            //Player movement
            if (canMove){


                if (IsTouchingTheGrounnd()){

                    coyoteTimeCounter = coyoteTime;
                }else{

                    coyoteTimeCounter -= Time.deltaTime;
                }

                if (Input.GetKey(KeyCode.UpArrow)){

                    jumpBufferCounter = jumpBufferTime;
                }else{

                    jumpBufferCounter -= Time.deltaTime;
                }

                if (coyoteTimeCounter > 0f && jumpBufferCounter > 0f){

                    playerRigid.velocity = Vector2.up * jumpForce;

                    jumpBufferCounter = 0f;
                }

                if(Input.GetKeyUp(KeyCode.UpArrow) && playerRigid.velocity.y > 0f){

                    playerRigid.velocity = new Vector2(playerRigid.velocity.x, playerRigid.velocity.y * 0.5f);
                    coyoteTimeCounter = 0f;
                }

                playerAnim.SetBool(ground_State, IsTouchingTheGrounnd());

                if (Input.GetKey(KeyCode.LeftArrow)){

                    playerRigid.velocity = new Vector2(-runSpeed, playerRigid.velocity.y);
                    playerAnim.SetFloat(speed_State, playerRigid.velocity.magnitude);
                    playerRenderer.flipX = true;
                }else{

                    if (Input.GetKey(KeyCode.RightArrow)){

                        playerRigid.velocity = new Vector2(+runSpeed, playerRigid.velocity.y);
                        playerAnim.SetFloat(speed_State, playerRigid.velocity.magnitude);
                        playerRenderer.flipX = false;
                    }else{

                        playerRigid.velocity = new Vector2(0, playerRigid.velocity.y);
                        playerRigid.constraints = RigidbodyConstraints2D.FreezePositionX |
                            RigidbodyConstraints2D.FreezeRotation;
                        playerAnim.SetFloat(speed_State, 0);
                    }
                }
            }  
        }
    }

    public void StartGame(){

        playerAnim.SetBool(alive_State, true);
        playerAnim.SetBool(ground_State, false);

        Invoke("RestartPosition", 0.1f);

        healtPoints = INITIAL_HEALT;
    }

    void RestartPosition(){

        this.transform.position = startedPosition;
        this.playerRigid.velocity = Vector2.zero;
        GameObject background = GameObject.Find("Background");
        background.transform.position = startedPosition;
    }

    bool IsTouchingTheGrounnd(){

        /* RAYCAST CODE!!!!!!!!!!!!!!!!!! */
        float extraHeightText = 0.1f;
        RaycastHit2D raycastHit = Physics2D.Raycast(playerColider.bounds.center, Vector2.down, 
            playerColider.bounds.extents.y + extraHeightText, platformMask);
        Color rayColor;
        if(raycastHit.collider != null){

            rayColor = Color.green;
        }else{

            rayColor = Color.red;
        }

        Debug.DrawRay(playerColider.bounds.center, 
            Vector2.down * (playerColider.bounds.extents.y + extraHeightText), rayColor);

        return raycastHit.collider != null;
    }

    public void PlayerDeath(){

        float traveledDistance = GetTraveledDistance();
        float previousMaxDistance = PlayerPrefs.GetFloat("maxscore", 0f);

        if (traveledDistance > previousMaxDistance){

            PlayerPrefs.SetFloat("maxscore", traveledDistance);
        }

        GameManager.sharedInstance.GameOver();
        this.playerAnim.SetBool(alive_State, false);
    }

    public void Bouncing(Vector2 hitSpot){

        playerRigid.velocity = new Vector2(-bouncingSpeed.x * hitSpot.x, bouncingSpeed.y);
    }

    public void CollectHealt(int hPoints){

        this.healtPoints += hPoints;

        if(this.healtPoints >= MAX_HEALT){

            this.healtPoints = MAX_HEALT;
        }

        if (this.healtPoints <= 0){ 
        
            PlayerDeath();
        }
    }

    public float GetHealtPoints(){

        return healtPoints;
    }

    public float GetTraveledDistance(){
        return this.transform.position.x - startedPosition.x;
    }
}