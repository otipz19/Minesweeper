using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    private static Game s;
    public static Game S => s;
    private void Awake()
    {
        if (s != null)
            throw new ApplicationException("Game.s is already assigned");
        s = this;
    }

    [SerializeField]
    private Sprite[] tileSprites;
    public Sprite GetTileSprite(TileSprite tileSprite)
    {
        return tileSprites[(int)tileSprite];
    }

    [SerializeField]
    private GameObject tilePrefab;
    public GameObject TilePrefab => tilePrefab;

    private Map map;

    private void Start()
    {
        map = new Map(SettignsContainer.ChosenMapSettings);
        map.PlaceTiles();
    }

    private bool firstClick = true;
    public bool FlagClick { get; set; } = false;
    public void TileClicked(Tile clickedTile)
    {
        if (FlagClick)
        {
            if (clickedTile.TileStatus == TileStatus.closed)
                clickedTile.TileStatus = TileStatus.flagged;
            else
                clickedTile.TileStatus = TileStatus.closed;

            if (map.CheckMines())
                GameOver(true);
        }
        else if (clickedTile.TileStatus == TileStatus.closed)
        {
            if (firstClick)
            {
                firstClick = false;
                map.GenerateMap(clickedTile);
            }

            if (clickedTile.TileType == TileType.safe)
                map.CascadeOpen(clickedTile);
            else
            {
                GameOver(false, clickedTile);
            }
        }
    }

    public bool IsGameOver { get; set; }
    private void GameOver(bool gameWon, Tile clickedTile = null)
    {
        IsGameOver = true;
        if (gameWon)
            Debug.Log("You won!");
        else
        {
            Debug.Log("You lost!");
            map.RevealMines();
            clickedTile.TileStatus = TileStatus.exploded;
        }
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene("Menu");
        }
    }
}