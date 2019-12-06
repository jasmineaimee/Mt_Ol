using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PandorasBox : Collectable
{
    //[Header("P A N D O R A S  B O X")]
    void Start()
    {
        type = Collectables.Box;
    }

    void Update()
    {
        transform.Rotate(5f, 5f, 5f);
    }

    void OnTriggerEnter(Collider other)
    {
        // just to override the other stuff in collectable (keeping this class as a collectiable child for future grabbing implementation)
    }
}
