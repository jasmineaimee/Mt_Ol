public class Hera : Character
{
    protected override void Start()
    {
        base.Start();
        if(GameManager.Instance.isInInventory(Collectables.Water))
        {
            missionActive = true;
        }
        missionStatement = "Philotes! Welcome! Oh. My husband wants some water? Well, if he wants it so badly, you can go through this maze, and then I'll give it to you. Just Press A on the teleportation spot by the chest to head to the maze.";
        talkingPoints[0] = "If Zeus needs something else, tell him to ask a different goddess. I'm done helping. But if you want to do another maze, I won't stop you.";
        talkingPoints[1] = "What did Zeus say about this human woman? What will she look like? Nevermind, I'm sure I can guess. You are welcopme to complete another maze if you wish.";
        talkingPoints[2] = "Apparently, I'm known for being vengeful. But if you ask me, they had it coming.";
        talkingPoints[3] = "Zeus is making the box? That's concerning, but ultimately not my problem. If Zeus makes a mess, he can fix it himself.";
    }

    protected override void ControlSpeaking()
    {
        base.ControlSpeaking();
        teleport.SetActive(true);
    }
}
