using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFromWater : MonoBehaviour {

    public Transform moveInRelationTo;

    public Transform water;

    Animal[] animalsToMove;
    Vector3[] newPositions;
    Vector3[] oldPositions;

    public AnimationCurve animationCurve;
    public AnimationCurve waterCurve;

    public float lengthOfTime = 1.0f;
    float currentTime;

	// Use this for initialization
	void Start () {
        animalsToMove = transform.GetComponentsInChildren<Animal>();
        newPositions = new Vector3[animalsToMove.Length];
        oldPositions = new Vector3[animalsToMove.Length];
        CalculatePositions();

        titleOldPosition = title.transform.position;
        titleNewPosition = titleOldPosition + new Vector3(0, 50, 0);

        title2OldPosition = title2.transform.position;
        title2NewPosition = title2OldPosition + new Vector3(0, -30, 0);
    }

    public float moveUpAmount = .2f;

    void CalculatePositions()
    {
        int i = 0;
        foreach (var animal in animalsToMove)
        {
            Vector3 diff = animal.transform.position - moveInRelationTo.position;
            float xDiff = diff.x;
            diff.Normalize();

            //animal.transform.position += new Vector3(-xDiff * .4f, moveUpAmount);

            oldPositions[i] = animal.transform.position;
            newPositions[i] = animal.transform.position + new Vector3(-xDiff * .4f, moveUpAmount);
            i++;
        }

        oldWaterPosition = water.transform.position;
        newWaterPosition = oldWaterPosition + new Vector3(0, moveUpAmount, 0);


    }

    public float timeBetweenWaves = 1.5f;
    float waveCounter = 0;
    int wavesPassed = 0;
    int maxWaves = 3;

    Vector3 newWaterPosition;
    Vector3 oldWaterPosition;

    public Transform title;
    Vector3 titleOldPosition;
    Vector3 titleNewPosition;

    public Transform title2;
    Vector3 title2OldPosition;
    Vector3 title2NewPosition;

    private void Update()
    {
        if (waveCounter > timeBetweenWaves && wavesPassed < maxWaves)
        {
            CalculatePositions();
            waveCounter = 0;
            currentTime = 0;
            wavesPassed++;
        }

        waveCounter += Time.deltaTime;

        int i = 0;
        foreach (var animal in animalsToMove)
        {
            Vector3 difference = newPositions[i] - oldPositions[i];
            animal.transform.position = oldPositions[i] + (difference * animationCurve.Evaluate(currentTime / lengthOfTime));
            i++;
        }

        if (wavesPassed == maxWaves && currentTime > lengthOfTime + .5f)
        {
            Vector3 titleDifference = titleNewPosition - titleOldPosition;
            title.transform.position = titleOldPosition + (titleDifference * animationCurve.Evaluate((currentTime-(lengthOfTime + .5f)) / (lengthOfTime + .5f)));
        }

        if (wavesPassed == maxWaves && currentTime > lengthOfTime + 1.0f)
        {
            Vector3 titleDifference = title2NewPosition - title2OldPosition;
            title2.transform.position = title2OldPosition + (titleDifference * animationCurve.Evaluate((currentTime - (lengthOfTime + 1.0f)) / (lengthOfTime + 1.0f)));
        }

        Vector3 waterDifference = newWaterPosition - oldWaterPosition;
        water.transform.position = oldWaterPosition + (waterDifference * waterCurve.Evaluate(currentTime / lengthOfTime));

        currentTime += Time.deltaTime;
    }
}
