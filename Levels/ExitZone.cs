using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitZone : MonoBehaviour{

    void OnTriggerEnter2D(Collider2D collision){
        
        if(collision.tag == "Player"){
            LevelControler.levelInstance.AddLevelBlock();
            LevelControler.levelInstance.RemoveLevelBlock();
        }
    }
}