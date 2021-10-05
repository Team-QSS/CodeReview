using UnityEngine;

public class MapGeneratorBSP : MapInitializer
{
    // 각 구역별 border구역 크기
    private readonly int _borderSizeX = 2;
    private readonly int _borderSizeY = 2;

    // 통로 최대 범위
    private readonly int _wideMax = 3;

    // 방 분할 비율
    private int RoomDivideRatio => Random.Range(0, 3) + 4;

    private int GetTileNumber()
    {
        // 디버그 모드면 채워진 블럭 반환, 
        if (isDebugMode)
        {
            return 16;
        }
        // 아니면 비어있는 블럭 반환
        return 0;
    }

    private int GetBorderLimit(int value)
    {
        if (value > 1)
        {
            return value;
        }
        return 1;
    }

    protected override void InitializeMapData(int X = MapInformation.X, int Y = MapInformation.Y)
    {
        // 맵 정보 초기화
        base.InitializeMapData(X, Y);
    }

    protected void GenerateMapBSP()
    {
        // 맵 생성 시작
        DivideMap(10, 0, 0, mapX, mapY);
    }

    private MapLocationPosition DivideMap(int depth, int startX, int startY, int endX, int endY)
    {
        // depth  : 깊이
        var lengthX = endX - startX;
        var lengthY = endY - startY;

        // 깊이가 0 이거나 사용 가능한 공간이 10 이하일 경우에 방을 생성
        if (depth == 0 || lengthX <= 10 || lengthY <= 10)
        {
            return GenerateRoom(startX, startY, endX, endY);
        }
        MapLocationPosition firstRoom, secondRoom;
        int dividenum;

        // X의 길이가 더 길 경우 세로방향으로 분할
        if (lengthX > lengthY)
        {
            dividenum = RoomDivideRatio * lengthX / 10;

            // x 좌표의 끝을 현재 x + 나눌 범위 로 갱신 
            firstRoom = DivideMap(depth - 1, startX, startY, startX + dividenum, endY);

            // x 좌표의 시작부분을 현재 x + 나눌 범위로 갱신
            secondRoom = DivideMap(depth - 1, startX + dividenum, startY, endX, endY);

            // 분할한 두 방을 병합
            MergeVerticalRoom(firstRoom, secondRoom);
        }
        else    // 가로 방향으로 분할
        {
            dividenum = RoomDivideRatio * lengthY / 10;

            // y 좌표의 끝을 현재 y + 나눌 범위로 갱신
            firstRoom = DivideMap(depth - 1, startX, startY, endX, startY + dividenum);

            // y 좌표의 시작 부분을 현재 y + 나눌 범위로 갱신
            secondRoom = DivideMap(depth - 1, startX, startY + dividenum, endX, endY);

            // 분할한 두 방을 병합
            MergeHorizonRoom(firstRoom, secondRoom);
        }

        return new MapLocationPosition(firstRoom.startX, firstRoom.startY, firstRoom.endX, firstRoom.endY);
    }

    private MapLocationPosition GenerateRoom(int startX, int startY, int endX, int endY)
    {
        // (startX, startY) ~ (endX, endY) 의 구역 중, border만큼을 띄우고 방을 생성
        for (var y = startY + _borderSizeY; y < endY - _borderSizeY; y++)
        {
            for (var x = startX + _borderSizeX; x < endX - _borderSizeX; x++)
            {
                map[y, x] = 0;
            }
        }

        // 실제 방의 위치를 반환함
        // 방이 생성된 위치는 border의 안쪽임으로, border의 크기만큼 연산을 하여 반환
        return new MapLocationPosition
            (
                startX + _borderSizeX,
                startY + _borderSizeY,
                endX - _borderSizeX - 1,
                endY - _borderSizeY - 1
            );
    }

