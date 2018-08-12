using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeIn : MonoBehaviour {

    public float delay;
    float timer = 0;
    public AnimationCurve curve;

    public float fadeInLength = 2.0f;

    Image image;
	// Use this for initialization
	void Start () {
        image = GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;

        if (timer > delay)
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, curve.Evaluate((timer - delay) / fadeInLength));
        }
	}
}
