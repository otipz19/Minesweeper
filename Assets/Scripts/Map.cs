using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map
{
    public MapSettings Settings { get; set; }

    public Map(MapSettings settings)
    {
        Settings = settings;
        tiles = new Tile[Settings.width, Settings.height];
        mines = new List<Tile>();
        Camera.main.orthographicSize = settings.cameraSize;
        tilesParent = new GameObject("TilesParent").transform;
    }

    private Tile[,] tiles;
    private List<Tile> mines;
    private Transform tilesParent;
    public void PlaceTiles()
    {
        for (int y = 0; y < Settings.height; y++)
            for (int x = 0; x < Settings.width; x++)
            {
                Tile newTile = GameObject.Instantiate(Game.S.TilePrefab).GetComponent<Tile>();
                tiles[x, y] = newTile;
                newTile.MapPos = new Vector2(x, y);
                newTile.transform.position = new Vector3(Settings.startPos.x + x, Settings.startPos.y - y, 0);
                newTile.transform.parent = tilesParent;
            }

        tilesParent.transform.position = Settings.tileParentPos;
    }

    public void GenerateMap(Tile clickedTile)
    {
        GenerateMines(clickedTile);
        GenerateDigits();
    }

    private void GenerateMines(Tile clickedTile)
    {
        int curMinesCount = 0;
        while (curMinesCount < Settings.minesCount)
        {
            int x = Random.Range(0, Settings.width);
            int y = Random.Range(0, Settings.height);
            if (tiles[x, y].TileType == TileType.safe && tiles[x, y] != clickedTile)
            {
                tiles[x, y].TileType = TileType.mine;
                mines.Add(tiles[x, y]);
                curMinesCount++;
            }
        }
    }

    private void GenerateDigits()
    {
        foreach (Tile mine in mines)
        {
            for (int i = -1; i < 2; i++)
                for (int j = -1; j < 2; j++)
                {
                    int yOffset = (int)mine.MapPos.y + i;
                    int xOffset = (int)mine.MapPos.x + j;
                    if (yOffset >= 0 && yOffset < Settings.height && xOffset >= 0 && xOffset < Settings.width &&
                        tiles[xOffset, yOffset].TileType == TileType.safe)
                        tiles[xOffset, yOffset].MinesAround++;
                }
        }
    }

    public void CascadeOpen(Tile clickedTile)
    {
        var tilesAlreadyChecked = new HashSet<Tile>();
        var tilesToCheck = new Queue<Tile>();
        tilesToCheck.Enqueue(clickedTile);

        while (tilesToCheck.Count != 0)
        {
            Tile curTile = tilesToCheck.Dequeue();
            for (int i = -1; i < 2; i++)
                for (int j = -1; j < 2; j++)
                {
                    int yOffset = (int)curTile.MapPos.y + i;
                    int xOffset = (int)curTile.MapPos.x + j;
                    if (yOffset >= 0 && yOffset < Settings.height && xOffset >= 0 && xOffset < Settings.width &&
                               !tilesAlreadyChecked.Contains(tiles[xOffset, yOffset]))
                    {
                        Tile neighbour = tiles[xOffset, yOffset];
                        if (neighbour.TileType == TileType.safe && neighbour.TileStatus != TileStatus.flagged)
                        {
                            neighbour.TileStatus = TileStatus.opened;
                            if(neighbour.MinesAround == 0)
                                tilesToCheck.Enqueue(neighbour);
                        }
                        tilesAlreadyChecked.Add(neighbour);
                    }
                }
        }
    }

    /// <returns>True if all mines are flagged</returns>
    public bool CheckMines()
    {
        int flaggedMines = 0;
        foreach (Tile mine in mines)
        {
            if (mine.TileStatus == TileStatus.flagged)
                flaggedMines++;
        }
        if (flaggedMines == mines.Count)
            return true;
        return false;
    }

    public void RevealMines()
    {
        foreach (Tile mine in mines)
            mine.TileStatus = TileStatus.opened;
    }
}
