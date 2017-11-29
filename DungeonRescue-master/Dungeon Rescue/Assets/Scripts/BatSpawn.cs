using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatSpawn : MonoBehaviour
{
    // Use this for initialization
    [SerializeField]
    GameObject monsterPrefab;

    Vector3 positionMonster;

    float cooldownBat1, cooldownBat2;

    GameObject[] listMonster = new GameObject[2];

    void Start()
    {
        spawnBats();
    }

    // Update is called once per frame
    void Update()
    {
        spawnCountdown();
        spawnBats();
    }

    void spawnBats()
    {
        if (listMonster[0] == null && cooldownBat1 < 0)
        {
            positionMonster.x = -40;
            positionMonster.y = 3;
            listMonster[0] = Instantiate(monsterPrefab, positionMonster, Quaternion.identity);
            cooldownBat1 = 3;

        }
        if (listMonster[1] == null && cooldownBat2 < 0)
        {
            positionMonster.x = -30;
            positionMonster.y = 3;
            listMonster[1] = Instantiate(monsterPrefab, positionMonster, Quaternion.identity);
            cooldownBat2 = 3;
        }
    }

    void spawnCountdown()
    {
        if (listMonster[0] == null)
        {
            cooldownBat1 -= Time.deltaTime;
        }
        if (listMonster[1] == null)
        {
            cooldownBat2 -= Time.deltaTime;
        }
    }
}
