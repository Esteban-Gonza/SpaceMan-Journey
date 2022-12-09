using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour{

    public float movementSpeed;
    public float xCordinate;
    public float yCordinate;

    void Update(){

        transform.Translate(new Vector2(xCordinate, yCordinate) * movementSpeed * Time.deltaTime);
    }
}
