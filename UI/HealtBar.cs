using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealtBar : MonoBehaviour{

    Slider slider;
    
    void Start(){
        
        slider = GetComponent<Slider>();
        slider.maxValue = PlayerController.MAX_HEALT;
        slider.minValue = 0;
    }

    
    void Update(){
        
        slider.value = GameObject.Find("Player").GetComponent<PlayerController>().GetHealtPoints();
    }
}