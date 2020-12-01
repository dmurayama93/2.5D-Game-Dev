﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Text _coinText;
    //Update Coin Display
    public void UpdateCoinDisplay(int coins)
    {
        _coinText.text = "Coins: " + coins.ToString();
    }
}
