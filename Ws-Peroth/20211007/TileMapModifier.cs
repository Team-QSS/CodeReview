using System.Collections;
using UnityEngine;

public class TileMapModifier : TileMapGenerator
{
    protected override void Start()
    {
        base.Start();

        if (isDebugMode)
        {
            // 재생성 코루틴 호출
            StartCoroutine(ModifyTileMap());
        }
        else
        {
            // 맵 초기화
            InitializeMapData();

            // 맵 생성
            GenerateTileMapObject();
        }

    }

    private IEnumerator ModifyTileMap()
    {
        int mapNumber = 1;
        while (true)
        {
            // 맵 초기화
            InitializeMapData();

            // 맵 생성
            GenerateTileMapObject();

            yield return new WaitForSeconds(1.5f);

            // 디버그 모드면 타일 16번을 전부 공백 타일로 대체
            if (isDebugMode)
            {
                ModifyFilledTiles();
            }

            yield return new WaitForSeconds(2f);

            print($"End [{mapNumber}]");
            mapNumber++;
        }
    }

    private void ModifyFilledTiles()
    {
        for (var y = 0; y < mapY; y++)
        {
            for (var x = 0; x < mapX; x++)
            {
                // 타일 종류를 가져옴
                var tileKind = map[y, x];

                // 타일이 16번일 경우 0번 (공백타일)로 대체
                if (tileKind == 16)
                {
                    // 맵 정보 갱신
                    map[y, x] = 0;

                    // 타일맵 적용
                    tileMapObjects[y, x].GetComponent<SpriteRenderer>().sprite = tileSprites[0];
                }
            }
        }
    }
}

