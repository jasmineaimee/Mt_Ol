﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PandorasBox : Collectable
{
    //[Header("P A N D O R A S  B O X")]
    void Start()
    {
        // set collectable type
        type = Collectables.Box;
    }

    // void OnTriggerEnter(Collider other)
    // {
    //     // just to override the other stuff in collectable (keeping this class as a collectable child for future grabbing implementation)
    // }
}
