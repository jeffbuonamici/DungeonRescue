﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour {
    void Start()
    {
    }
        void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Application.LoadLevel("Scene");
        }
    }
}