using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCoins : MonoBehaviour{

    [SerializeField] GameObject[] coinInstans;

    void Start(){

        int n = Random.Range(0, coinInstans.Length);
        Instantiate(coinInstans[n], this.transform.position, this.transform.rotation);
    }
}