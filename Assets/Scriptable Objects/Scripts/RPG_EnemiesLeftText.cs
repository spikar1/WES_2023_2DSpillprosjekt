using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RPG_EnemiesLeftText : MonoBehaviour
{
    TMP_Text text;

    private void Start()
    {
        text = GetComponent<TMP_Text>();
        UpdateText();
    }

    public void UpdateText()
    {
        int enemiesLeft = 0;
        foreach (var enemy in FindObjectsOfType<RPG_Enemy>())
        {
            if(!enemy.IsDead)
                enemiesLeft++;
        }
        text.text = "Enemies left "+ enemiesLeft;
    }
}
