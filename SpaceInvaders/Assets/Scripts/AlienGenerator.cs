using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienGenerator : MonoBehaviour
{
    public static AlienGenerator Instance = null;

    public GameObject[] alienArr = new GameObject[5];
    int alienNum = 0;
    Vector2 pos = new Vector2(-40,25);

	// Use this for initialization
	void Start ()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
        for (int y = 0; y < 5; y++)
        {
            for (int x = 0; x < 5; x++)
            {
                Instantiate(alienArr[alienNum], pos, Quaternion.identity);
                pos.x += 9;
            }
            pos.x -= 45;
            pos.y -= 5;
            alienNum++;
        }
	}
	
	
}
