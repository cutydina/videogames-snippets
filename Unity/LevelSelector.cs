using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelSelector : MonoBehaviour
{
    public TextMeshProUGUI score;
    public int scoreAmount;

    void Start()
    {
        scoreAmount = 30;
        score = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        score.text = scoreAmount.ToString();
    }

    public void LessScore()
    {
        scoreAmount -= 5;
    }

    public void MoreScore()
    {
        scoreAmount += 5;
    }

}
