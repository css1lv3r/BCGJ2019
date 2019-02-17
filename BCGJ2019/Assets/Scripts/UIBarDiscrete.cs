using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBarDiscrete : MonoBehaviour
{
    public int Capacity = 5;
    [SerializeField] private Image[] displaySprites;
    [SerializeField] private Sprite onSprite;
    [SerializeField] private Sprite offSprite;


    public void UpdateDisplay (int newValue)
    {
        for (int i = 0; i < Capacity; ++i)
        {
            displaySprites[i].sprite = (newValue >= (i + 1)) ?onSprite:offSprite;
        }
    }
}
