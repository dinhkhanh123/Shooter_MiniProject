using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image filledPart;
    [SerializeField] private Image background;

    public void ShowHealthFraction(float fraction)
    {
        filledPart.rectTransform.localScale = new Vector3(fraction, 1, 1);
        if (fraction <= 1)
        {
            filledPart.enabled = true;
            background.enabled = true;
        }
        else
        {
            filledPart.enabled = false;
            background.enabled = false;
        }
    }

}
