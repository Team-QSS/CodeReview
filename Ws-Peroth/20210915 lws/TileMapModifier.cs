using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMapModifier : TileMapMaker
{
    // Start is called before the first frame update
    void Start()
    {
        GenerateTileMapObject();
        StartCoroutine(ModifyTileMap());
    }

    private IEnumerator ModifyTileMap()
    {
        // 맵과 맵 사이 막힌 부분을 열어주려함
        // 아직 구현 중
        yield return null;
    }
}
