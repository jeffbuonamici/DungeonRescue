using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class door : MonoBehaviour
{


    public GameObject roomSong;
    public GameObject muteSong;
    public Transform teleport1;
    public Vector3 spawnOffset;
    public string levelToLoad;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name == "Knight")
        {
            Debug.Log("aaa");
            col.gameObject.transform.position = teleport1.position + spawnOffset + new Vector3(0, -1);
            //SceneManager.LoadScene(levelToLoad);
            roomSong.SetActive(true);
            muteSong.SetActive(false);
        }
    }
}
