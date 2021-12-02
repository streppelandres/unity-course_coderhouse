using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;
    private int points = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddScore()
    {
        instance.points += 1;
        Debug.Log("Se agrego un punto al score -> [" + instance.GetScore() + "]");
    }

    public void RemoveScore()
    {
        instance.points -= 1;
        Debug.Log("Se quito un punto al score -> [" + instance.GetScore() + "]");
    }

    public int GetScore()
    {
        return instance.points;
    }
}
