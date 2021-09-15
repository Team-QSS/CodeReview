using System;
using UnityEngine;

public class MapGenerator : MapInitializer
{
    public void GenerateRandomMap()
    {
        // Initialize Map Data
        InitMapData();

        // Generate Type Map
        GenerateTypeMap();

        // Generate Map (Type Map To Map)
        MakeMap();
    }

    private void GenerateTypeMap()
    {
        int x, y, type;

        while (minHeap.Count > 0)
        {
            y = FindY();
            x = FindX(y);
            type = GetStructureType();
            Type targetStructure = structureTypes[type];
            progressCount = y / mapY;

            while (x != -1)
            {
                // 맵 공간이 부족하면 다음 Y 탐색
                if (!CheckMapIndexRange(x + targetStructure.x, y + targetStructure.y))
                    break;

                // 구조물 공간이 부족하면 X를 증가시켜 공간을 찾음
                // X가 맵 공간이 부족한 지점까지 도달하면 다음 Y를 탐색
                // 또는 X 가 -1이면 종료
                while (!CheckStructureArea(x, y, type))
                {
                    x = FindNextX(x, y, type);
                    if (x == -1) break;                    
                }

                // 예외면 다음 Y를 탐색
                if (x == -1) break;

                // 구조물 복사
                CopyStructureTypeToTypeMap(x, y, type);

                // 값 갱신
                minHeap.Add(y + targetStructure.y);
                type = GetStructureType();
                x = FindX(y);
            }
        }
    }

    private int FindNextX(int x, int y, int type)
    {
        if (!CheckMapIndexRange(x + structureTypes[type].x, y + structureTypes[type].y))
            return -1;

        return x + 1;
    }

    private int FindY()
    {
        if (minHeap.Count == 0)
            return -1;
        else
            return minHeap.Remove();
    }

    private int FindX(int y)
    {
        for (int x = 0; x < mapX; x++)
        {
            if (typeMap[y, x] == 0)
                return x;
        }
        return -1;
    }

    private int GetStructureType()
    {
        return UnityEngine.Random.Range(1, structureTypes.Length);   // 0 = blank structure
    }

    private bool CheckMapIndexRange(int checkPositionX, int checkPositionY)
    {
        return mapX > checkPositionX && mapY > checkPositionY;
    }

    private bool CheckStructureArea(int x, int y, int type)
    {
        Type target = structureTypes[type];
        int sizeX = target.x;
        int sizeY = target.y;
        int tempX;
        for (int i = 0; i < sizeY; i++, y++)
        {
            tempX = x;

            for (int j = 0; j < sizeX; j++, tempX++)
            {
                try
                {
                    if (y >= mapY || tempX >= mapX)
                        return false;

                    if (typeMap[y, tempX] != 0)
                        return false;
                }
                catch (IndexOutOfRangeException e)
                {
                    Debug.LogError($"i = {i}, y = {y}, j = {j}, tempX = {tempX}, mapX = {mapX}, mapY = {mapY} \n{e}");
                }
            }
        }
        return true;
    }

    private void MakeMap()
    {
        for(int y = 0; y < mapY; y++)
        {
            for(int x = 0; x < mapX; x++)
            {
                if(typeMap[y, x] != 0)
                {
                    CopyStructureTypeToMap(x, y, typeMap[y, x]);
                }
            }
        }
    }

    private void CopyStructureTypeToMap(int x, int y, int type)
    {
        Type target = structureTypes[type];
        int sizeX = structureTypes[type].x;
        int sizeY = structureTypes[type].y;
        int tempX;

        for (int positionY = 0; positionY < sizeY; positionY++, y++)
        {
            tempX = x;
            for (int positionX = 0; positionX < sizeX; positionX++, tempX++)
            {
                if (!CheckMapIndexRange(tempX, y))
                    break;

                int copyData = target.structure[positionY, positionX];
                typeMap[y, tempX] = 0;
                map[y, tempX] = copyData;
            }
        }
    }

    private void CopyStructureTypeToTypeMap(int x, int y, int type)
    {
        int sizeX = structureTypes[type].x;
        int sizeY = structureTypes[type].y;
        int tempX;

        for (int targetY = 0; targetY < sizeY; targetY++, y++)
        {
            tempX = x;
            for (int targetX = 0; targetX < sizeX; targetX++, tempX++)
            {
                if (!CheckMapIndexRange(tempX, y))
                    break;

                typeMap[y, tempX] = type;
            }
        }
    }
}
