using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartBar : MonoBehaviour
{

    [SerializeField]
    GameObject[] listHearts;

    int healthPoint = 10;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(healthPoint);
        if (healthPoint == 0)
        {
            //Add death animation + invoke delay to show death
            Application.LoadLevel("GameOver");
        }
    }

    public void loseHeart(int dmg)
    {
        if (healthPoint >= 0)
        {
            for (int i = 0; i < dmg; i++)
            {
                healthPoint--;
                listHearts[healthPoint].active = false;
            }
        }
        else
        {
            for (int i = 0; i < listHearts.Length - 1; i++)
            {
                listHearts[i].active = false;
            }
            healthPoint = 0;
        }
    }

    public void gainHeart(int heart)
    {
        if (healthPoint + heart <= 10)
        {
            for (int i = 0; i < heart; i++)
            {
                listHearts[healthPoint].active = true;
                healthPoint++;
            }
        }
        else
        {
            for (int i = 0; i < listHearts.Length - 1; i++)
            {
                listHearts[i].active = true;
            }
            healthPoint = 10;
        }
    }

}
