﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyparticle : MonoBehaviour
{

    public float timeLeft;

    void Update() {
        timeLeft -= Time.deltaTime;
        if (timeLeft <= 0.0f) {
           Destroy(this.gameObject);
         }
    }
}
