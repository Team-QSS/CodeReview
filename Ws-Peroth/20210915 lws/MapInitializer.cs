using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public struct MapInformation
{
    public static int x = 50;   // Map x size
    public static int y = 50;   // Map y size
}
public class MapInitializer : MonoBehaviour
{
    public float progressCount;

    protected readonly MinHeap<int> minHeap = new MinHeap<int>();
    protected int[,] typeMap = new int[MapInformation.y, MapInformation.x];
    protected int[,] map = new int[MapInformation.y, MapInformation.x];
    protected int mapX;
    protected int mapY;
    protected Type[] structureTypes;
    
    protected void InitMapData()
    {
        // Progress Reset
        progressCount = 0;

        // Heap Init
        minHeap.Add(0);

        mapX = map.GetLength(1);
        mapY = map.GetLength(0);

        // Set Default Tile
        for (int y = 0; y < mapY; y++)
        {
            for (int x = 0; x < mapX; x++)
            {
                map[y, x] = 0;
                typeMap[y, x] = 0;
            }
        }

        // Set Types
        structureTypes = new Type[] {
            new Type( new int[,]
            {
                { 0 }
            }),
            new Type( new int[,]
            {
                { 14, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 15 },
                { 5,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4  },
                { 5,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4  },
                { 5,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4  },
                { 5,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4  },
                { 5,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4  },
                { 5,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4  },
                { 19, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 20 }
            }),
            new Type( new int[,]
            {
                { 14, 7, 7, 7, 7, 7, 7, 7, 7, 7, 15 },
                { 5,  0, 0, 0, 0, 0, 0, 0, 0, 0, 4  },
                { 5,  0, 0, 0, 0, 0, 0, 0, 0, 0, 4  },
                { 5,  0, 0, 0, 0, 0, 0, 0, 0, 0, 4  },
                { 5,  0, 0, 0, 0, 0, 0, 0, 0, 0, 4  },
                { 5,  0, 0, 0, 0, 0, 0, 0, 0, 0, 4  },
                { 5,  0, 0, 0, 0, 0, 0, 0, 0, 0, 4  },
                { 5,  0, 0, 0, 0, 0, 0, 0, 0, 0, 4  },
                { 5,  0, 0, 0, 0, 0, 0, 0, 0, 0, 4  },
                { 5,  0, 0, 0, 0, 0, 0, 0, 0, 0, 4  },
                { 5,  0, 0, 0, 0, 0, 0, 0, 0, 0, 4  },
                { 5,  0, 0, 0, 0, 0, 0, 0, 0, 0, 4  },
                { 5,  0, 0, 0, 0, 0, 0, 0, 0, 0, 4  },
                { 19, 2, 2, 2, 2, 2, 2, 2, 2, 2, 20 }
            })
        };
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
        x = structure.GetLength(1);
        y = structure.GetLength(0);
    }
}