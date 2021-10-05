using UnityEngine;
public struct MapInformation
{
    public const int X = 100;   // Map x 크기
    public const int Y = 50;   // Map y 크기
}
    
public class MapInitializer : MonoBehaviour
{
    [SerializeField]
    protected bool isDebugMode;

    protected int[,] map;
    protected int mapX;
    protected int mapY;

    protected virtual void InitializeMapData(int X = MapInformation.X, int Y = MapInformation.Y)
    {
        map = new int[Y, X];

        mapX = map.GetLength(1);
        mapY = map.GetLength(0);

        // map의 값 초기화
        for (var y = 0; y < mapY; y++)
        {
            for (var x = 0; x < mapX; x++)
            {
                map[y, x] = 1;
            }
        }
    }
}

/// <summary>
/// [position information class] field : startX = 0, startY = 0, endX = 0, endY = 0
/// </summary>
public class MapLocationPosition
{
    public int startX, endX;
    public int startY, endY;

    public MapLocationPosition(int startX = 0, int startY = 0, int endX = 0, int endY = 0)
    {
        this.startX = startX;
        this.startY = startY;

        this.endX = endX;
        this.endY = endY;
    }

}

public struct Type
{
    public int x;
    public int y;
    public int[,] structure;

    public Type(int[,] structure)
    {
        this.structure = structure;
        x = structure.GetLength(0);
        y = structure.GetLength(1);
    }
}