public class Kharities : Character
{
    protected override void Start()
    {
        base.Start();
        if(GameManager.Instance.isInInventory(Collectables.Flowers))
        {
            missionActive = true;
        }
        missionStatement = "Hello Philotes! We need you to make sure our boxes are lighting in the correct order, and then we can give you the Jewellery Zeus wanted. Just Press A on the spot by the chest to start!";
        talkingPoints[0] = "If you're looking for Hades, he's in the Underworld right now. I could take you to him though! Just head to the teleportation spot in the corner, and Press A.";
        talkingPoints[1] = "The creation of Pandora makes Hades nervous. I don't see the problem with the whole thing though. Don't we want souls in the Underworld?";
        talkingPoints[2] = "Everytime I come back from seeing hades in the Underworld, he gives me a flower to bring back here for my mother, Demeter.";
        talkingPoints[3] = "I've been thinking of things that Zeus could put in the box. I'm not sure he'll like my ideas though. Maybe I'll go to the Underworld and ask Hades what he thinks.";
    }

    protected override void ControlSpeaking()
    {
        base.ControlSpeaking();
        riddleSpot.SetActive(true);
    }
}
