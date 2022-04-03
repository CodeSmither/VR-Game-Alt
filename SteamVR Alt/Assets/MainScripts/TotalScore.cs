using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TotalScore : MonoBehaviour
{
    // stores the players score
    private int points;

    public int Points
    {
        get
        {
            return points;
        }
        set
        {
            points = value;
        }
    }

    [SerializeField] private Text PointsText;

    //sets the players score into a text format to be read in the gameover screen.
    private void FixedUpdate()
    {
        PointsText.text = "Score:" + points.ToString();
    }
    //resets the players score
    public void Restart()
    {
        Points = 0;
    }
}
