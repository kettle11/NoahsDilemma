using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : MonoBehaviour {

    public string animalName;

    Vector2 origin;
	// Use this for initialization
	void Start () {
        origin = this.transform.Find("origin").localPosition;
        AnimalColliders.CreateColliders(this.gameObject, origin, animalName);
	}

    bool moving = false;

	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonUp(0))
        {
            moving = false;
        }

        if (moving)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = mousePos - moveOffset;
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);

            if (Input.GetKeyDown(KeyCode.R))
            {
                Rotate(1);
            }
        }
    }


    void Rotate(int direction)
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        transform.RotateAround(mousePos, Vector3.back, 90);
        moveOffset = mousePos - transform.position;
    }

    Vector3 moveOffset;
    void OnMouseOver()
    {
        //If your mouse hovers over the GameObject with the script attached, output this message
       // Debug.Log("Mouse is over " + animalName);

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            moveOffset = mousePos - transform.position;
            moving = true;
        }
    }

}
