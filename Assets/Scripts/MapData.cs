using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapData : MonoBehaviour
{

    public int
        width = 10,
        height = 5;
    public TextAsset textAsset;

    public List<string> GetTextFromFile(TextAsset tAsset)
    {
        List<string> lines = new List<string>();
        if (tAsset != null)
        {
            string textData = tAsset.text;
            string[] delimiters = { "\r\n", "\n" };
            lines.AddRange(textData.Split(delimiters, System.StringSplitOptions.None));
            lines.Reverse();
        }
        else
        {
            Debug.LogWarning("MAPDATA GetTextFromFile Error: invalid TextAsset");
        }
        return lines;
    }

    public List<string> GetTextFromFile()
    {
        return GetTextFromFile(textAsset);
    }

    public int[,] MakeMap()
    {
        int[,] map = new int[width, height];
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                map[x, y] = 0;
            }
        }
        return map;
    }

}
