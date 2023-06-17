using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;

public class DisplayHealthBar : AttributeBar
{
    protected override void Start()
    {
        base.Start();
        maxValue = player.maxHP;
    }
}
