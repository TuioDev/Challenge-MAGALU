using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class LevelPoints : MonoBehaviour
{
    private Image[] PointsImageReference;
    
    public Image[] GetPointsImageReference => PointsImageReference;

    void Awake()
    {
        PointsImageReference = GetComponentsInChildren<Image>();
        PointsImageReference = PointsImageReference.Skip(1).ToArray(); // The first is element is the self Image
    }

    public void ChangePointsSprites(Sprite sprite, int points)
    {
        for (int i = 0; i < points; i++)
        {
            PointsImageReference[i].sprite = sprite;
        }
    }

    // Maybe do a method that changes back the points when resetting the stats
}
