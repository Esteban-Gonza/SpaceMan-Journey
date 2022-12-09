using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TrabsitionsController : MonoBehaviour{

    public static TrabsitionsController transitionInstance;
    
    [SerializeField] AnimationClip endAnimation;
    [SerializeField] Canvas menuCanvas;
    [SerializeField] Canvas optionCanvas;

    AudioSource audioSource;
    Animator animator;

    void Awake(){

        if (transitionInstance == null){

            transitionInstance = this;
        }
    }

    void Start(){

        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }

    public void StartChange(){

        StartCoroutine(ChangeScene());
    }

    IEnumerator ChangeScene(){

        animator.SetTrigger("Iniciar");

        yield return new WaitForSeconds(endAnimation.length);

        SceneManager.LoadScene("GameScene");
    }

    public void PlaySound(){

        audioSource.Play();
    }

    public void Options(){

        menuCanvas.gameObject.SetActive(false);
        optionCanvas.gameObject.SetActive(true);
    }
}