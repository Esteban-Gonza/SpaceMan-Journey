using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraSystemController : MonoBehaviour{

    public static CameraSystemController cameraInstance;

    CinemachineVirtualCamera cinemachineCamera;
    CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin;

    float movementTime;
    float totalMovementTime;
    float initialIntensity;

    void Awake(){
        
        cameraInstance = this;
        cinemachineCamera = GetComponent<CinemachineVirtualCamera>();
        cinemachineBasicMultiChannelPerlin = cinemachineCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    public void MoveCamera(float intensity, float frequency, float time){

        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;
        cinemachineBasicMultiChannelPerlin.m_FrequencyGain = frequency;
        initialIntensity = intensity;
        totalMovementTime = time;
        movementTime = time;
    }

    void Update() { 
    
        if(movementTime > 0){

            movementTime -= Time.deltaTime;
            cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = Mathf.Lerp(initialIntensity, 0, 1 - (movementTime/totalMovementTime));
        }
    }
}
