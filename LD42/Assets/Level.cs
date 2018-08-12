using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour {

    public GameObject tileGameObject;
    public Color tileColor1;
    public Color tileColor2;
    public Color backgroundColor;

    public bool disableImmediately = true;

    public int numberOfTilesNeededToWin = 0;
    int currentNumberOfTilesCovered = 0;

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
        public Animal occupiedBy;
        public bool visible;
    };

    Tile[,] tiles;

    float tilePadding = .0f;

    int minX;
    int minY;

    int maxX;
    int maxY;

    public void CreateLevel()
    {
        grid = GetComponent<Grid>();
        grid.cellSize = new Vector3(1 + tilePadding, 1 + tilePadding, 1 + tilePadding);

        var transformFound = transform.Find("LevelData");
        BoxCollider2D[] animals = transformFound.GetComponentsInChildren<BoxCollider2D>();

        minX = int.MaxValue;
        minY = int.MaxValue;

        maxX = int.MinValue;
        maxY = int.MinValue;

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
            tiles[xPos, yPos].occupied = false;
            numberOfTilesNeededToWin++;

           // Destroy(animal.gameObject);
        }

        maxX -= minX;
        maxY -= minY;
        minX = 0;
        minY = 0;

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

    public void RemoveAnimal(Animal animal)
    {
        for(int i = 0; i < tiles.GetLength(0); ++i)
        {
            for (int j = 0; j < tiles.GetLength(1); ++j)
            {
                if (tiles[i, j].occupiedBy == animal)
                {
                    tiles[i, j].occupied = false;
                    tiles[i, j].occupiedBy = null;
                    currentNumberOfTilesCovered--;
                }
            }
        }
    }

    public bool CheckPlaceAnimal(Animal animal, ref bool partiallyWithinGrid)
    {
        BoxCollider2D[] animals = animal.GetComponentsInChildren<BoxCollider2D>();

        bool entirelyWithinGrid = true;
        bool cannotPlaceHere = false;

        foreach (var collider in animals)
        {
            Vector3 position = grid.LocalToCell(collider.bounds.center);
            int xPos = (int)position.x - minX;
            int yPos = (int)position.y - minY;

            if (position.x < minX || position.x > maxX )
            {
                entirelyWithinGrid = false;
                continue;
            }

            if (position.y < minY || position.y > maxY)
            {
                entirelyWithinGrid = false;
                continue;
            }


            if (tiles[xPos, yPos].visible)
            {
                partiallyWithinGrid = true;
            }

            if (tiles[xPos, yPos].occupied || !tiles[xPos, yPos].visible)
            {
                cannotPlaceHere = true;
            }
        }

        if (cannotPlaceHere)
        {
            return false;
        }

        if (!partiallyWithinGrid)
        {
            return false;
        }

        if (partiallyWithinGrid && !entirelyWithinGrid)
        {
            return false;
        }

        foreach (var collider in animals)
        {
            Vector3 position = grid.LocalToCell(collider.bounds.center);
            int xPos = (int)position.x - minX;
            int yPos = (int)position.y - minY;

            tiles[xPos, yPos].occupied = true;
            tiles[xPos, yPos].occupiedBy = animal;
            currentNumberOfTilesCovered++;
        }

        if(currentNumberOfTilesCovered >= numberOfTilesNeededToWin)
        {
            LevelManager.instance.Victory();
        }

        return true;
    }

    public Vector3 ReturnSnappingDiff(Animal animal)
    {
        BoxCollider2D[] animals = animal.GetComponentsInChildren<BoxCollider2D>();
        Vector3 position = grid.LocalToCell(animals[0].bounds.center - this.transform.position);
        return position + new Vector3(.5f, .5f, 0) - animals[0].bounds.center;
    }

    // Update is called once per frame
    void Update () {
		
	}
}
