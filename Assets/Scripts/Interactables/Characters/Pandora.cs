using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pandora : Character
{

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
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
