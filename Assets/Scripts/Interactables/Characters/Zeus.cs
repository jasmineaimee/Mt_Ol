public class Zeus : Character
{
    protected override void Start()
    {
        base.Start();
        missionActive = true;
        missionStatement = "Philotes! Thank you for coming to see me. I have a quest for you. I need you to go and gather all of the things I need to create a human woman, Pandora! These are the things I require: \nDirt\nWater\nA Symbol of Grace\nJewellery\nFlowers\nSome Wovens\nAnd a Symbol of Deceit";
        talkingPoints[0] = "Have you retrieved all of the ingredients yet? You can check Pandora's room to see everything you have recieved so far, just look for the purple flowers.";
        talkingPoints[1] = "Thank you for finding everything I needed. You can find Pandora and the box in her room. Her favourite flowers are purple.";
    }

    // zeus overrides control speaking because he gives the quest, rather than an item
    protected override void ControlSpeaking()
    {
        //base.ControlSpeaking();
        if(!GameManager.Instance.hasSeenZeus)
        {
            GameManager.Instance.hasSeenZeus = true;
            speechText.text = missionStatement;
        }
        else if(GameManager.Instance.hasSeenZeus && GameManager.Instance.numCollectables >= 6)
        {
            speechText.text = talkingPoints[1];
        }
        else
        {
            speechText.text = talkingPoints[0];
        }
    }
}