    private void MergeVerticalRoom(MapLocationPosition firstRoom, MapLocationPosition secondRoom)
    {
        // 생성할 통로의 넓이
        int wide;

        // 임의의 넓이를 받아옴
        wide = GetRandomVerticalWide(0, _wideMax, firstRoom.startY, firstRoom.endY);

        // 두 구역의 시작부분끼리 이어진 통로를 생성
        ConnectVerticalRoom(firstRoom.endX, secondRoom.startX, firstRoom.startY, wide);

        // 임의의 넓이를 받아옴
        wide = GetRandomVerticalWide(0, _wideMax, firstRoom.startY, firstRoom.endY);

        // 두 구역의 끝부분끼리 이어진 통로를 생성
        ConnectVerticalRoom(firstRoom.endX, secondRoom.startX, firstRoom.endY, wide);
    }

    private int GetRandomVerticalWide(int min, int max, int startLimit, int endLimit, int defaultValue = 0)
    {
        // 기본값 설정
        var wide = defaultValue;

        // 기본값을 부여할 기준값 설정
        var borderLimit = GetBorderLimit(_borderSizeY / 2);

        for (var i = 0; i < 3; i++)
        {
            // 임의의 통로 범위 설정
            wide = GetRandomWide(min, max + 1);

            // 통로 범위가 map을 초과하는지 검사
            // 통로 범위가 map을 초과하면 기본값을 대입
            if (startLimit - (wide / 2) <= borderLimit)
            {
                wide = defaultValue;
            }

            else if (endLimit + (wide / 2) >= mapY - borderLimit)
            {
                wide = defaultValue;
            }
        }

        return wide;
    }

    private void ConnectVerticalRoom(int start, int end, int lockedPosition, int connectWide = 0)
    {
        // Debug용 함수
        var tileNum = GetTileNumber();

        // 통로 좌우 넓이
        var wide = connectWide / 2;

        // 두 구역에 통로 연결
        for (var x = start; x < end; x++)
        {
            // 통로 넓이 적용
            for (var y = lockedPosition - wide; y <= lockedPosition + wide; y++)
            {
                map[y, x] = tileNum;
            }
        }
    }

    private void MergeHorizonRoom(MapLocationPosition firstRoom, MapLocationPosition secondRoom)
    {
        // 생성할 통로의 넓이
        int wide;

        // 임의의 넓이를 받아옴
        wide = GetRandomHorizonWide(0, _wideMax, firstRoom.startX, firstRoom.endX);

        // 두 구역의 시작부분끼리 이어진 통로를 생성
        ConnectHorizonRoom(firstRoom.endY, secondRoom.startY, firstRoom.startX, wide);

        // 임의의 넓이를 받아옴
        wide = GetRandomHorizonWide(0, _wideMax, firstRoom.startX, firstRoom.endX);

        // 두 구역의 끝부분끼리 이어진 통로를 생성
        ConnectHorizonRoom(firstRoom.endY, secondRoom.startY, firstRoom.endX, wide);
    }

    private int GetRandomHorizonWide(int min, int max, int startLimit, int endLimit, int defaultValue = 0)
    {
        // 기본값 설정
        var wide = defaultValue;

        // 기본값을 부여할 기준값 설정
        var borderLimit = GetBorderLimit(_borderSizeX / 2);

        for (var i = 0; i < 3; i++)
        {
            // 임의의 통로 범위 설정
            wide = GetRandomWide(min, max + 1);

            // 임의의 통로 범위가 map을 초과하는지 검사
            // 통로 범위가 map을 초과하면 기본값을 대입
            if (startLimit - (wide / 2) <= borderLimit)
            {
                wide = defaultValue;
            }

            else if (endLimit + (wide / 2) >= mapX - borderLimit)
            {
                wide = defaultValue;
            }
        }

        return wide;
    }

    private void ConnectHorizonRoom(int start, int end, int lockedPosition, int connectWide = 0)
    {
        // Debug용 함수
        var tileNum = GetTileNumber();

        // 통로 좌우 넓이
        var wide = connectWide / 2;

        // 두 구역에 통로 연결
        for (var y = start; y < end; y++)
        {
            // 통로 넓이 적용
            for (var x = lockedPosition - wide; x <= lockedPosition + wide; x++)
            {
                map[y, x] = tileNum;
            }
        }
    }

    private int GetRandomWide(int min, int max)
    {
        return Random.Range(min, max);
    }

}