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
/*
- 현재 구상하는 방법 -
- 타입 맵의 죄측 상단부터 탐색함.
- 만약 현재 블럭으로 부터 오른쪽, 아래쪽, 우측 하단 대각선쪽으로 1블럭씩, 총 4개의 블럭이 전부 차 있는 블럭인 경우가 나올 때 까지 탐색
- 위의 경우를 탐색했을 경우 어느 방향으로 이어져 있는지 보기 위해 상하좌우 방향 2블럭을 확인함
- 그중 먼저 확인된 방향의 이어야 하는 부분 (모서리)와 뚫어야 하는 부분 (벽면)을 구분함
- 구분하여 뚫어야 하는 부분에는 0을, 아닌 부분은 그대로 둔다
- 한쪽 방향의 수정을 완료하였으면 다른 쪽 방향의 수정도 진행함
- 이렇게 전체를 돈 다음 맵 확인을 진행
- 이때 수정된 맵은 tileMapObjects에서 해당 위치의 타일맵의 Sprite를 교체한다.
- 맵 확인부분에서는 위의 방법대로 구현되었을 때 발생 가능한 문제부분을 검사하여 수정할 계획
---> 다시 확인해보니 type map을 0으로 지워주어서 힘들거라 예상
---> typemap clone으로 복사된걸 사용하던,
---> 다른 방식으로 맵 생성 과정을 갈아엎어야 할듯
*/
yield return null;
}
}
