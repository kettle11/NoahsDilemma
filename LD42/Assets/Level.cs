using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour {

    public GameObject tileGameObject;
    public Color tileColor1;
    public Color tileColor2;
    public Color backgroundColor;

    public bool disableImmediately = true;

    Grid grid;
	// Use this for initialization
	void Start () {
        grid = GetComponent<Grid>();
        grid.cellSize = new Vector3(1 + tilePadding, 1 + tilePadding, 1 + tilePadding);
        // CreateLevel();

        if (disableImmediately)
        {
            this.gameObject.SetActive(false);
        }
    }

    struct Tile
    {
        public bool occupied;
        public bool visible;
    };

    Tile[,] tiles;

    float tilePadding = .0f;

    public void CreateLevel()
    {
        grid = GetComponent<Grid>();
        grid.cellSize = new Vector3(1 + tilePadding, 1 + tilePadding, 1 + tilePadding);

        var transformFound = transform.Find("LevelData");
        BoxCollider2D[] animals = transformFound.GetComponentsInChildren<BoxCollider2D>();

        int minX = int.MaxValue;
        int minY = int.MaxValue;

        int maxX = int.MinValue;
        int maxY = int.MinValue;

        foreach (BoxCollider2D animal in animals)
        {
            Vector3 position = grid.LocalToCell(animal.bounds.center);

            if (position.x < minX) minX = (int)position.x;
            if (position.y < minY) minY = (int)position.y;

            if (position.x > maxX) maxX = (int)position.x;
            if (position.y > maxY) maxY = (int)position.y;
        }

        tiles = new Tile[(maxX +1) - minX, (maxY+1) - minY];

        foreach (BoxCollider2D animal in animals)
        {
            Vector3 position = grid.LocalToCell(animal.bounds.center);
            int xPos = (int)position.x - minX;
            int yPos = (int)position.y - minY;

            tiles[xPos, yPos].visible = true;

           // Destroy(animal.gameObject);
        }

        foreach (BoxCollider2D animal in animals)
        {
            animal.gameObject.SetActive(false);
         //   Destroy(animal.gameObject);
        }

        for (int i = 0; i < tiles.GetLength(0); ++i)
        {
            for (int j = 0; j < tiles.GetLength(1); j++)
            {
                if (tiles[i, j].visible)
                {
                    GameObject tile = GameObject.Instantiate(tileGameObject, this.transform);

                    if (j % 2 != i % 2)
                    {
                        tile.GetComponent<SpriteRenderer>().color = tileColor1;
                    } else
                    {
                        tile.GetComponent<SpriteRenderer>().color = tileColor2;
                    }

                    tile.transform.localScale += Vector3.one * tilePadding;
                    tile.transform.localPosition = new Vector3(i, j, -.3f) * (1.0f + tilePadding) + (grid.cellSize / 2f);
                }
            }
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
