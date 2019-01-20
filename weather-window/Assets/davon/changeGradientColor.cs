using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class changeGradientColor : MonoBehaviour {

    public Material GradientSky;
    public Slider mySlider;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        ChangeColor();

    }

   public void ChangeColor()
    {
        GradientSky.SetColor ("_Color", new Color(1, mySlider.value, mySlider.value));
       //GradientSky.SetColor("_Color", new Color(255, 255, 255));

    }
}
