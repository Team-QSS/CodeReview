using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMapMaker : MapGenerator
{
    [SerializeField] private GameObject tile;   // Dummy Tile Object
    [SerializeField] private List<Sprite> tileSprites = new List<Sprite>(); // Tilemap Sprites

    protected GameObject[,] tileMapObjects = new GameObject[MapInformation.y, MapInformation.x];

    private Vector3 worldStart;
    private int tileKind = 0;   // Kind of Current Tile
    private float tileSize; // Size of Tile

    protected void GenerateTileMapObject()
    {
        // Set Tile Size
        tileSize = tile.GetComponent<SpriteRenderer>().sprite.bounds.size.x;

        // Set Start Position
        worldStart = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height));

        // Generate Start
        GenerateRandomMap();
        CreatTilemap();

        // Generate End
        progressCount = 1;
    }

    private void CreatTilemap()
    {
        for(int y = 0; y < mapY; y++)
        {
            for(int x = 0; x < mapX; x++)
            {
                tileKind = map[y, x];
                tileMapObjects[y, x] = PlaceTile(x, y);
            }
        }
    }

    private GameObject PlaceTile(int x, int y)
    {
        GameObject newTile =
            Instantiate(tile,
                        new Vector3(worldStart.x + (tileSize * x), worldStart.y - (tileSize * y), 0),
                        Quaternion.identity,
                        transform
                        );

        newTile.GetComponent<SpriteRenderer>().sprite = tileSprites[tileKind];  // Set Sprite
        return newTile;
    }


}