using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TotalScore : MonoBehaviour
{
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

    private void FixedUpdate()
    {
        PointsText.text = "Score:" + points.ToString();
    }
}
