using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pandora : Character
{

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update() //Pandora isn't a speaking character, so we override update.
    {
        // if player close enough, Pandora bows.
        if(inRange)
        {
            animator.SetBool("InRange", true);
        }
        else
        {
            animator.SetBool("InRange", false);
        }
    }
}
