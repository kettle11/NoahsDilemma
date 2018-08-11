using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalColliders : MonoBehaviour {

    static int[,] giraffe = new int[,]
    {
        { 0, 1 },
        { 0, 1 },
        { 1, 1 },
        { 1, 1 }
    };

    static int[,] elephant = new int[,]
    {
        { 0, 1 , 1},
        { 1, 1 , 1},
        { 1, 1 , 1}
    };

    static int[,] lion = new int[,]
    {
        { 0, 1},
        { 1, 1 }
    };

    static int[,] alligator = new int[,]
    {
        { 1, 1, 1, 1},
    };

    static int[,] hippo = new int[,]
{
        { 1, 1 , 1},
        { 1, 1 , 1}
};

    static int[,] penguin = new int[,]
{
        { 1 }       
};

    static int[,] trex = new int[,]
{
        { 0, 0, 0, 1, 1, 1 },
        { 0, 0, 0, 1, 1, 1 },
        { 0, 1, 1, 1, 1, 0 },
        { 1, 1, 1, 1, 0, 0 },
        { 0, 1, 1, 1, 0, 0 },
};

static int[,] disguised_trex = new int[,]
{
        { 0, 0, 0, 1, 1, 1 },
        { 0, 0, 0, 1, 1, 1 },
        { 0, 1, 1, 1, 1, 0 },
        { 1, 1, 1, 1, 1, 0 },
        { 0, 1, 1, 1, 0, 0 },
};

    static Dictionary<string, int[,]> animalNameToData = new Dictionary<string, int[,]>
    {
        {"giraffe", giraffe },
        {"elephant", elephant },
        {"lion", lion  },
        {"hippo", hippo },
        {"penguin", penguin },
        {"alligator", alligator },
        {"trex", trex},
        {"disguised_trex", disguised_trex }
    };

    // Use this for initialization
    void Start () {
		
	}

    public static float tileSize = 270;

    public static void CreateColliders(GameObject gameObject, Vector2 originOffset, string animal)
    {
        int[,] dataArray = animalNameToData[animal];

        for (int i = 0; i < dataArray.GetLength(0); ++i)
        {
            for (int j = 0; j < dataArray.GetLength(1); ++j)
            {
                if (dataArray[i, j] == 1)
                {
                    BoxCollider2D boxCollider = gameObject.AddComponent<BoxCollider2D>();
                    boxCollider.offset = new Vector2(j + .5f, dataArray.GetLength(0) - i - .5f) + originOffset;
                    boxCollider.size = new Vector2(1, 1);
                }
            }
        }
    }
}
