using UnityEngine;

static public class SettignsContainer
{
    static public MapSettings Map8x8 { get; set; } = new MapSettings
    {
        width = 8,
        height = 8,
        minesCount = 10,
        startPos = new Vector2(-3.5f, 4),
        cameraSize = 8,
        tileParentPos = new Vector2(0, 0)
    };

    static public MapSettings Map16x16 { get; set; } = new MapSettings
    {
        width = 16,
        height = 16,
        minesCount = 40,
        startPos = new Vector2(-3.5f, 4),
        cameraSize = 15,
        tileParentPos = new Vector2(-4, 3)
    };
    static public MapSettings Map16x30 { get; set; } = new MapSettings
    {
        width = 16,
        height = 30,
        minesCount = 99,
        startPos = new Vector2(-3.5f, 4),
        cameraSize = 19,
        tileParentPos = new Vector2(-4, 10)
    };

    static public MapSettings ChosenMapSettings { get; set; } = Map8x8;
}

