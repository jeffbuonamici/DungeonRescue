using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaderSpawner : MonoBehaviour {

    [SerializeField]
    GameObject monsterPrefab;

    Vector3 positionMonster;

    float cooldownShader1;

    GameObject[] listMonster = new GameObject[1];

    bool spawnerDestroyed = false;

    void Start()
    {
        spawnShader();
    }

    // Update is called once per frame
    void Update()
    {
        spawnCountdown();
        spawnShader();
    }

    void spawnShader()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            spawnerDestroyed = true;
        }
        if (listMonster[0] == null && cooldownShader1 < 0 && spawnerDestroyed == false)
        {
            positionMonster.x = -46;
            positionMonster.y = -0.51f;
            listMonster[0] = Instantiate(monsterPrefab, positionMonster, Quaternion.identity);
            cooldownShader1 = 3;

        }
    }

    void spawnCountdown()
    {
        if (listMonster[0] == null)
        {
            cooldownShader1 -= Time.deltaTime;
        }
    }
}
