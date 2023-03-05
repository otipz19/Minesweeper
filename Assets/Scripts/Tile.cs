using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public TileType TileType { get; set; } = TileType.safe;

    private TileStatus tileStatus = TileStatus.closed;
    public TileStatus TileStatus
    {
        get => tileStatus;
        set
        {
            tileStatus = value;
            ChangeSprite();
        }
    }

    private int minesAround;
    public int MinesAround
    {
        get => minesAround;
        set
        {
            if (value < 0 || value > 8)
                throw new ArgumentException("Invalid amount of mines around the tile");
            minesAround = value;
        }
    }

    [SerializeField]
    private SpriteRenderer spriteRenderer;

    public Vector2 MapPos { get; set; }
    private void OnMouseUp()
    {
        if (Game.S.IsGameOver || TileStatus == TileStatus.opened)
            return;

        Game.S.TileClicked(this);
        ChangeSprite();
    }

    public void ChangeSprite()
    {
        switch (TileStatus)
        {
            case TileStatus.closed:
                spriteRenderer.sprite = Game.S.GetTileSprite(TileSprite.closed);
                break;
            case TileStatus.flagged:
                spriteRenderer.sprite = Game.S.GetTileSprite(TileSprite.flag);
                break;
            case TileStatus.opened:
                spriteRenderer.sprite = TileType == TileType.safe ? Game.S.GetTileSprite((TileSprite)MinesAround)
                                                                  : Game.S.GetTileSprite(TileSprite.mine);
                break;
            case TileStatus.exploded:
                spriteRenderer.sprite = Game.S.GetTileSprite(TileSprite.exploded);
                break;
        }
    }
}
