using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Reference 
{
    public static PlayerController thePlayer;
    public static BossEnemy theBoss;
    public static GameObject canvas;
    
 
    public static LevelManager levelManager;

    public static List<GameObject> allEnemies = new List<GameObject>();
    public static bool isKey= false;
}
