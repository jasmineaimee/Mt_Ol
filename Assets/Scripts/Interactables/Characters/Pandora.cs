using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pandora : Character
{
    private Animator animator;

    protected override void Start()
    {
        base.Start();
        animator = this.gameObject.GetComponent<Animator>();
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
