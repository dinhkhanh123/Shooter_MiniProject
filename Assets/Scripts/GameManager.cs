using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
 public static GameManager instance;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject losePanel;


    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
        }
    }


    private void Update()
    {
        if(Reference.thePlayer == null)
        {
            ShowDefeatPanel();
        }
    }

    public void ShowWinPanel()
    {
        if (winPanel != null)
        {
            Time.timeScale = 0;
            winPanel.SetActive(true);
        }
    }

    public void ShowDefeatPanel()
    {
        if (losePanel != null)
        {

            losePanel.SetActive(true);
        }
    }

}
