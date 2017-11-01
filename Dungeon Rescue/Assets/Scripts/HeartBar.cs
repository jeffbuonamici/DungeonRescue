using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartBar : MonoBehaviour
{

    [SerializeField]
    GameObject[] listHearts;

    int index;
    int healthPoint = 10;

    // Use this for initialization
    void Start()
    {
        index = listHearts.Length - 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (healthPoint == 0)
        {
            Application.LoadLevel("GameOver");
        }
    }

    public void loseHeart(int dmg)
    {
        healthPoint -= dmg;
        if (healthPoint >= 0)
        {
            for (int i = 0; i < dmg; i++)
            {
                listHearts[index].active = false;
                index--;
            }
        }
        else
        {
            for (int i = 0; i < listHearts.Length-1; i++)
            {
                listHearts[i].active = false;
            }
            healthPoint = 0;
        }
    }

    public void gainHeart(int heart)
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
