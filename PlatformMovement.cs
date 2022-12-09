using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour{

    [SerializeField] GameObject platform;
    [SerializeField] Transform startPoint;
    [SerializeField] Transform endPoint;
    [SerializeField] float speed;

    Vector3 goTo;

    void Start(){

        goTo = endPoint.position;
    }
}