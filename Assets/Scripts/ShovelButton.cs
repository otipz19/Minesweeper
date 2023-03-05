using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShovelButton : MonoBehaviour
{
    [SerializeField]
    private Image image;
    [SerializeField]
    private Sprite shovelSprite;
    [SerializeField]
    private Sprite flagSprite;

    private bool isShovel = true;

    public void OnClick()
    {
        if (isShovel)
        {
            isShovel = false;
            image.sprite = flagSprite;
            Game.S.FlagClick = true;
        }
        else
        {
            isShovel = true;
            image.sprite = shovelSprite;
            Game.S.FlagClick = false;
        }
    }
}
