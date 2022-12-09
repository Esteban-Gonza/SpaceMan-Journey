using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelControler : MonoBehaviour{

    public static LevelControler levelInstance;
    LevelBlock block;
    [SerializeField] Transform levelStartPosition;

    [Space]
    [Header("Levels of difficulty")]
    public List<LevelBlock> easyLevelBlocks = new List<LevelBlock>();
    public List<LevelBlock> mediumLevelBlocks = new List<LevelBlock>();
    public List<LevelBlock> hardLevelBlocks = new List<LevelBlock>();
    public List<LevelBlock> reallyHardLevelBlocks = new List<LevelBlock>();
    public List<LevelBlock> wtfLevelBlocks = new List<LevelBlock>();

    [Space]
    public List<LevelBlock> currentLevelBlocks = new List<LevelBlock>();
    
    void Awake(){

        if (levelInstance == null){

            levelInstance = this;
        }
    }

    void Start(){

        GenerateInitialBlocks();
    }

    public void AddLevelBlock(){

        int randomEL = Random.Range(0, easyLevelBlocks.Count);
        int randomML = Random.Range(0, mediumLevelBlocks.Count);
        int randomHL = Random.Range(0, hardLevelBlocks.Count);
        int randomRHL = Random.Range(0, reallyHardLevelBlocks.Count);
        int randomWTFL = Random.Range(0, wtfLevelBlocks.Count);

        Vector3 spawnPosition = Vector3.zero;

        if(currentLevelBlocks.Count == 0){

            block = Instantiate(easyLevelBlocks[0]);
            spawnPosition = levelStartPosition.position;
        }else{

            if(GameManager.sharedInstance.currentDifficulty == GameDifficulty.easy){

                block = Instantiate(easyLevelBlocks[randomEL]);
                spawnPosition = currentLevelBlocks[currentLevelBlocks.Count - 1].endPoint.position;
            }

            if (GameManager.sharedInstance.currentDifficulty == GameDifficulty.medium){

                block = Instantiate(mediumLevelBlocks[randomML]);
                spawnPosition = currentLevelBlocks[currentLevelBlocks.Count - 1].endPoint.position;
            }

            if (GameManager.sharedInstance.currentDifficulty == GameDifficulty.hard){

                block = Instantiate(hardLevelBlocks[randomHL]);
                spawnPosition = currentLevelBlocks[currentLevelBlocks.Count - 1].endPoint.position;
            }

            if (GameManager.sharedInstance.currentDifficulty == GameDifficulty.reallyHard){

                block = Instantiate(reallyHardLevelBlocks[randomRHL]);
                spawnPosition = currentLevelBlocks[currentLevelBlocks.Count - 1].endPoint.position;
            }

            if (GameManager.sharedInstance.currentDifficulty == GameDifficulty.wtf){

                block = Instantiate(wtfLevelBlocks[randomWTFL]);
                spawnPosition = currentLevelBlocks[currentLevelBlocks.Count - 1].endPoint.position;
            }
        }

        block.transform.SetParent(this.transform, false);

        Vector3 correction = new Vector3(spawnPosition.x - block.startPoint.position.x,
            spawnPosition.y - block.startPoint.position.y, 0);
        block.transform.position = correction;
        currentLevelBlocks.Add(block);
    }

    public void RemoveLevelBlock(){

        LevelBlock oldBlock = currentLevelBlocks[0];
        currentLevelBlocks.Remove(oldBlock);
        Destroy(oldBlock.gameObject);
    }

    public void RemoveAllLevelBlocks(){

        while(currentLevelBlocks.Count > 0){
            RemoveLevelBlock();
        }
    }

    public void GenerateInitialBlocks(){

        for (int i = 0; i < 5; i++){
        
            AddLevelBlock();
        }
    }
}