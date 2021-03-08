using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zeus : Character
{

    protected override void Start()
    {
        base.Start();
    }

    public override void ControlSpeaking()
    {
        base.ControlSpeaking();
        if(!missionActive)
        {
            missionActive = true;
            speechText.text = missionStatement[0];
        }
        else
        {

        }
    }

    protected override void Update()
    {
        base.Update();
    }
}
