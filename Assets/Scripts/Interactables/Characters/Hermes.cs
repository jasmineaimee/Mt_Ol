public class Hermes : Character
{
    protected override void Start()
    {
        base.Start();
        if(GameManager.Instance.isInInventory(Collectables.Deceit))
        {
            missionActive = true;
        }
        missionStatement = "Philotes! I have something for you for to give to Zeus. I need you to complete this maze for me though. Just Press A on the teleportation spot by the chest to head to the maze.";
        talkingPoints[0] = "Zeus is making a human woman? That is worrying. I wonder what he'll do with her? Is he going to keep her on the mountain, I wonder?";
        talkingPoints[1] = "Do you like my things? I aquired them throuhg all my travels. Some were gifts. Others were unknowing gifts.";
        talkingPoints[2] = "The only thing missing from my colection is a tortoise. I do love those things.";
        talkingPoints[3] = "Someone told me that Zeus is making the box as well? That is very grave news. In other news, I might leave for a while.";
    }

    protected override void ControlSpeaking()
    {
        base.ControlSpeaking();
        teleport.SetActive(true);
    }
}
