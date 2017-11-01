using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartBar : MonoBehaviour
{

    [SerializeField]
    GameObject[] listHearts;

    int index;

    // Use this for initialization
    void Start()
    {
        index = listHearts.Length - 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            loseHeart(1);
            Debug.Log(index);
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            gainHeart(1);
            Debug.Log(index);
        }
    }

    void loseHeart(int dmg)
    {
        for (int i = 0; i < dmg; i++)
        {
            listHearts[index].active = false;
            index--;
        }
        if (index < 0)
        {
            Time.timeScale = 0;
            index = 0;
        }
    }

    void gainHeart(int heart)
    {
        for (int i = 0; i < heart; i++)
        {
            listHearts[index].active = true;
            index++;
        }
        if (index > 9)
        {
            index = 9;
        }
    }

}
