using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : MonoBehaviour {

    public string animalName;
    public bool collidersSetup = false;

    Vector2 origin;
	// Use this for initialization
	void Awake () {
        origin = this.transform.Find("origin").localPosition;

        if (!collidersSetup)
        {
            AnimalColliders.CreateColliders(this.gameObject, origin, animalName);
            collidersSetup = true;
        }
       // transform.position = new Vector3(transform.position.x, transform.position.y, -1); // All animals are -1 so they're above the background
        // This fixes potentially small imperfections in placement when designing levels. It's important animals are only rotated to 90 degrees.
        if (Mathf.Abs(Mathf.DeltaAngle(this.gameObject.transform.eulerAngles.z, 0)) < 30)
        {
            this.gameObject.transform.eulerAngles = new Vector3(0, 0, 0);
        }
        if (Mathf.Abs(Mathf.DeltaAngle(this.gameObject.transform.eulerAngles.z, 90)) < 30)
        {
            this.gameObject.transform.eulerAngles = new Vector3(0, 0, 90);
        }
        if (Mathf.Abs(Mathf.DeltaAngle(this.gameObject.transform.eulerAngles.z, 180)) < 30)
        {
            this.gameObject.transform.eulerAngles = new Vector3(0, 0, 180);
        }
        if (Mathf.Abs(Mathf.DeltaAngle(this.gameObject.transform.eulerAngles.z, 270)) < 30)
        {
            this.gameObject.transform.eulerAngles = new Vector3(0, 0, 270);
        }

    }

    bool moving = false;
    bool previouslyInGrid = false;

	// Update is called once per frame
	void Update () {
        if (!Input.GetMouseButton(0))
        {

            if (moving)
            {
                bool partiallyWithinGrid = false;
                bool canPlaceHere = LevelManager.currentLevel.CheckPlaceAnimal(this, ref partiallyWithinGrid);

                if(!canPlaceHere && partiallyWithinGrid)
                {
                    transform.position = originalPosition;
                    transform.rotation = originalRotation;

                    SoundSource.PlayError();
                }

                if (canPlaceHere)
                {
                    SoundSource.PlayDropGrid();

                    previouslyInGrid = true;
                    Vector3 snappingDif = LevelManager.currentLevel.ReturnSnappingDiff(this);
                    this.transform.position += snappingDif;
                } else if (previouslyInGrid)
                {
                    SoundSource.PlayDropGrid(); // Could be a different sound eventually

                    // If the animal was previously in the grid place it back where it back into the grid where it started.
                    previouslyInGrid = true;
                    LevelManager.currentLevel.CheckPlaceAnimal(this, ref partiallyWithinGrid);
                }

                if (!partiallyWithinGrid)
                {
                    SoundSource.PlayDrop();
                }
            }

            moving = false;
        }

        if (moving)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = mousePos - moveOffset;
            transform.position = new Vector3(transform.position.x, transform.position.y, -1);

            if (Input.GetKeyDown(KeyCode.R) || Input.GetKeyDown(KeyCode.Space))
            {
                SoundSource.PlayRotate();
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
    Vector3 originalPosition;
    Quaternion originalRotation;

    void OnMouseOver()
    {
        //If your mouse hovers over the GameObject with the script attached, output this message
       // Debug.Log("Mouse is over " + animalName);

        if (Input.GetMouseButtonDown(0))
        {
            if (previouslyInGrid)
            {
                SoundSource.PlayPickupGrid();
            }
            else
            {
                SoundSource.PlayPickup();
            }

            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            moveOffset = mousePos - transform.position;
            moving = true;

            originalPosition = transform.position;
            originalRotation = transform.rotation;

            LevelManager.currentLevel.RemoveAnimal(this);
        }
    }

}
