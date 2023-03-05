using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DifficultySelector : MonoBehaviour
{
    public void OnDifficultyChanged(TMP_Dropdown dropdown)
    {
        switch (dropdown.value)
        {
            case 0:
                SettignsContainer.ChosenMapSettings = SettignsContainer.Map8x8;
                break;
            case 1:
                SettignsContainer.ChosenMapSettings = SettignsContainer.Map16x16;
                break;
            case 2:
                SettignsContainer.ChosenMapSettings = SettignsContainer.Map16x30;
                break;
        }
    }
}
