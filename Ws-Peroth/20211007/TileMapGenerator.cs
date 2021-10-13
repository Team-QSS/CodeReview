using System.Collections.Generic;
using UnityEngine;

public abstract class TileMapGenerator : MapGeneratorBSP
{
    [SerializeField]
    protected List<Sprite> tileSprites = new List<Sprite>(); // Tilemap Sprites

    [SerializeField]
    private GameObject _tile;   // Dummy Tile Object

    protected GameObject[,] tileMapObjects;

    private Vector3 _worldStart;

    private int _tileKind;
    private float _tileSize; // 타일 크기

    protected virtual void Start()
    {
        // 타일 생성 시작 지점 설정
        _worldStart = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height));
    }

    protected override void InitializeMapData(int X = MapInformation.X, int Y = MapInformation.Y)
    {
        base.InitializeMapData(X, Y);

        if (tileMapObjects == null)
        {
            InitializeTileMapObjects();
        }
    }

    private void InitializeTileMapObjects()
    {
        // 타일 크기 설정
        _tileSize = _tile.GetComponent<SpriteRenderer>().sprite.bounds.size.x;

        // 타일맵 배열 할당
        tileMapObjects = new GameObject[mapY, mapX];

        // 타일맵 배열 초기화
        for (var y = 0; y < mapY; y++)
        {
            for (var x = 0; x < mapX; x++)
            {
                // Dummy Tile 생성
                var newTile =
                    Instantiate(_tile,
                                new Vector3(_worldStart.x + (_tileSize * x), _worldStart.y - (_tileSize * y), 0),
                                Quaternion.identity,
                                transform
                                );

                tileMapObjects[y, x] = newTile;
            }
        }
    }

    protected void GenerateTileMapObject()
    {
        // 맵 생성 시작
        GenerateMapBSP();

        // 타일맵 생성
        GenerateTileMap();
    }

    private void GenerateTileMap()
    {
        for(var y = 0; y < mapY; y++)
        {
            for(var x = 0; x < mapX; x++)
            {
                // 해당 위치의 타일맵에 스프라이트 적용
                ApplyTileMapSprite(x, y);
            }
        }
    }

    private GameObject ApplyTileMapSprite(int x, int y)
    {
        // map에서 생성할 타일과 타일의 종류를 가져옴
        _tileKind = map[y, x];
        var targetTile = tileMapObjects[y, x];

        // 타일 종류에 맞는 Sprite 부여
        targetTile.GetComponent<SpriteRenderer>().sprite = tileSprites[_tileKind];
        return targetTile;
    }
}