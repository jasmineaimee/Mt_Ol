public class Athena : Character
{
    protected override void Start()
    {
        base.Start();
        if(GameManager.Instance.isInInventory(Collectables.Clothing))
        {
            missionActive = true;
            riddleSpot.SetActive(false);
        }
        missionStatement = "Philotes. The grapevine says that Zeus wants some Clothing. I'll give you some, but I need you to answer some questions for me first. Just Press A on the spot by the chest to start the test.";
        talkingPoints[0] = "This human business seems dangerous. Do you know what Zeus' plan is?";
        talkingPoints[1] = "The idea of a human woman makes me nervous. I wondxer what Zeus is up to.";
        talkingPoints[2] = "I wouldn't keep Zeus waiting. Go find all of the things he needs.";
        talkingPoints[3] = "I heard Zeus said something about a box. I hope for everyone's sake that isn't true.";
    }
}
